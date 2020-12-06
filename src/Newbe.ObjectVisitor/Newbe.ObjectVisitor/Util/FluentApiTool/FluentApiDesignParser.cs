using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// FluentApiDesignParser
    /// </summary>
    public class FluentApiDesignParser : IFluentApiDesignParser
    {
        /// <inheritdoc />
        public FluentApiDesign Parse(string designContent)
        {
            var re = new FluentApiDesign {SourceDesignContent = designContent};
            ParseBlock(designContent, re);
            ParseParametersBlock(re);
            ParseActionBlock(re);
            return re;
        }

        private void ParseActionBlock(FluentApiDesign re)
        {
            if (string.IsNullOrEmpty(re.StateDiagramBlock))
            {
                return;
            }

            var allLines = ReadAllLines(re.StateDiagramBlock);
            var dataLines = allLines.Where(x => x.Contains(":")).ToArray();
            re.ApiSteps = dataLines.Select(ParseStep).ToArray();

            ApiStep ParseStep(string line)
            {
                var tuple = Split(line, ":");
                var direction = tuple.Item1;
                var action = tuple.Item2;
                var directionEndpoint = Split(direction, "-->");
                var from = directionEndpoint.Item1;
                var to = directionEndpoint.Item2;

                string share = string.Empty;
                if (action.Contains("share"))
                {
                    var split = Split(action, "share");
                    action = split.Item1;
                    share = split.Item2;
                }

                string returning = string.Empty;
                if (action.Contains("return"))
                {
                    var split = Split(action, "return");
                    action = split.Item1;
                    returning = split.Item2;
                }

                var step = new ApiStep
                {
                    From = from,
                    To = to,
                    SourceContent = line,
                    Action = action,
                    Return = returning,
                    Share = share
                };

                return step;
            }
        }

        internal void ParseParametersBlock(FluentApiDesign re)
        {
            if (string.IsNullOrEmpty(re.ParametersBlock))
            {
                return;
            }

            var allLines = ReadAllLines(re.ParametersBlock);

            var defLines = allLines.Where(x => x.Contains(":")).ToArray();
            var dataLines = defLines.Where(x => x.StartsWith("#")).ToArray();

            var objectVisitor = re.V()
                .WithExtendObject<FluentApiDesign, Tuple<string, string>>()
                .ForEach(context => FillValue(context))
                .CreateVisitor();

            foreach (var dataLine in dataLines)
            {
                var tuple = Split(dataLine, ":");
                objectVisitor.Run(re, tuple);
            }

            var mappingLines = defLines.Where(x => x.StartsWith("_")).ToArray();
            re.ActionMapping = mappingLines.Select(x => Split(x, ":")).ToDictionary(x => x.Item1, x => x.Item2);
        }

        private void FillValue(IObjectVisitorContext<FluentApiDesign, Tuple<string, string>, object> context)
        {
            var tuple = context.ExtendObject;
            var pName = tuple.Item1.TrimStart('#');
            if (context.Name == pName)
            {
                context.Value = tuple.Item2;
            }
        }

        internal void ParseBlock(string designContent, FluentApiDesign re)
        {
            using var sr = new StringReader(designContent);
            string? nowLine;
            var stateBlock = new List<string>();
            var parameterBlock = new List<string>();
            do
            {
                nowLine = sr.ReadLine();
                if (nowLine != null && !string.IsNullOrEmpty(nowLine))
                {
                    if (nowLine.Contains("```mermaid"))
                    {
                        stateBlock.Add(nowLine);
                        do
                        {
                            nowLine = sr.ReadLine();
                            stateBlock.Add(nowLine);
                        } while (!IsEnd(nowLine, "```"));
                    }
                    else if (nowLine.Contains("```cs"))
                    {
                        parameterBlock.Add(nowLine);
                        do
                        {
                            nowLine = sr.ReadLine();
                            parameterBlock.Add(nowLine);
                        } while (!IsEnd(nowLine, "```"));
                    }
                }
            } while (nowLine != null);

            re.ParametersBlock = string.Join(Environment.NewLine, parameterBlock);
            re.StateDiagramBlock = string.Join(Environment.NewLine, stateBlock);

            static bool IsEnd(string? content, string endTag)
            {
                return content == null || content.Contains(endTag);
            }
        }

        private static List<string> ReadAllLines(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new List<string>();
            }

            using var sr = new StringReader(content);
            var allLines = new List<string>();
            string? nowLine;
            do
            {
                nowLine = sr.ReadLine();
                if (!string.IsNullOrEmpty(nowLine))
                {
                    allLines.Add(nowLine);
                }
            } while (nowLine != null);

            return allLines;
        }

        private static Tuple<string, string> Split(string source, string d)
        {
            if (!source.Contains(d))
            {
                throw new ArgumentOutOfRangeException(nameof(source), $"{source} do not contains {d}");
            }

            var strings = source.Split(d);
            if (strings.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(source), $"{source} must contains only one {d}");
            }

            return new Tuple<string, string>(strings[0].Trim(), strings[1].Trim());
        }
    }
}