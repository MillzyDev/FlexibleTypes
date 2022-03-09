using FlexibleTypes.Enumerator;
using Value = System.Tuple<string, string>;

namespace FlexibleTypes.Tests
{
    internal class Language : FEnum<Value>
    {
        public static readonly FEnum<Value>.FValue EN = new Value("English", "EN");
        public static readonly FEnum<Value>.FValue FR = new Value("French", "FR");
        public static readonly FEnum<Value>.FValue ES = new Value("Spanish", "ES");
    }

    enum Thingy
    {
        Thing1,
        Thing2
    }
}
