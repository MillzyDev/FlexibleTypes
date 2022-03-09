using fint = TypeUtils.Integer.FInt32;

namespace TypeUtils.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            Tuple<string, string> french = Language.FR;
            Console.WriteLine($"Language: {french.Item1}, Code: {french.Item2}");

            fint i = new(0, min: 0, max: 5);
            int j = 0;
            while (j < 12)
            {
                i++;
                j++;
                Console.WriteLine(i);
            }
        }
    }
}