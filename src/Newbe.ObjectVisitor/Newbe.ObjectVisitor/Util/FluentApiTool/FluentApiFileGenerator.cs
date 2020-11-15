using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newbe.ObjectVisitor.Tpl;

namespace Newbe.ObjectVisitor
{
    public class FluentApiFileGenerator : IFluentApiFileGenerator
    {
        public const string EdgeNodeName = "[*]";

        public FluentApiOutput Generate(FluentApiDesign design)
        {
            var steps = ReplaceWithMapping(design.ApiSteps, design.ActionMapping);

            var ordered = steps.OrderByDescending(x => x.IsStart)
                .ThenBy(x => x.From)
                .ThenBy(x => x.IsEnd)
                .ThenBy(x => x.To);


            var nodeDic = new Dictionary<string, Node>();
            foreach (var lineItem in ordered)
            {
                var key = lineItem.From;
                if (!nodeDic.TryGetValue(key, out var node))
                {
                    node = new Node {Name = lineItem.From, Steps = new List<ApiStep>()};
                }

                node.Steps.Add(lineItem);
                nodeDic[key] = node;
            }

            var interfacePrefix = design.BuilderTypeName;
            var leIndex = interfacePrefix.IndexOf('<');
            if (leIndex > 0)
            {
                interfacePrefix = interfacePrefix.Substring(0, leIndex);
            }

            var allNodeNames = steps.Select(x => x.From)
                .Concat(steps.Select(x => x.To))
                .Except(new[] {EdgeNodeName})
                .Distinct()
                .ToArray();

            var interfaceCode = allNodeNames.Select(name => nodeDic[name])
                .Select(node => new StepInterfaceCodeTpl
                {
                    Name = GetStepNodeInterfaceName(interfacePrefix, node.Name),
                    Methods = node.Steps.Select(s => new StepInterfaceMethodCodeTpl
                        {
                            Returning = s.ContainsReturn ? s.Return : GetStepNodeInterfaceName(interfacePrefix, s.To),
                            ArgsList = GetArgsList(s.Action),
                            MethodName = GetMethodName(s.Action)
                        })
                        .Select(x => x.Format())
                        .ToArray()
                })
                .Select(x => x.Format())
                .ToArray();
            var interfaces = allNodeNames
                .Select(x => $",{design.BuilderTypeName}.{GetStepNodeInterfaceName(interfacePrefix, x)}").ToArray();

            var sharedMethods = steps.Where(x => x.ContainsShare)
                .Select(x => new PrivateMethodCodeTpl
                {
                    Returning = x.ContainsReturn ? x.Return : "void",
                    ArgsList = GetArgsList(x.Action),
                    MethodName = GetSharedMethodName(x.Action),
                    Impl = "throw new NotImplementedException();"
                })
                .Select(x => x.Format())
                .Distinct()
                .ToArray();

            var notSharedMethods = steps.Where(x => !x.ContainsShare)
                .Select(x => new PrivateMethodCodeTpl
                {
                    Returning = x.ContainsReturn ? x.Return : "void",
                    ArgsList = GetArgsList(x.Action),
                    MethodName = GetNotSharedMethodName(x.Action),
                    Impl = "throw new NotImplementedException();"
                })
                .Select(x => x.Format())
                .Distinct()
                .ToArray();


            var autoGenerateSteps = steps.Select(x =>
                {
                    var methodName = GetMethodName(x.Action);
                    var methodReturning = x.IsEnd
                        ? x.ContainsReturn ? x.Return : "void"
                        : GetStepNodeInterfaceName(interfacePrefix, x.To);
                    var bodyReturning = x.IsEnd
                        ? x.ContainsReturn ? "return" : ""
                        : GetStepNodeInterfaceName(interfacePrefix, x.To);
                    if (x.IsStart)
                    {
                        return (ICodeTpl) new PublicMethodCodeTpl
                        {
                            MethodName = methodName,
                            Returning = methodReturning,
                            ArgsList = GetArgsList(x.Action),
                            Impl = new StepInterfaceImplMiddleTpl
                            {
                                Returning = bodyReturning,
                                Args = GetArgs(x.Action),
                                MethodName = x.ContainsShare
                                    ? GetSharedMethodName(x.Action)
                                    : GetNotSharedMethodName(x.Action)
                            }.Format()
                        };
                    }

                    var fromInterface = GetStepNodeInterfaceName(interfacePrefix, x.From);

                    var impl =
                        x.IsEnd
                            ? new StepInterfaceImplFinalTpl
                            {
                                Returning = bodyReturning,
                                Args = GetArgs(x.Action),
                                MethodName = x.ContainsShare
                                    ? GetSharedMethodName(x.Action)
                                    : GetNotSharedMethodName(x.Action)
                            }.Format()
                            : new StepInterfaceImplMiddleTpl
                            {
                                Returning = bodyReturning,
                                Args = GetArgs(x.Action),
                                MethodName = x.ContainsShare
                                    ? GetSharedMethodName(x.Action)
                                    : GetNotSharedMethodName(x.Action)
                            }.Format();
                    return (ICodeTpl) new ImplicitMethodCodeTpl
                    {
                        MethodName = methodName,
                        Returning = methodReturning,
                        ArgsList = GetArgsList(x.Action),
                        InterfaceName = fromInterface,
                        Impl = impl
                    };
                })
                .Select(x => x.Format())
                .Concat(interfaceCode)
                .ToArray();
            var constructorName = design.BuilderTypeName;
            var genericStartIndex = constructorName.IndexOf('<');
            if (genericStartIndex >= 0)
            {
                constructorName = constructorName.Substring(0, genericStartIndex);
            }

            var builderImplCodeTemplate = new BuilderImplCodeTpl
            {
                Namespace = design.Namespace,
                ImplClassName = design.BuilderTypeName,
                ImplClassConstructorName = constructorName,
                BuilderContextType = design.BuilderContextType,
                Interfaces = interfaces,
                AutoGenerateMethods = autoGenerateSteps,
                UserImplMethods = sharedMethods.Concat(notSharedMethods).ToArray()
            };
            var re = new FluentApiOutput
            {
                FluentApiDesign = design,
                FluentApiFiles = new FluentApiFiles
                {
                    AutoGenerate = builderImplCodeTemplate.Format(),
                }
            };
            return re;
        }

