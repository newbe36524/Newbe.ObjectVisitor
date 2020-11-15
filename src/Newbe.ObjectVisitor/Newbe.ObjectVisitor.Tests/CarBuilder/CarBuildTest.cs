using FluentAssertions;
using NUnit.Framework;

namespace Newbe.ObjectVisitor.Tests.CarBuilder
{
    public class CarBuildTest
    {
        [Test]
        public void EngineFirst()
        {
            var builder = new CarBuilder(new Car());
            var car = builder
                .AddEngine("yueluoe")
                .AddWheel(1)
                .AddWheel(2)
                .AddWheel(3)
                .AddWheel(4)
                .Build();
            AssertCar(car);
        }

        [Test]
        public void EngineLast()
        {
            var builder = new CarBuilder(new Car());
            var car = builder
                .AddWheel(1)
                .AddWheel(2)
                .AddWheel(3)
                .AddWheel(4)
                .AddEngine("yueluoe")
                .Build();
            AssertCar(car);
        }

        [Test]
        public void EngineMiddle()
        {
            var builder = new CarBuilder(new Car());
            var car = builder
                .AddWheel(1)
                .AddWheel(2)
                .AddEngine("yueluoe")
                .AddWheel(3)
                .AddWheel(4)
                .Build();
            AssertCar(car);
        }

        private static void AssertCar(Car car)
        {
            var expected = new Car
            {
                Engine = "yueluoe",
                Wheel1 = 1,
                Wheel2 = 2,
                Wheel3 = 3,
                Wheel4 = 4
            };
            car.Should().BeEquivalentTo(expected);
        }
    }
}