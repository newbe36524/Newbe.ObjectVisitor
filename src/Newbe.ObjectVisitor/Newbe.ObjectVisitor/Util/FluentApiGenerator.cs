using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Newbe.ObjectVisitor
{
    public class FluentApiGenerator : IFluentApiGenerator
    {
        public const string EdgeNode = "[*]";

        public const string StateChangerClassTpl = @"
public static class [StateChangerName]
{
    [Methods]
}";

        public const string VoidStateChangerMethodTpl = @"
public static void [ChangeStateMethodName]([StateType] state[ArgsList])
{
    throw new NotImplementedException();
}
";

        public const string ResultStateChangerMethodTpl = @"
public static [FinalResultType] [ChangeStateMethodName]([StateType] state[ArgsList])
{
    throw new NotImplementedException();
}
";

        public const string ClassTpl = @"
public class [Name]
{
    private readonly [StateType] _state;
    public [Name]([StateType] state)
    {
        _state = state;
    }
    [Methods]
}
";

        public const string MethodTpl = @"
public [Return] [Method]
{
    StateChanger.[ChangeStateMethodName](_state[Args]);
    return new [Return](_state);
}
";

        public const string VoidFinalMethodTpl = @"
public void [Method]
{
    StateChanger.[ChangeStateMethodName](_state[Args]);
}
";

        public const string ResultFinalMethodTpl = @"
public [FinalResultType] [Method]
{
    return StateChanger.[ChangeStateMethodName](_state[Args]);
}
";

        public FluentApiGenerationOutput Create(FluentApiGenerationInput input)
        {
            using var stringReader = new StringReader(input.Mermaid);
            var list = new List<LineItem>();
            string? nowLine;
            do
            {
                nowLine = stringReader.ReadLine();
                if (!string.IsNullOrEmpty(nowLine))
                {
                    var strings = nowLine!.Split(":");
                    if (strings.Length != 2)
                    {
                        continue;
                    }

                    var action = strings[1].Trim();
                    var direction = strings[0].Trim();
                    var nodes = direction.Split("-->");
                    var from = nodes[0].Trim();
                    var to = nodes[1].Trim();
                    var item = new LineItem
                    {
                        Action = action,
                        From = from,
                        To = to
                    };
                    list.Add(item);
                }
            } while (nowLine != null);

            var ordered = list.OrderByDescending(x => x.IsStart)
                .ThenBy(x => x.From)
                .ThenBy(x => x.IsEnd)
                .ThenBy(x => x.To);

            var nodeDic = new Dictionary<string, Node>();
            foreach (var lineItem in ordered)
            {
                var key = lineItem.From;
                if (!nodeDic.TryGetValue(key, out var node))
                {
                    node = new Node {Name = lineItem.From, Actions = new List<LineItem>()};
                }

                node.Actions.Add(lineItem);
                nodeDic[key] = node;
            }

            var staticMethodsList = new List<string>();
            var nodeClasses = new List<string>();
            const string voidTypeString = "void";

            foreach (var nodeKv in nodeDic)
            {
                var k = nodeKv.Key;
                var v = nodeKv.Value;
                var className = k == EdgeNode
                    ? GetClassName(input.StartObjectType)
                    : GetClassName(k);
                var methods = v.Actions
                    .Select(CreateMethodBlock)
                    .ToArray();
                var newClass = ClassTpl
                    .Replace("[Name]", className)
                    .Replace("[Methods]", string.Join(Environment.NewLine, methods))
                    .Replace("[StateType]", input.StateType);
                nodeClasses.Add(newClass);

                var staticMethods = v.Actions
                    .Select(x => CreateStaticMethodBlock(x, input.StateType, input.FinalResultType))
                    .ToArray();
                staticMethodsList.AddRange(staticMethods);
            }

            var stateChanger = StateChangerClassTpl
                .Replace("[StateChangerName]", input.StateChangerType)
                .Replace("[Methods]", string.Join(Environment.NewLine, staticMethodsList));

            var re = new FluentApiGenerationOutput
            {
                StateChanger = string.Join(Environment.NewLine, StateChangerBlocks()),
                StateNodes = string.Join(Environment.NewLine, StateNodeCsBlocks()),
            };
            return re;

            IEnumerable<string> StateChangerBlocks()
            {
                yield return "using System;";
                yield return $"namespace {input.Namespace}";
                yield return "{";
                yield return stateChanger;
                yield return "}";
            }

            IEnumerable<string> StateNodeCsBlocks()
            {
                yield return "using System;";
                yield return $"using StateChanger = {input.Namespace}.{input.StateChangerType};";
                yield return $"namespace {input.Namespace}";
                yield return "{";
                foreach (var nodeClass in nodeClasses)
                {
                    yield return nodeClass;
                }

                yield return "}";
            }

            string CreateMethodBlock(LineItem x)
            {
                if (!x.IsEnd)
                {
                    return MethodTpl
                        .Replace("[Return]", x.To == EdgeNode
                            ? input.FinalResultType
                            : GetClassName(x.To)
                        )
                        .Replace("[Method]", x.Action)
                        .Replace("[Args]", GetArgs(x.Action))
                        .Replace("[ChangeStateMethodName]", GetMovingMethodName(x));
                }

                if (input.FinalResultType == voidTypeString)
                {
                    return VoidFinalMethodTpl
                        .Replace("[Method]", x.Action)
                        .Replace("[Args]", GetArgs(x.Action))
                        .Replace("[ChangeStateMethodName]", GetMovingMethodName(x));
                }
                else
                {
                    return ResultFinalMethodTpl
                        .Replace("[FinalResultType]", input.FinalResultType)
                        .Replace("[Method]", x.Action)
                        .Replace("[Args]", GetArgs(x.Action))
                        .Replace("[ChangeStateMethodName]", GetMovingMethodName(x));
                }
            }

            static string CreateStaticMethodBlock(LineItem x, string stateType, string finalResultType)
            {
                var argsList = GetArgsList(x.Action);
                if (!string.IsNullOrEmpty(argsList))
                {
                    argsList = $",{argsList}";
                }

                if (!x.IsEnd || finalResultType == voidTypeString)
                {
                    return VoidStateChangerMethodTpl
                        .Replace("[ChangeStateMethodName]", GetMovingMethodName(x))
                        .Replace("[StateType]", stateType)
                        .Replace("[ArgsList]", argsList);
                }

                return ResultStateChangerMethodTpl
                    .Replace("[FinalResultType]", finalResultType)
                    .Replace("[ChangeStateMethodName]", GetMovingMethodName(x))
                    .Replace("[StateType]", stateType)
                    .Replace("[ArgsList]", argsList);
            }

            static string GetClassName(string nodeName)
            {
                return nodeName;
            }

            static string GetArgsList(string action)
            {
                var index = action.IndexOf('(');
                var argsList = action.Substring(index + 1).TrimEnd(')');
                return argsList;
            }

            static string GetArgs(string action)
            {
                var argsList = GetArgsList(action);
                if (string.IsNullOrWhiteSpace(argsList))
                {
                    return string.Empty;
                }

                var args = argsList.Split(',')
                    .Select(s => $",{s.Split(" ").Last()}");
                var result = string.Join("", args);
                return result;
            }

            static string GetMovingMethodName(LineItem item)
            {
                var from = item.IsStart ? "Start" : Clean(item.From);
                var to = item.IsEnd ? "End" : Clean(item.To);
                return $"{from}_{to}_{GetMethodName(item.Action)}";

                static string Clean(string name)
                {
                    return name.Replace("<", "_")
                        .Replace(">", "_");
                }
            }


            static string GetMethodName(string action)
            {
                var index = action.IndexOf('(');
                return action.Substring(0, index);
            }
        }

        public class LineItem
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Action { get; set; }
            public bool IsStart => From == "[*]";
            public bool IsEnd => To == "[*]";

            public override string ToString()
            {
                return $"{From} --> {To} : {Action}";
            }
        }

        public class Node
        {
            public string Name { get; set; }
            public List<LineItem> Actions { get; set; }
        }
    }
}