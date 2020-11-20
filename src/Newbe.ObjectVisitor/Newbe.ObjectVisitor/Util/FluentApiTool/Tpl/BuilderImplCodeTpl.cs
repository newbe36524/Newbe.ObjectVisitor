#pragma warning disable 8618
namespace Newbe.ObjectVisitor.Tpl
{
    public class BuilderImplCodeTpl : ICodeTpl
    {
        public string ImplClassName { get; set; }
        public string ImplClassConstructorName { get; set; }
        public string Namespace { get; set; }
        public string[] Interfaces { get; set; }
        public string BuilderContextType { get; set; }
        public string[] UserImplMethods { get; set; }
        public string[] AutoGenerateMethods { get; set; }

        public string Template { get; } = @"
using System;
namespace [Namespace]
{
    public class [ImplClassName]:Newbe.ObjectVisitor.IFluentApi
        [Interfaces]
    {
        private readonly [BuilderContextType] _context;
        public [ImplClassName]([BuilderContextType] context)
        {
            _context = context;
        }

        #region UserImpl
        [UserImplMethods]
        #endregion

        #region AutoGenerate
        [AutoGenerateMethods]
        #endregion
    }
}
";

        #region MyRegion

        public string Format()
        {
            return CodeTemplateReplacer<BuilderImplCodeTpl>.GetContent(this);
        }

        #endregion
    }
}