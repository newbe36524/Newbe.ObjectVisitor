using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests
{
    public class OvFactoryTest
    {
        [Test]
        public void Normal()
        {
            Assert.Throws<MissingBuilderContextHandlerException>(() =>
            {
                var _ = OvFactory.Instance.Create(new OvBuilderContext());
            });
        }
    }
}