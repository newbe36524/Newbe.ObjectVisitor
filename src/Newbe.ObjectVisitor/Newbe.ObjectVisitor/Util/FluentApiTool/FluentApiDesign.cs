using System;
using System.Collections.Generic;

namespace Newbe.ObjectVisitor
{
    /// <summary>
    /// Fluent API design
    /// </summary>
    public class FluentApiDesign
    {
        /// <summary>
        /// Source design content
        /// </summary>
        public string SourceDesignContent { get; set; } = null!;

        /// <summary>
        /// Content block of state diagram
        /// </summary>
        public string StateDiagramBlock { get; set; } = null!;

        /// <summary>
        /// Content block of parameters
        /// </summary>
        public string ParametersBlock { get; set; } = null!;

        /// <summary>
        /// Target builder context type of fluent api
        /// </summary>
        public string BuilderContextType { get; set; } = null!;

        /// <summary>
        /// Namespace of fluent API
        /// </summary>
        public string Namespace { get; set; } = null!;

        /// <summary>
        /// Type of generated builder fluent API
        /// </summary>
        public string BuilderTypeName { get; set; } = null!;

        /// <summary>
        /// Mapping parsed from <see cref="ParametersBlock"/>. It indicate the relationship between short name and full name of action. Key: short name, Value: full name
        /// </summary>
        public Dictionary<string, string> ActionMapping { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// API step design of API
        /// </summary>
        public ApiStep[] ApiSteps { get; set; } = Array.Empty<ApiStep>();
    }
}