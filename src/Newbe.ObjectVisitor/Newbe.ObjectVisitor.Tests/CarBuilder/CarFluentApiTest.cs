using System;
using System.IO;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.CarBuilder
{
    [Category("FluentAPI")]
    [Explicit]
    public class CarFluentApiTest
    {
        [Test]
        public void Normal()
        {
            const string mermaid = @"stateDiagram
[*]  --> W1 : AddWheel(int size)
W1 --> W2 : AddWheel(int size)
W2 --> W3 : AddWheel(int size)
W3 --> W4 : AddWheel(int size)

[*] --> E : AddEngine(string engine)
E --> WE1 : AddWheel(int size)
WE1 --> WE2 : AddWheel(int size)
WE2 --> WE3 : AddWheel(int size)
WE3 --> WE4 : AddWheel(int size)

W1 --> WE1 : AddEngine(string engine)
W2 --> WE2 : AddEngine(string engine)
W3 --> WE3 : AddEngine(string engine)
W4 --> WE4 : AddEngine(string engine)

WE4 --> [*] : Build()";

            var fluentApiGenerator = new FluentApiGenerator();
            var input = new FluentApiGenerationInput
            {
                Mermaid = mermaid,
                StateType = "Car",
                FinalResultType = "Car",
                StartObjectType = "CarBuilder",
                Namespace = "Newbe.ObjectVisitor.Tests.CarBuilder",
                StateChangerType = "CarBuilderStateChanger"
            };
            var output = fluentApiGenerator.Create(input);

            var nodesCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "CarBuilder",
                $"{input.FinalResultType}BuilderNodes.cs");
            File.WriteAllText(nodesCs, output.StateNodes);
            Console.WriteLine(output.StateNodes);

            var stateChangerCs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../", "CarBuilder",
                $"{input.StateChangerType}.cs");
            File.WriteAllText(stateChangerCs, output.StateChanger);
            Console.WriteLine(output.StateChanger);
        }
    }
}