using System;
using StateChanger = Newbe.ObjectVisitor.Tests.CarBuilder.CarBuilderStateChanger;

namespace Newbe.ObjectVisitor.Tests.CarBuilder
{
    public class CarBuilder
    {
        private readonly Car _state;

        public CarBuilder(Car state)
        {
            _state = state;
        }

        public E AddEngine(string engine)
        {
            StateChanger.Start_E_AddEngine(_state, engine);
            return new E(_state);
        }


        public W1 AddWheel(int size)
        {
            StateChanger.Start_W1_AddWheel(_state, size);
            return new W1(_state);
        }
    }


    public class E
    {
        private readonly Car _state;

        public E(Car state)
        {
            _state = state;
        }

        public WE1 AddWheel(int size)
        {
            StateChanger.E_WE1_AddWheel(_state, size);
            return new WE1(_state);
        }
    }


    public class W1
    {
        private readonly Car _state;

        public W1(Car state)
        {
            _state = state;
        }

        public W2 AddWheel(int size)
        {
            StateChanger.W1_W2_AddWheel(_state, size);
            return new W2(_state);
        }


        public WE1 AddEngine(string engine)
        {
            StateChanger.W1_WE1_AddEngine(_state, engine);
            return new WE1(_state);
        }
    }


    public class W2
    {
        private readonly Car _state;

        public W2(Car state)
        {
            _state = state;
        }

        public W3 AddWheel(int size)
        {
            StateChanger.W2_W3_AddWheel(_state, size);
            return new W3(_state);
        }


        public WE2 AddEngine(string engine)
        {
            StateChanger.W2_WE2_AddEngine(_state, engine);
            return new WE2(_state);
        }
    }


    public class W3
    {
        private readonly Car _state;

        public W3(Car state)
        {
            _state = state;
        }

        public W4 AddWheel(int size)
        {
            StateChanger.W3_W4_AddWheel(_state, size);
            return new W4(_state);
        }


        public WE3 AddEngine(string engine)
        {
            StateChanger.W3_WE3_AddEngine(_state, engine);
            return new WE3(_state);
        }
    }


    public class W4
    {
        private readonly Car _state;

        public W4(Car state)
        {
            _state = state;
        }

        public WE4 AddEngine(string engine)
        {
            StateChanger.W4_WE4_AddEngine(_state, engine);
            return new WE4(_state);
        }
    }


    public class WE1
    {
        private readonly Car _state;

        public WE1(Car state)
        {
            _state = state;
        }

        public WE2 AddWheel(int size)
        {
            StateChanger.WE1_WE2_AddWheel(_state, size);
            return new WE2(_state);
        }
    }


    public class WE2
    {
        private readonly Car _state;

        public WE2(Car state)
        {
            _state = state;
        }

        public WE3 AddWheel(int size)
        {
            StateChanger.WE2_WE3_AddWheel(_state, size);
            return new WE3(_state);
        }
    }


    public class WE3
    {
        private readonly Car _state;

        public WE3(Car state)
        {
            _state = state;
        }

        public WE4 AddWheel(int size)
        {
            StateChanger.WE3_WE4_AddWheel(_state, size);
            return new WE4(_state);
        }
    }


    public class WE4
    {
        private readonly Car _state;

        public WE4(Car state)
        {
            _state = state;
        }

        public Car Build()
        {
            return StateChanger.WE4_End_Build(_state);
        }
    }
}