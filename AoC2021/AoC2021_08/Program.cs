namespace AoC2021_08
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new StreamReader(@"input.txt");

            Console.WriteLine(SolveDay8_1(file));
            Console.WriteLine(SolveDay8_2(file));

            Console.ReadKey();
        }

        private static long SolveDay8_1(StreamReader file)
        {
            string digitdata;
            int simpledigits = 0;
            while ((digitdata = file.ReadLine()) != null)
            {
                var parts = digitdata.Split(" | ");
                var digits = parts[1].Split(' ');
                simpledigits += digits.Where(d => d.Length == 2 || d.Length == 3 ||  d.Length == 4 || d.Length == 7).Count();
            }
            return simpledigits;
        }

        private static long SolveDay8_2(StreamReader file)
        {
            return -1;
        }
    }
}
