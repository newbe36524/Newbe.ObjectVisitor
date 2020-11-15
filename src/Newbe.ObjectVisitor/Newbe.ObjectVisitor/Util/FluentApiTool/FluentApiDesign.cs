using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor
{
    public class FluentApiDesign
    {
        public string SourceDesignContent { get; set; } = null!;
        public string StateDiagramBlock { get; set; } = null!;
        public string ParametersBlock { get; set; } = null!;

        public string BuilderContextType { get; set; } = null!;
        public string Namespace { get; set; } = null!;
        public string BuilderTypeName { get; set; } = null!;

        public Dictionary<string, string> ActionMapping { get; set; } = new Dictionary<string, string>();
        public ApiStep[] ApiSteps { get; set; } = Array.Empty<ApiStep>();
    }
}