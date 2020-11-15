using System;
using System.Collections.Generic;
using System.Linq;

namespace Newbe.ObjectVisitor.Tests.SumBuilderFluentApi
{
    public class SumBuilder : Newbe.ObjectVisitor.IFluentApi
        , SumBuilder.ISumBuilder_AddNumber
    {
        private readonly List<int> _context;

        public SumBuilder(List<int> context)
        {
            _context = context;
        }

        #region UserImpl

        private void Core_AddNumber(int number)
        {
            _context.Add(number);
        }


        private int Core_Sum()
        {
            return _context.Sum();
        }

        #endregion

        #region AutoGenerate

        public ISumBuilder_AddNumber AddNumber(int number)
        {
            Core_AddNumber(number);
            return (ISumBuilder_AddNumber) this;
        }


        ISumBuilder_AddNumber ISumBuilder_AddNumber.AddNumber(int number)
        {
            Core_AddNumber(number);
            return (ISumBuilder_AddNumber) this;
        }


        int ISumBuilder_AddNumber.Sum()
        {
            return Core_Sum();
        }


        public interface ISumBuilder_AddNumber
        {
            ISumBuilder_AddNumber AddNumber(int number);


            int Sum();
        }

        #endregion
    }
}