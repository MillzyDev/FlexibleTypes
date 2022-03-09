using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace TypeUtils.Number
{
    /// <summary>
    /// Overflowable 32 bit Integer. OInt32 or oint
    /// <para><see cref="int"/> implementation where the <see cref="OInt32.MinValue"/> and <see cref="OInt32.MaxValue"/> are not constant.</para>
    /// <para>If the value receeds or exceeds <see cref="MinValue"/> or <see cref="OInt32.MaxValue"/> then it will wrap back around.</para>
    /// </summary>
    /// <example>
    /// If the max value is <c>5</c> and the min value is <c>0</c>: When the <see cref="OInt32"/> instance increments past 5, it will become 0.
    /// If the <see cref="OInt32"/> instance is set to a value beyond the maximum value, the value will be the maximum value, the same is true for any value before the minimum value
    /// <code>
    /// using TypeUtils.Number;
    /// using oint = OInt32;
    /// ...
    /// oint i; // MaxValue is int.MaxValue and MinValue is int.MinValue by default.
    /// i.MaxValue = 5;
    /// i.MinValue = 0;
    /// 
    /// /*    or    */
    ///     
    /// OInt32 i = new OInt32(0, min: 0, max: 5);
    /// 
    /// i = 5;
    /// i++; // Will wrap around and become 0
    /// </code>
    /// </example>
    public struct OInt32 : IComparable, IComparable<OInt32>, IConvertible, IEquatable<OInt32>, IFormattable
    {
        /// <summary>
        /// <see cref="OInt32"/> instance where <see cref="Value"/> is <c>0</c>, <see cref="MaxValue"/> is <see cref="int.MaxValue"/> and <see cref="MinValue"/> is <see cref="int.MinValue"/>
        /// </summary>
        public static OInt32 Default
        {
            get => new OInt32(0);
        }

        private int _value;
        private int Value
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
        public int MaxValue { get; set; }

        /// <summary>
        /// The minimum value the value can be
        /// </summary>
        public int MinValue { get; set; }

        public OInt32(int value, int min = int.MinValue, int max = int.MaxValue)
        {
            (MaxValue, MinValue) = (max, min);
            if (value > MaxValue || value < MinValue)
                throw new ArgumentOutOfRangeException($"Value was {value} where MinValue was {min} and MaxValue was {max}");
            _value = value;
        }

        public static implicit operator OInt32(int value) => new OInt32(value);
        public static implicit operator int(OInt32 value) => value.Value;

        public static OInt32 operator ++(OInt32 value)
        {
            value.Value++;
            if (value.Value > value.MaxValue) value.Value = value.MinValue;
            return value;
        }

        public static OInt32 operator --(OInt32 value)
        {
            value.Value--;
            if (value.Value < value.MinValue) value.Value = value.MaxValue;
            return value;
        }

        public static OInt32 operator +(OInt32 value, OInt32 other)
        {
            return new OInt32(value.Value + other.Value,
                value.MinValue < other.MinValue ? value.MinValue : other.MinValue,
                value.MaxValue > other.MaxValue ? value.MaxValue : other.MaxValue);
        }

        public static OInt32 operator -(OInt32 value, OInt32 other)
        {
            return new OInt32(value.Value - other.Value,
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
            if (!(obj is OInt32))
                throw new ArgumentException("Argument must be OInt32");
            return CompareTo((OInt32)obj);
        }

        public int CompareTo(OInt32 other)
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
            if (obj is OInt32 @int)
                return Equals(@int);
            return false;
        }

        public bool Equals(OInt32 other) => Value == other;

        // The absolute value for Value
        public override int GetHashCode()
        {
            return Value;
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
            throw new InvalidCastException("Invalid cast from OInt32 to DateTime");
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
        public static OInt32 Parse(string s)
        {
            return int.Parse(s);
        }

        public static OInt32 Parse(string s, NumberStyles style) 
        {
            return int.Parse(s, style);
        }

        public static OInt32 Parse(string s, IFormatProvider provider)
        {
            return int.Parse(s, provider);
        }

        public static OInt32 Parse(string s, NumberStyles style, IFormatProvider format)
        {
            return int.Parse(s, style, format);
        }

        public static bool TryParse(string s, out OInt32 result)
        {
            bool b = int.TryParse(s, out int i);
            result = i;
            return b;
        }

        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out OInt32 result)
        {
            bool b = int.TryParse(s, style, provider, out int i);
            result = i;
            return b;
        }
    }
}