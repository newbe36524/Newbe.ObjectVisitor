using System.Collections.Generic;

namespace Newbe.ObjectVisitor
{
    internal class OvFactory : IOvFactory
    {
        public static IOvFactory Instance { get; } = Create();

        private readonly IEnumerable<IBuildContextHandler> _handlers;

        private OvFactory(IEnumerable<IBuildContextHandler> handlers)
        {
            _handlers = handlers;
        }

        public IObjectVisitor Create(IOvBuilderContext builderContext)
        {
            foreach (var handler in _handlers)
            {
                var exp = handler.CreateExpression(builderContext);
                if (exp != null)
                {
                    return new ObjectVisitor(exp!);
                }
            }

            throw new MissingBuilderContextHandlerException();
        }

        private static OvFactory Create()
        {
            return new OvFactory(new[]
            {
                new ForEachActionBuildContextHandler(),
            });
        }
    }
}