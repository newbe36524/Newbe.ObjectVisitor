using System;
using System.Collections.Generic;
using System.Linq;

namespace Newbe.ObjectVisitor.Tests.SumBuilderFluentApi
{
    public class MultipleSumBuilder : Newbe.ObjectVisitor.IFluentApi
        , MultipleSumBuilder.IMultipleSumBuilder_AddNumber
    {
        private readonly List<List<int>> _context;
        private int nowIndex = 0;

        public MultipleSumBuilder(List<List<int>> context)
        {
            _context = context;
        }

        #region UserImpl

        private void Core_AddNumber(int number)
        {
            if (_context.Count <= nowIndex)
            {
                _context.Add(new List<int>());
            }

            _context[nowIndex].Add(number);
        }


        private void Core_NextFactor()
        {
            nowIndex++;
            _context.Add(new List<int>());
        }


        private int Core_Sum()
        {
            var re = 1;
            foreach (var list in _context)
            {
                var factor = list.Sum();
                re *= factor;
            }

            return re;
        }

        #endregion

        #region AutoGenerate

        public IMultipleSumBuilder_AddNumber AddNumber(int number)
        {
            Core_AddNumber(number);
            return (IMultipleSumBuilder_AddNumber) this;
        }


        IMultipleSumBuilder_AddNumber IMultipleSumBuilder_AddNumber.AddNumber(int number)
        {
            Core_AddNumber(number);
            return (IMultipleSumBuilder_AddNumber) this;
        }


        IMultipleSumBuilder_AddNumber IMultipleSumBuilder_AddNumber.NextFactor()
        {
            Core_NextFactor();
            return (IMultipleSumBuilder_AddNumber) this;
        }


        int IMultipleSumBuilder_AddNumber.Sum()
        {
            return Core_Sum();
        }


        public interface IMultipleSumBuilder_AddNumber
        {
            IMultipleSumBuilder_AddNumber AddNumber(int number);


            IMultipleSumBuilder_AddNumber NextFactor();


            int Sum();
        }

        #endregion
    }
}