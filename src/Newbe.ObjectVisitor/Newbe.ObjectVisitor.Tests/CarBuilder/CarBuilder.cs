using System;

namespace Newbe.ObjectVisitor.Tests.CarBuilder
{
    public class CarBuilder : Newbe.ObjectVisitor.IFluentApi
        , CarBuilder.ICarBuilder_W1
        , CarBuilder.ICarBuilder_W2
        , CarBuilder.ICarBuilder_W3
        , CarBuilder.ICarBuilder_E
        , CarBuilder.ICarBuilder_WE1
        , CarBuilder.ICarBuilder_WE2
        , CarBuilder.ICarBuilder_WE3
        , CarBuilder.ICarBuilder_W4
        , CarBuilder.ICarBuilder_WE4
    {
        private readonly Car _context;

        public CarBuilder(Car context)
        {
            _context = context;
        }

        #region UserImpl

        private void Shared_AddWheel(int size)
        {
            if (_context.Wheel1 == 0)
            {
                _context.Wheel1 = size;
                return;
            }

            if (_context.Wheel2 == 0)
            {
                _context.Wheel2 = size;
                return;
            }

            if (_context.Wheel3 == 0)
            {
                _context.Wheel3 = size;
                return;
            }

            if (_context.Wheel4 == 0)
            {
                _context.Wheel4 = size;
                return;
            }
        }


        private void Shared_AddEngine(string engine)
        {
            _context.Engine = engine;
        }


        private Car Core_Build()
        {
            return _context;
        }

        #endregion

        #region AutoGenerate

        public ICarBuilder_W1 AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_W1) this;
        }


        ICarBuilder_W2 ICarBuilder_W1.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_W2) this;
        }


        ICarBuilder_W3 ICarBuilder_W2.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_W3) this;
        }


        ICarBuilder_W4 ICarBuilder_W3.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_W4) this;
        }


        public ICarBuilder_E AddEngine(string engine)
        {
            Shared_AddEngine(engine);
            return (ICarBuilder_E) this;
        }


        ICarBuilder_WE1 ICarBuilder_E.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_WE1) this;
        }


        ICarBuilder_WE2 ICarBuilder_WE1.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_WE2) this;
        }


        ICarBuilder_WE3 ICarBuilder_WE2.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_WE3) this;
        }


        ICarBuilder_WE4 ICarBuilder_WE3.AddWheel(int size)
        {
            Shared_AddWheel(size);
            return (ICarBuilder_WE4) this;
        }


        ICarBuilder_WE1 ICarBuilder_W1.AddEngine(string engine)
        {
            Shared_AddEngine(engine);
            return (ICarBuilder_WE1) this;
        }


        ICarBuilder_WE2 ICarBuilder_W2.AddEngine(string engine)
        {
            Shared_AddEngine(engine);
            return (ICarBuilder_WE2) this;
        }


        ICarBuilder_WE3 ICarBuilder_W3.AddEngine(string engine)
        {
            Shared_AddEngine(engine);
            return (ICarBuilder_WE3) this;
        }


        ICarBuilder_WE4 ICarBuilder_W4.AddEngine(string engine)
        {
            Shared_AddEngine(engine);
            return (ICarBuilder_WE4) this;
        }


        Car ICarBuilder_WE4.Build()
        {
            return Core_Build();
        }


        public interface ICarBuilder_W1
        {
            ICarBuilder_W2 AddWheel(int size);


            ICarBuilder_WE1 AddEngine(string engine);
        }


        public interface ICarBuilder_W2
        {
            ICarBuilder_W3 AddWheel(int size);


            ICarBuilder_WE2 AddEngine(string engine);
        }


        public interface ICarBuilder_W3
        {
            ICarBuilder_W4 AddWheel(int size);


            ICarBuilder_WE3 AddEngine(string engine);
        }


        public interface ICarBuilder_E
        {
            ICarBuilder_WE1 AddWheel(int size);
        }


        public interface ICarBuilder_WE1
        {
            ICarBuilder_WE2 AddWheel(int size);
        }


        public interface ICarBuilder_WE2
        {
            ICarBuilder_WE3 AddWheel(int size);
        }


        public interface ICarBuilder_WE3
        {
            ICarBuilder_WE4 AddWheel(int size);
        }


        public interface ICarBuilder_W4
        {
            ICarBuilder_WE4 AddEngine(string engine);
        }


        public interface ICarBuilder_WE4
        {
            Car Build();
        }

        #endregion
    }
}