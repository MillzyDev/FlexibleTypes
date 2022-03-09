using System;
using System.Collections.Generic;
using System.Text;

namespace TypeUtils.Enumerator
{
    /// <summary>
    /// A custom enumerator that allows you to use any type for the Enumeration
    /// </summary>
    /// <typeparam name="T">The type that should be used for the Enumeration</typeparam>
    /// <example>
    /// <code>
    /// using TupleEnum = TypeUtils.Enumerator.IFlexibleEnumerator<System.Tuple<string, string>>;
    /// using FValue = TypeUtils.Enumerator.IFlexibleEnumerator<System.Tuple<string, string>>.FlexibleValue;
    /// using Value = System.Tuple<string, string>;
    ///
    /// namespace TypeUtils.Tests
    ///{
    ///    internal class Language : TupleEnum
    ///    {
    ///        public static readonly FValue EN = new Value("English", "EN");
    ///        public static readonly FValue FR = new Value("French", "FR");
    ///        public static readonly FValue ES = new Value("Spanish", "ES");
    ///    }
    ///}
    /// </code>
    /// </example>
    public interface FEnum<T>
    {
        /// <summary>
        /// Container class for the enum value
        /// </summary>
        public class FValue
        {
            public static implicit operator T(FValue value)
            {
                return value.Value;
            }

            public static implicit operator FValue(T value)
            {
                return new FValue(value);
            }

            /// <!--NO DOCS-->
            public T Value { get; private set; }

            /// <summary>
            /// Constructs a new enum value
            /// </summary>
            /// <param name="value"></param>
            public FValue(T value) => Value = value;
        }
    }
}
