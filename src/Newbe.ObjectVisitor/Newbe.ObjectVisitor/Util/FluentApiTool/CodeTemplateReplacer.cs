using System;
using System.Linq;
using Newbe.ObjectVisitor.Tpl;

namespace Newbe.ObjectVisitor
{
    public static class CodeTemplateReplacer<T>
        where T : ICodeTpl
    {
        private static readonly ICachedObjectVisitor<T, CodeInput> Visitor = default(T)!.V()
            .WithExtendObject<T, CodeInput>()
            .ForEach<T, CodeInput, string>(context => Replace(context))
            .ForEach<T, CodeInput, string[]>(context => ReplaceArray(context))
            .Cache();

        private static void ReplaceArray(IObjectVisitorContext<T, CodeInput, string[]> c)
        {
            var input = c.ExtendObject;
            if (string.IsNullOrEmpty(input.Result))
            {
                input.Result = input.Template;
            }

            var placeHolder = $"[{c.PropertyInfo.Name}]";
            string newValue = c.Value?.Any() == true ? string.Join(Environment.NewLine, c.Value) : string.Empty;
            input.Result = input.Result.Replace(placeHolder, newValue);
        }

        private static void Replace(IObjectVisitorContext<T, CodeInput, string> c)
        {
            var input = c.ExtendObject;
            if (string.IsNullOrEmpty(input.Result))
            {
                input.Result = input.Template;
            }

            var placeHolder = $"[{c.PropertyInfo.Name}]";
            input.Result = input.Result.Replace(placeHolder, c.Value);
        }

        private class CodeInput
        {
            public string Template { get; set; }
            public string Result { get; set; }
        }

        public static string GetContent(T tpl)
        {
            var input = new CodeInput
            {
                Template = tpl.Template
            };
            Visitor.Run(tpl, input);
            return input.Result;
        }
    }
}