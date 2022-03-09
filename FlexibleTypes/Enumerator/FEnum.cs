using System;
using System.Collections.Generic;
using System.Text;

namespace FlexibleTypes.Enumerator
{
    /// <summary>
    /// A custom enumerator that allows you to use any type for the Enumeration
    /// </summary>
    /// <typeparam name="T">The type that should be used for the Enumeration</typeparam>
    /// <example>
    /// <code>
    /// using TupleEnum = FlexibleTypes.Enumerator.IFlexibleEnumerator<System.Tuple<string, string>>;
    /// using FValue = FlexibleTypes.Enumerator.IFlexibleEnumerator<System.Tuple<string, string>>.FlexibleValue;
    /// using Value = System.Tuple<string, string>;
    ///
    /// namespace FlexibleTypes.Tests
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
            /// <summary>
            /// Implicitly converts <see cref="FValue"/> to <typeparamref name="T"/>
            /// </summary>
            public static implicit operator T(FValue value)
            {
                return value.Value;
            }

            /// <summary>
            /// Implicitly converts <typeparamref name="T"/> to <see cref="FValue"/>
            /// </summary>
            public static implicit operator FValue(T value)
            {
                return new FValue(value);
            }

            /// <!--NO DOCS-->
            public T Value { get; private set; }

            /// <summary>
            /// Constructs a new enum value
            /// </summary>
            public FValue(T value) => Value = value;
        }
    }
}