        private ApiStep[] ReplaceWithMapping(ApiStep[] steps, Dictionary<string, string> mapping)
        {
            var visitor = default(ApiStep)!
                .V()
                .WithExtendObject(mapping)
                .ForEach<ApiStep, Dictionary<string, string>, string>(c => ReplaceValueWithMapping(c),
                    info => info.PropertyType == typeof(string) && info.Name != nameof(ApiStep.SourceContent))
                .CreateVisitor();

            var re = steps.Select(x =>
            {
                var item = new ApiStep
                {
                    Action = x.Action,
                    From = x.From,
                    Return = x.Return,
                    Share = x.Share,
                    To = x.To,
                    SourceContent = x.SourceContent
                };
                visitor.Run(item, mapping);
                return item;
            }).ToArray();
            return re;
        }

        private static void ReplaceValueWithMapping(
            IObjectVisitorContext<ApiStep, Dictionary<string, string>, string> c)
        {
            var source = c.Value;
            foreach (var kv in c.ExtendObject)
            {
                source = source.Replace(kv.Key, kv.Value);
            }

            c.Value = source;
        }


        static string GetArgsList(string action)
        {
            var index = action.IndexOf('(');
            var argsList = action.Substring(index + 1).TrimEnd(')');
            return argsList;
        }

        private static readonly HashSet<char> EnterSkipping = new HashSet<char> {'[', '<', '('};
        private static readonly HashSet<char> ExitSkipping = new HashSet<char> {']', '>', ')'};

        static string GetArgs(string action)
        {
            var argsList = GetArgsList(action);
            if (string.IsNullOrWhiteSpace(argsList))
            {
                return string.Empty;
            }

            var indexList = new List<int>
            {
                0
            };
            var skipping = false;
            for (var i = 0; i < argsList.Length; i++)
            {
                var c = argsList[i];
                if (c == ',' && !skipping)
                {
                    indexList.Add(i);
                }
                else if (EnterSkipping.Contains(c))
                {
                    skipping = true;
                }
                else if (ExitSkipping.Contains(c))
                {
                    skipping = false;
                }
            }

            var args = new List<string>();
            for (int i = 0; i < indexList.Count; i++)
            {
                var index = indexList[i];
                string pSpan;
                if (i == indexList.Count - 1)
                {
                    pSpan = argsList.Substring(index);
                }
                else
                {
                    var nextIndex = indexList[i + 1];
                    pSpan = argsList.Substring(index, nextIndex - index);
                }

                var eqIndex = pSpan.IndexOf('=');
                if (eqIndex > 0)
                {
                    pSpan = pSpan.Substring(0, eqIndex).Trim();
                }

                var spaceIndex = pSpan.LastIndexOf(' ');
                args.Add(pSpan.Substring(spaceIndex));
            }

            var result = string.Join(",", args);
            return result;
        }

        static string GetMethodName(string action)
        {
            var index = action.IndexOf('(');
            return action.Substring(0, index);
        }

        static string GetSharedMethodName(string action)
        {
            return $"Shared_{GetMethodName(action)}";
        }

        static string GetNotSharedMethodName(string action)
        {
            return $"Core_{GetMethodName(action)}";
        }

        static string GetStepNodeInterfaceName(string interfacePrefix, string nodeName)
        {
            var name = Regex.Replace(nodeName, "_(.+)_", "<$1>");
            var re = $"I{interfacePrefix}_{name}";
            return re;
        }

        public class Node
        {
            public string Name { get; set; }
            public List<ApiStep> Steps { get; set; }
        }
    }
}