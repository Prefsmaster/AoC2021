namespace AoC2021_03
{
    class Program
    {
        static void Main(string[] args)
        {
            var bitpatterns = File.ReadAllLines(@"input.txt");

            Console.WriteLine(Part1(bitpatterns));
            Console.WriteLine(Part2(bitpatterns));

            Console.ReadKey();
        }

        private static int Part1(string[] patterns)
        {
            var gammarate = 0;

            var patterncount = patterns.Length;
            var bitsperpattern = patterns[0].Length;

            var bitvalue = 1 << (bitsperpattern - 1);
            for (var bit = 0; bit < bitsperpattern; bit++)
            {
                var onecount = patterns.Where((row) => row[bit] == '1').Count();
                if (onecount > (patterncount - onecount))
                    gammarate += bitvalue;
                bitvalue >>= 1;
            }
            var epsilonrate = (1<<bitsperpattern)-1-gammarate;
            return gammarate*epsilonrate;
        }

        private static int Part2(string[] patterns)
        {
            return -1;
        }
    }
}