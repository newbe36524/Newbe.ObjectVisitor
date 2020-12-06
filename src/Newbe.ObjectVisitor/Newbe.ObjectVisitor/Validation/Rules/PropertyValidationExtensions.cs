using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newbe.ObjectVisitor.Validation;

// ReSharper disable once CheckNamespace
namespace Newbe.ObjectVisitor.Validator
{
    /// <summary>
    /// Extensions for property validation
    /// </summary>
    public static class PropertyValidationExtensions
    {
        #region Equal

        /// <summary>
        /// Value should equals to <paramref name="expected"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expected">Value of expected</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Equal<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected)
            where TValue : IEquatable<TValue>
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should equals to <paramref name="expected"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expected">Value of expected</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            Equal<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue? expected)
            where TValue : struct, IEquatable<TValue>
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should equals to <paramref name="expected"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expected">Value of expected</param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Equal<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected,
                IEqualityComparer<TValue> comparer)
        {
            var rule = EqualRuleFactory.Create<T, TValue>(expected, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region NotEqual

        /// <summary>
        /// Value should not equal to <paramref name="expected"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expected">Value of expected</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected)
            where TValue : IEquatable<TValue>
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should not equal to <paramref name="expected"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expected">Value of expected</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            NotEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue? expected)
            where TValue : struct, IEquatable<TValue>
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should not equal to <paramref name="expected"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expected">Value of expected</param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue expected,
                IEqualityComparer<TValue> comparer)
        {
            var rule = NotEqualRuleFactory.Create<T, TValue>(expected, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region LessThan

        /// <summary>
        /// Value should be less than <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max">Max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = LessThanRuleFactory.Create<T, TValue>(max);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be less than <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max">Max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            LessThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue max)
            where TValue : struct, IComparable<TValue>
        {
            var rule = LessThanRuleFactory.CreateNullable<T, TValue>(max);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be less than <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max">Max value</param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max,
                IComparer<TValue> comparer)
        {
            var rule = LessThanRuleFactory.Create<T, TValue>(max, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region LessThanOrEqual

        /// <summary>
        /// Value should be less than or equal <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max">Max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max)
            where TValue : IComparable<TValue>
        {
            var rule = LessThanOrEqualRuleFactory.Create<T, TValue>(max);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be less than or equal <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max">Max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            LessThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue max)
            where TValue : struct, IComparable<TValue>
        {
            var rule = LessThanOrEqualRuleFactory.CreateNullable<T, TValue>(max);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be less than or equal <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max">Max value</param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            LessThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue max,
                IComparer<TValue> comparer)
        {
            var rule = LessThanOrEqualRuleFactory.Create<T, TValue>(max, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region GreaterThan

        /// <summary>
        /// Value should be greater than <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min)
            where TValue : IComparable<TValue>
        {
            var rule = GreaterThanRuleFactory.Create<T, TValue>(min);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be greater than <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            GreaterThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue min)
            where TValue : struct, IComparable<TValue>
        {
            var rule = GreaterThanRuleFactory.CreateNullable<T, TValue>(min);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be greater than <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThan<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                IComparer<TValue> comparer)
        {
            var rule = GreaterThanRuleFactory.Create<T, TValue>(min, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region GreaterThanOrEqual

        /// <summary>
        /// Value should be greater than or equal to <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min)
            where TValue : IComparable<TValue>
        {
            var rule = GreaterThanOrEqualRuleFactory.Create<T, TValue>(min);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be greater than or equal to <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            GreaterThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue min)
            where TValue : struct, IComparable<TValue>
        {
            var rule = GreaterThanOrEqualRuleFactory.CreateNullable<T, TValue>(min);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be greater than or equal to <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            GreaterThanOrEqual<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                IComparer<TValue> comparer)
        {
            var rule = GreaterThanOrEqualRuleFactory.Create<T, TValue>(min, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region IsInRange

        /// <summary>
        /// Value should be between <paramref name="min"/> and <paramref name="max"/>.
        /// You can specify <paramref name="excludeMin"/> and <paramref name="excludeMax"/> to include min and max or not. Default range is [min,max).
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <param name="excludeMin">Exclude min value</param>
        /// <param name="excludeMax">Exclude max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInRange<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                TValue max,
                bool excludeMin = false,
                bool excludeMax = true)
            where TValue : IComparable<TValue>
        {
            var rule = IsInRangeRuleFactory.Create<T, TValue>(min, max, excludeMin, excludeMax);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be between <paramref name="min"/> and <paramref name="max"/>.
        /// You can specify <paramref name="excludeMin"/> and <paramref name="excludeMax"/> to include min and max or not. Default range is [min,max).
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <param name="excludeMin">Exclude min value</param>
        /// <param name="excludeMax">Exclude max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            IsInRange<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                TValue max,
                bool excludeMin = false,
                bool excludeMax = true)
            where TValue : struct, IComparable<TValue>
        {
            var rule = IsInRangeRuleFactory.CreateNullable<T, TValue>(min, max, excludeMin, excludeMax);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be between <paramref name="min"/> and <paramref name="max"/>.
        /// You can specify <paramref name="excludeMin"/> and <paramref name="excludeMax"/> to include min and max or not. Default range is [min,max).
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min">Min value</param>
        /// <param name="max">Max value</param>
        /// <param name="comparer">Value comparer</param>
        /// <param name="excludeMin">Exclude min value</param>
        /// <param name="excludeMax">Exclude max value</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInRange<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                TValue min,
                TValue max,
                IComparer<TValue> comparer,
                bool excludeMin = false,
                bool excludeMax = true)
        {
            var rule = IsInRangeRuleFactory.Create<T, TValue>(min, max, excludeMin, excludeMax, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region IsInSet

        /// <summary>
        /// Value should be in a range specified in <paramref name="expectedSet"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expectedSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = IsInSetRuleFactory.Create<T, TValue>(expectedSet);
            return step.Validate(rule);
        }


        /// <summary>
        /// Value should be in a range specified in <paramref name="expectedSet"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expectedSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            IsInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue?> expectedSet)
            where TValue : struct, IEquatable<TValue>
        {
            var rule = IsInSetRuleFactory.CreateNullable<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be in a range specified in <paramref name="expectedSet"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expectedSet"></param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet,
                IEqualityComparer<TValue> comparer)
        {
            var rule = IsInSetRuleFactory.Create<T, TValue>(expectedSet, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region IsNotInSet

        /// <summary>
        /// Value should not be in a range specified in <paramref name="expectedSet"/>. It is like a negation of <see cref="IsInSet{T,TValue}(Newbe.ObjectVisitor.Validation.PropertyValidationRuleBuilder{T,TValue}.IPropertyValidationRuleBuilder_S,System.Collections.Generic.IEnumerable{TValue})"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expectedSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsNotInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet)
            where TValue : IEquatable<TValue>
        {
            var rule = IsNotInSetRuleFactory.Create<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should not be in a range specified in <paramref name="expectedSet"/>. It is like a negation of <see cref="IsInSet{T,TValue}(Newbe.ObjectVisitor.Validation.PropertyValidationRuleBuilder{T,TValue}.IPropertyValidationRuleBuilder_S,System.Collections.Generic.IEnumerable{TValue})"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expectedSet"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            IsNotInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue?> expectedSet)
            where TValue : struct, IEquatable<TValue>
        {
            var rule = IsNotInSetRuleFactory.CreateNullable<T, TValue>(expectedSet);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should not be in a range specified in <paramref name="expectedSet"/>. It is like a negation of <see cref="IsInSet{T,TValue}(Newbe.ObjectVisitor.Validation.PropertyValidationRuleBuilder{T,TValue}.IPropertyValidationRuleBuilder_S,System.Collections.Generic.IEnumerable{TValue})"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="expectedSet"></param>
        /// <param name="comparer">Value comparer</param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsNotInSet<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                IEnumerable<TValue> expectedSet,
                IEqualityComparer<TValue> comparer)
        {
            var rule = IsNotInSetRuleFactory.Create<T, TValue>(expectedSet, comparer);
            return step.Validate(rule);
        }

        #endregion

        #region Null

        /// <summary>
        /// Value should be null
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Null<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : class
        {
            var rule = new ClassNullRule<T, TValue>();
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be null
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            Null<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step)
            where TValue : struct
        {
            var rule = new NullableNullRule<T, TValue>();
            return step.Validate(rule);
        }

        #endregion

        #region NotNull

        /// <summary>
        /// Value should not be null
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotNull<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : class
        {
            var rule = new ClassNotNullRule<T, TValue>();
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should not be null
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S
            NotNull<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue?>.IPropertyValidationRuleBuilder_S step)
            where TValue : struct
        {
            var rule = new NullableNotNullRule<T, TValue>();
            return step.Validate(rule);
        }

        #endregion

        #region NotEmpty

        /// <summary>
        /// Value should not be null or white space
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            NotEmpty<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step)
        {
            var rule = new StringNotEmptyRule<T>();
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should contains one element at least.
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            NotEmpty<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : IEnumerable
        {
            var rule = new EnumerableNotEmptyRule<T, TValue>();
            return step.Validate(rule);
        }

        #endregion


        #region Empty

        /// <summary>
        /// Value should be null or white space
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            Empty<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step)
        {
            var rule = new StringEmptyRule<T>();
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should contains no element.
        /// </summary>
        /// <param name="step"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Empty<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step)
            where TValue : IEnumerable
        {
            var rule = new EnumerableEmptyRule<T, TValue>();
            return step.Validate(rule);
        }

        #endregion

        #region MatchRegex

        /// <summary>
        /// Value should match regex success.
        /// </summary>
        /// <param name="step"></param>
        /// <param name="pattern">Pattern of regex. It will create a new regex with this pattern by <see cref="GlobalFactories.Validation.RegexFactory"/></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            MatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                string pattern)
        {
            var rule = MatchRegexRuleFactory.Create<T>(pattern);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should match regex success
        /// </summary>
        /// <param name="step"></param>
        /// <param name="regex"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            MatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                Regex regex)
        {
            var rule = MatchRegexRuleFactory.Create<T>(regex);
            return step.Validate(rule);
        }

        #endregion

        #region NotMatchRegex

        /// <summary>
        /// Value should not match regex success
        /// </summary>
        /// <param name="step"></param>
        /// <param name="pattern">Pattern of regex. It will create a new regex with this pattern by <see cref="GlobalFactories.Validation.RegexFactory"/></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            NotMatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                string pattern)
        {
            var rule = NotMatchRegexRuleFactory.Create<T>(pattern);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should not match regex success
        /// </summary>
        /// <param name="step"></param>
        /// <param name="regex"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            NotMatchRegex<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                Regex regex)
        {
            var rule = NotMatchRegexRuleFactory.Create<T>(regex);
            return step.Validate(rule);
        }

        #endregion

        /// <summary>
        /// Count of element in value should be in range [min,max]
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Length<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int min,
                int max)
            where TValue : IEnumerable
        {
            var rule = new LengthRule<T, TValue>(min, max);
            return step.Validate(rule);
        }

        /// <summary>
        /// Count of element in value should be greater than or equal to <paramref name="min"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="min"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            MinLength<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int min)
            where TValue : IEnumerable
        {
            var rule = new MinLengthRule<T, TValue>(min);
            return step.Validate(rule);
        }

        /// <summary>
        /// Count of element in value should be less than or equal to <paramref name="max"/>
        /// </summary>
        /// <param name="step"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            MaxLength<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int max)
            where TValue : IEnumerable
        {
            var rule = new MaxLengthRule<T, TValue>(max);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be in range of <paramref name="enumType"/> definition. 
        /// </summary>
        /// <param name="step"></param>
        /// <param name="enumType"></param>
        /// <param name="flagged"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInEnum<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                Type enumType,
                bool? flagged = null)
        {
            var rule = IsInEnumRuleFactory.Create<T, TValue>(enumType,
                flagged);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be in range of <typeparamref name="TValue"/> definition. 
        /// </summary>
        /// <param name="step"></param>
        /// <param name="flagged"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            IsInEnum<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                bool? flagged = null)
        {
            var rule = IsInEnumRuleFactory.Create<T, TValue>(typeof(TValue),
                flagged);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be in range of <paramref name="enumType"/> definition. 
        /// </summary>
        /// <param name="step"></param>
        /// <param name="enumType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S
            IsInEnumName<T>(
                this PropertyValidationRuleBuilder<T, string>.IPropertyValidationRuleBuilder_S step,
                Type enumType)
        {
            var rule = IsInEnumRuleFactory.Create<T, string>(enumType, false);
            return step.Validate(rule);
        }

        /// <summary>
        /// Value should be less than <paramref name="precision"/> digits in total with allowance for <paramref name="scale"/>} decimals 
        /// </summary>
        /// <param name="step"></param>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            ScalePrecision<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                int scale,
                int precision)
            where TValue : struct
        {
            var rule = ScalePrecisionRuleFactory.Create<T, TValue>(scale, precision);
            return step.Validate(rule);
        }

        /// <summary>
        /// Create a new validation group which contains one or move rules. Validation success if any of rule success in this group.
        /// </summary>
        /// <param name="step"></param>
        /// <param name="ruleFactories"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Or<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                params Func<PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S,
                        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S>[]
                    ruleFactories)
        {
            var builder = new ValidationRuleGroupBuilder<T>(step.Build());
            var anyBuilder = builder.GetBuilder(ValidationRuleRelation.Or);
            var pBuilder =
                new PropertyValidationRuleBuilder<T, TValue>(anyBuilder).GetBuilder(step.GetPropertyExpression());
            foreach (var func in ruleFactories)
            {
                pBuilder = func.Invoke(pBuilder);
            }

            _ = pBuilder.Build();
            return step;
        }

        /// <summary>
        /// Create a negation of inner validation rule
        /// </summary>
        /// <param name="step"></param>
        /// <param name="notFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S
            Not<T, TValue>(
                this PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S step,
                Func<PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S,
                        PropertyValidationRuleBuilder<T, TValue>.IPropertyValidationRuleBuilder_S>
                    notFunc)
        {
            var builder = new ValidationRuleGroupBuilder<T>(step.Build());
            var anyBuilder = builder.GetBuilder(ValidationRuleRelation.Not);
            var pBuilder =
                new PropertyValidationRuleBuilder<T, TValue>(anyBuilder).GetBuilder(step.GetPropertyExpression());
            pBuilder = notFunc.Invoke(pBuilder).Next();
            _ = pBuilder.Build();
            return step;
        }
    }
}