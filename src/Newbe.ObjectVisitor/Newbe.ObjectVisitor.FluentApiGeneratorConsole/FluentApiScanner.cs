using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Newbe.ObjectVisitor.FluentApiGeneratorConsole
{
    public class FluentApiScanner : IFluentApiScanner
    {
        private readonly ILogger<FluentApiScanner> _logger;

        public FluentApiScanner(
            ILogger<FluentApiScanner> logger)
        {
            _logger = logger;
        }

        public async Task ScanAsync()
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "../../../../", "Newbe.ObjectVisitor");
            var filePaths = Directory.GetFiles(baseDir, "*.fluent.md", SearchOption.AllDirectories);
            var files = await Task.WhenAll(filePaths.Select(async x => new FluentApiFile
            {
                MarkdownContent = await File.ReadAllTextAsync(x),
                FluentMarkdownFilePath = x,
                MarkdownFileLockPath = $"{x}.lock",
            }));

            var tasks = files.Select(GenerateAsync);
            await Task.WhenAll(tasks);
        }

        private async Task GenerateAsync(FluentApiFile file)
        {
            var markdownHash = GetHash();
            if (!NeedGeneration())
            {
                _logger.LogInformation("No Change : {markdown}", file.FluentMarkdownFilePath);
                return;
            }

            _logger.LogInformation("Start : {markdown}", file.FluentMarkdownFilePath);
            var parser = new FluentApiDesignParser();
            var design = parser.Parse(file.MarkdownContent);
            var generator = new FluentApiFileGenerator();
            var output = generator.Generate(design);
            var codes = output.FluentApiFiles.AutoGenerate;
            await File.WriteAllTextAsync(GetCsFilePath(), codes);
            await File.WriteAllTextAsync(file.MarkdownFileLockPath, markdownHash);
            _logger.LogInformation("End : {markdown}", file.FluentMarkdownFilePath);

            bool NeedGeneration()
            {
                if (!File.Exists(file.MarkdownFileLockPath))
                {
                    return true;
                }

                var existsHash = File.ReadAllText(file.MarkdownFileLockPath);
                return markdownHash != existsHash;
            }

            string GetHash()
            {
                using var hashAlgorithm = HashAlgorithm.Create("md5");
                var computeHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(file.MarkdownContent));
                var hash = Convert.ToBase64String(computeHash);
                return hash;
            }

            string GetCsFilePath()
            {
                var directory = Path.GetDirectoryName(file.FluentMarkdownFilePath);
                var builderTypeName = design.BuilderTypeName;
                var indexOf = builderTypeName.IndexOf('<');
                if (indexOf >= 0)
                {
                    builderTypeName = builderTypeName.Substring(0, indexOf);
                }

                var path = Path.Combine(directory!, $"{builderTypeName}.cs");
                return path;
            }
        }
    }
}