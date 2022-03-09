using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace FlexibleTypes.Integer
{
    /// <summary>
    /// Flexible 64 bit Integer. FInt64 or fint
    /// <para><see cref="int"/> implementation where the <see cref="MinValue"/> and <see cref="MaxValue"/> are not constant.</para>
    /// <para>If the value receeds or exceeds <see cref="MinValue"/> or <see cref="MaxValue"/> then it will wrap back around.</para>
    /// </summary>
    /// <example>
    /// If the max value is <c>5</c> and the min value is <c>0</c>: When the <see cref="FInt64"/> instance increments past 5, it will become 0.
    /// If the <see cref="FUInt32"/> instance is set to a value beyond the maximum value, the value will be the maximum value, the same is true for any value before the minimum value
    /// <code>
    /// using FlexibleTypes.Number;
    /// using fuint = Fuint64
    /// ...
    /// fuint i; // MaxValue is uint.MaxValue and MinValue is uint.MinValue by default.
    /// i.MaxValue = 5;
    /// i.MinValue = 0;
    /// 
    /// /*    or    */
    ///     
    /// fuint i = new fuint(0, min: 0, max: 5);
    /// 
    /// i = 5;
    /// i++; // Will wrap around and become 0
    /// </code>
    /// </example>
    public struct FUInt32 : IComparable, IComparable<FUInt32>, IConvertible, IEquatable<FUInt32>, IFormattable
    {
        /// <summary>
        /// <see cref="FUInt32"/> instance where <see cref="Value"/> is <c>0</c>, <see cref="MaxValue"/> is <see cref="uint.MaxValue"/> and <see cref="MinValue"/> is <see cref="uint.MinValue"/>
        /// </summary>
        public static FUInt32 Default
        {
            get => new FUInt32(0);
        }

        private uint _value;
        private uint Value
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
        public uint MaxValue { get; set; }

        /// <summary>
        /// The minimum value the value can be
        /// </summary>
        public uint MinValue { get; set; }

        public FUInt32(uint value, uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            (MaxValue, MinValue) = (max, min);
            if (value > MaxValue || value < MinValue)
                throw new ArgumentOutOfRangeException($"Value was {value} where MinValue was {min} and MaxValue was {max}");
            _value = value;
        }

        /// <summary>
        /// Implicitly converts a <see cref="uint"/> value to a <see cref="FUInt32"/>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator FUInt32(uint value) => new FUInt32(value);
        /// <summary>
        /// Implicitly converts a <see cref="Fuint64"/> to a <see cref="uint"/>
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator uint(FUInt32 value) => value.Value;

        /// <summary>
        /// Increments a <see cref="FUInt32"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FUInt32 operator ++(FUInt32 value)
        {
            value.Value++;
            if (value.Value > value.MaxValue) value.Value = value.MinValue;
            return value;
        }

        /// <summary>
        /// Deincrements a <see cref="FUInt32"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FUInt32 operator --(FUInt32 value)
        {
            value.Value--;
            if (value.Value < value.MinValue) value.Value = value.MaxValue;
            return value;
        }

        /// <summary>
        /// Adds two <see cref="FUInt32"/> instances together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns>The result of the calculation (<paramref name="value"/> + <paramref name="other"/>)</returns>
        public static FUInt32 operator +(FUInt32 value, FUInt32 other)
        {
            return new FUInt32(value.Value + other.Value,
                value.MinValue < other.MinValue ? value.MinValue : other.MinValue,
                value.MaxValue > other.MaxValue ? value.MaxValue : other.MaxValue);
        }

        /// <summary>
        /// Subtracts one <see cref="Fuint64"/> from the other.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns>The result of the calculation (<paramref name="value"/> + <paramref name="other"/>)</returns>
        public static FUInt32 operator -(FUInt32 value, FUInt32 other)
        {
            return new FUInt32(value.Value - other.Value,
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
            if (!(obj is FUInt32))
                throw new ArgumentException("Argument must be FUInt32");
            return CompareTo((FUInt32)obj);
        }
        /// <inheritdoc/>

        public int CompareTo(FUInt32 other)
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
            if (obj is FUInt32 @uint)
                return Equals(@uint);
            return false;
        }
        /// <inheritdoc/>

        public bool Equals(FUInt32 other) => Value == other;
        /// <inheritdoc/>

        // The absolute value for Value
        public override int GetHashCode()
        {
            return (int)Value;
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
        public static FInt64 Parse(string s)
        {
            return int.Parse(s);
        }

        public static FInt64 Parse(string s, NumberStyles style)
        {
            return int.Parse(s, style);
        }

        public static FInt64 Parse(string s, IFormatProvider provider)
        {
            return int.Parse(s, provider);
        }

        public static FInt64 Parse(string s, NumberStyles style, IFormatProvider format)
        {
            return int.Parse(s, style, format);
        }

        public static bool TryParse(string s, out FInt64 result)
        {
            bool b = int.TryParse(s, out int i);
            result = i;
            return b;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out FInt64 result)
        {
            bool b = int.TryParse(s, style, provider, out int i);
            result = i;
            return b;
        }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
