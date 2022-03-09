using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace FlexibleTypes.Integer
{
    /// <summary>
    /// Flexible 64 bit Integer. FInt16 or fint
    /// <para><see cref="short"/> implementation where the <see cref="MinValue"/> and <see cref="MaxValue"/> are not constant.</para>
    /// <para>If the value receeds or exceeds <see cref="MinValue"/> or <see cref="MaxValue"/> then it will wrap back around.</para>
    /// </summary>
    /// <example>
    /// If the max value is <c>5</c> and the min value is <c>0</c>: When the <see cref="FInt16"/> instance increments past 5, it will become 0.
    /// If the <see cref="FInt32"/> instance is set to a value beyond the maximum value, the value will be the maximum value, the same is true for any value before the minimum value
    /// <code>
    /// using FlexibleTypes.Number;
    /// using fshort = FInt16
    /// ...
    /// fint i; // MaxValue is short.MaxValue and MinValue is short.MinValue by default.
    /// i.MaxValue = 5;
    /// i.MinValue = 0;
    /// 
    /// /*    or    */
    ///     
    /// fint i = new fint(0, min: 0, max: 5);
    /// 
    /// i = 5;
    /// i++; // Will wrap around and become 0
    /// </code>
    /// </example>
    public struct FInt16 : IComparable, IComparable<FInt32>, IConvertible, IEquatable<FInt32>, IFormattable
    {
        /// <summary>
        /// <see cref="FInt32"/> instance where <see cref="Value"/> is <c>0</c>, <see cref="MaxValue"/> is <see cref="int.MaxValue"/> and <see cref="MinValue"/> is <see cref="int.MinValue"/>
        /// </summary>
        public static FInt32 Default
        {
            get => new FInt32(0);
        }

        private short _value;
        private short Value
        {
            get => _value;
            set
            {
                if (value > MaxValue) _value = MaxValue;
                if (value < MinValue) _value = MinValue;
                _value = value;
            }
        }

        /// <summary>
        /// The maximum value the value can be
        /// </summary>
        public short MaxValue { get; set; }

        /// <summary>
        /// The minimum value the value can be
        /// </summary>
        public short MinValue { get; set; }

        public FInt16(short value, short min = short.MinValue, short max = short.MaxValue)
        {
            (MaxValue, MinValue) = (max, min);
            if (value > MaxValue || value < MinValue)
                throw new ArgumentOutOfRangeException($"Value was {value} where MinValue was {min} and MaxValue was {max}");
            _value = value;
        }

        /// <summary>
        /// Implicitly converts a <see cref="int"/> value to a <see cref="FInt32"/>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator FInt16(short value) => new FInt16(value);
        /// <summary>
        /// Implicitly converts a <see cref="FInt16"/> to a <see cref="int"/>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator short(FInt16 value) => value.Value;

        /// <summary>
        /// Increments a <see cref="FInt16"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FInt16 operator ++(FInt16 value)
        {
            value.Value++;
            if (value.Value > value.MaxValue) value.Value = value.MinValue;
            return value;
        }

        /// <summary>
        /// Deincrements a <see cref="FInt16"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FInt16 operator --(FInt16 value)
        {
            value.Value--;
            if (value.Value < value.MinValue) value.Value = value.MaxValue;
            return value;
        }

        /// <summary>
        /// Adds two <see cref="FInt16"/> instances together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns>The result of the calculation (<paramref name="value"/> + <paramref name="other"/>)</returns>
        public static FInt32 operator +(FInt16 value, FInt16 other)
        {
            return new FInt32(value.Value + other.Value,
                value.MinValue < other.MinValue ? value.MinValue : other.MinValue,
                value.MaxValue > other.MaxValue ? value.MaxValue : other.MaxValue);
        }

        /// <summary>
        /// Subtracts one <see cref="FInt16"/> from the other.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns>The result of the calculation (<paramref name="value"/> + <paramref name="other"/>)</returns>
        public static FInt16 operator -(FInt16 value, FInt16 other)
        {
            return new FInt16((short)(value.Value - other.Value),
                value.MinValue < other.MinValue ? value.MinValue : other.MinValue,
                value.MaxValue > other.MaxValue ? value.MaxValue : other.MaxValue);
        }

        /*
         * IComparable Implementation
         */

        /// <inheritdoc/>

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is FInt32))
                throw new ArgumentException("Argument must be FInt32");
            return CompareTo((FInt32)obj);
        }
        /// <inheritdoc/>

        public int CompareTo(FInt32 other)
        {
            if (Value < other) return -1;
            if (Value > other) return 1;
            return 0;
        }
        /*
         * IEquatable Implementation
         */

        /// <inheritdoc/>

        public override bool Equals(object obj)
        {
            if (obj is FInt32 @int)
                return Equals(@int);
            return false;
        }
        /// <inheritdoc/>

        public bool Equals(FInt32 other) => Value == other;
        /// <inheritdoc/>

        // The absolute value for Value
        public override int GetHashCode()
        {
            return Value;
        }

        /*
         * IConvertible Implementation
         */

        /// <inheritdoc/>
        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }
        /// <inheritdoc/>

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }
        /// <inheritdoc/>

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }
        /// <inheritdoc/>

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from OInt64 to DateTime");
        }
        /// <inheritdoc/>

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }
        /// <inheritdoc/>

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }
        /// <inheritdoc/>


        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }
        /// <inheritdoc/>

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }
        /// <inheritdoc/>

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }
        /// <inheritdoc/>

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }
        /// <inheritdoc/>

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }
        /// <inheritdoc/>

        public string ToString(IFormatProvider provider)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return ToString(provider);
        }
        /// <inheritdoc/>

        public override string ToString()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Value.ToString();
        }
        /// <inheritdoc/>

        public string ToString(string format)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Value.ToString(format);
        }
        /// <inheritdoc/>

        public string ToString(string format, IFormatProvider provider)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Value.ToString(format, provider);
        }
        /// <inheritdoc/>

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        /// <inheritdoc/>

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value, provider);
        }
        /// <inheritdoc/>

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value, provider);
        }
        /// <inheritdoc/>

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value, provider);
        }
        /// <inheritdoc/>

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        /*
         * Parsing
         */
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static FInt16 Parse(string s)
        {
            return short.Parse(s);
        }

        public static FInt16 Parse(string s, NumberStyles style)
        {
            return short.Parse(s, style);
        }

        public static FInt16 Parse(string s, IFormatProvider provider)
        {
            return short.Parse(s, provider);
        }

        public static FInt16 Parse(string s, NumberStyles style, IFormatProvider format)
        {
            return short.Parse(s, style, format);
        }

        public static bool TryParse(string s, out FInt16 result)
        {
            bool b = short.TryParse(s, out short i);
            result = i;
            return b;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out FInt16 result)
        {
            bool b = short.TryParse(s, style, provider, out short i);
            result = i;
            return b;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}