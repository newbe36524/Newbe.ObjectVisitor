using System;

namespace Newbe.ObjectVisitor.Tests.CarBuilder
{
    public static class CarBuilderStateChanger
    {
        public static void Start_E_AddEngine(Car state, string engine)
        {
            state.Engine = engine;
        }


        public static void Start_W1_AddWheel(Car state, int size)
        {
            state.Wheel1 = size;
        }


        public static void E_WE1_AddWheel(Car state, int size)
        {
            state.Wheel1 = size;
        }


        public static void W1_W2_AddWheel(Car state, int size)
        {
            state.Wheel2 = size;
        }


        public static void W1_WE1_AddEngine(Car state, string engine)
        {
            state.Engine = engine;
        }


        public static void W2_W3_AddWheel(Car state, int size)
        {
            state.Wheel3 = size;
        }


        public static void W2_WE2_AddEngine(Car state, string engine)
        {
            state.Engine = engine;
        }


        public static void W3_W4_AddWheel(Car state, int size)
        {
            state.Wheel4 = size;
        }


        public static void W3_WE3_AddEngine(Car state, string engine)
        {
            state.Engine = engine;
        }


        public static void W4_WE4_AddEngine(Car state, string engine)
        {
            state.Engine = engine;
        }


        public static void WE1_WE2_AddWheel(Car state, int size)
        {
            state.Wheel2 = size;
        }


        public static void WE2_WE3_AddWheel(Car state, int size)
        {
            state.Wheel3 = size;
        }


        public static void WE3_WE4_AddWheel(Car state, int size)
        {
            state.Wheel4 = size;
        }


        public static Car WE4_End_Build(Car state)
        {
            return state;
        }
    }
}