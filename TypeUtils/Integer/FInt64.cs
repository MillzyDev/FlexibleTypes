using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

namespace TypeUtils.Number
{
    /// <summary>
    /// Overflowable 64 bit Integer. OInt64 or olong
    /// <para><see cref="long"/> implementation where the <see cref="OInt64.MinValue"/> and <see cref="OInt64.MaxValue"/> are not constant.</para>
    /// <para>If the value receeds or exceeds <see cref="MinValue"/> or <see cref="OInt64.MaxValue"/> then it will wrap back around.</para>
    /// </summary>
    /// <example>
    /// If the max value is <c>5</c> and the min value is <c>0</c>: When the <see cref="OInt64"/> instance increments past 5, it will become 0.
    /// If the <see cref="OInt64"/> instance is set to a value beyond the maximum value, the value will be the maximum value, the same is true for any value before the minimum value
    /// <code>
    /// using TypeUtils.Number;
    /// using olong = OInt64
    /// ...
    /// olong i; // MaxValue is long.MaxValue and MinValue is long.MinValue by default.
    /// i.MaxValue = 5;
    /// i.MinValue = 0;
    /// 
    /// /*    or    */
    ///     
    /// olong i = new OInt64(0, min: 0, max: 5);
    /// 
    /// i = 5;
    /// i++; // Will wrap around and become 0
    /// </code>
    /// </example>
    public struct OInt64 : IComparable, IComparable<OInt64>, IConvertible, IEquatable<OInt64>, IFormattable
    {
        /// <summary>
        /// <see cref="OInt64"/> instance where <see cref="Value"/> is <c>0</c>, <see cref="MaxValue"/> is <see cref="long.MaxValue"/> and <see cref="MinValue"/> is <see cref="long.MinValue"/>
        /// </summary>
        public static OInt64 Default
        {
            get => new OInt64(0);
        }

        private long _value;
        private long Value
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
        public long MaxValue { get; set; }

        /// <summary>
        /// The minimum value the value can be
        /// </summary>
        public long MinValue { get; set; }

        public OInt64(long value, long min = long.MinValue, long max = long.MaxValue)
        {
            (MaxValue, MinValue) = (max, min);
            if (value > MaxValue || value < MinValue)
                throw new ArgumentOutOfRangeException($"Value was {value} where MinValue was {min} and MaxValue was {max}");
            _value = value;
        }

        public static implicit operator OInt64(long value) => new OInt64(value);
        public static implicit operator long(OInt64 value) => value.Value;

        public static OInt64 operator ++(OInt64 value)
        {
            value.Value++;
            if (value.Value > value.MaxValue) value.Value = value.MinValue;
            return value;
        }

        public static OInt64 operator --(OInt64 value)
        {
            value.Value--;
            if (value.Value < value.MinValue) value.Value = value.MaxValue;
            return value;
        }

        public static OInt64 operator +(OInt64 value, OInt64 other)
        {
            return new OInt64(value.Value + other.Value,
                value.MinValue < other.MinValue ? value.MinValue : other.MinValue,
                value.MaxValue > other.MaxValue ? value.MaxValue : other.MaxValue);
        }

        public static OInt64 operator -(OInt64 value, OInt64 other)
        {
            return new OInt64(value.Value - other.Value,
                value.MinValue < other.MinValue ? value.MinValue : other.MinValue,
                value.MaxValue > other.MaxValue ? value.MaxValue : other.MaxValue);
        }

        /*
         * IComparable Implementation
         */
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (!(obj is OInt64))
                throw new ArgumentException("Argument must be OInt64");
            return CompareTo((OInt64)obj);
        }

        public int CompareTo(OInt64 other)
        {
            if (Value < other) return -1;
            if (Value > other) return 1;
            return 0;
        }

        /*
         * IEquatable Implementation
         */
        public override bool Equals(object obj)
        {
            if (obj is OInt64 @int)
                return Equals(@int);
            return false;
        }

        public bool Equals(OInt64 other) => Value == other;

        // The absolute value for Value
        public override int GetHashCode()
        {
            return (int)Value;
        }

        /*
         * IConvertible Implementation
         */
        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from OInt64 to DateTime");
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return ToString(provider);
        }

        public override string ToString()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Value.ToString();
        }

        public string ToString(string format)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Value.ToString(format);
        }

        public string ToString(string format, IFormatProvider provider)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Value.ToString(format, provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value, provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value, provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value, provider);
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        /*
         * Parsing
         */
        public static OInt64 Parse(string s)
        {
            return long.Parse(s);
        }

        public static OInt64 Parse(string s, NumberStyles style)
        {
            return long.Parse(s, style);
        }

        public static OInt64 Parse(string s, IFormatProvider provider)
        {
            return long.Parse(s, provider);
        }

        public static OInt64 Parse(string s, NumberStyles style, IFormatProvider format)
        {
            return long.Parse(s, style, format);
        }

        public static bool TryParse(string s, out OInt64 result)
        {
            bool b = long.TryParse(s, out long i);
            result = i;
            return b;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out OInt64 result)
        {
            bool b = long.TryParse(s, style, provider, out long i);
            result = i;
            return b;
        }
    }
}
