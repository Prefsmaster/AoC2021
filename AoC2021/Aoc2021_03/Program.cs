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
            var patternlist = patterns.ToList();
            var gammarate = 0;
            var bitsperpattern = patterns[0].Length;

            var bitvalue = 1 << (bitsperpattern - 1);
            for (var bit = 0; bit < bitsperpattern; bit++, bitvalue >>=1)
            {
                if (MostOnes(patternlist,bit)) gammarate += bitvalue;
            }
            var epsilonrate = (1<<bitsperpattern)-1-gammarate;
            return gammarate*epsilonrate;
        }

        private static bool MostOnes(List<string> patterns, int bit) => patterns.Where((pattern) => pattern[bit] == '1').Count() * 2 >= patterns.Count;

        private static int Part2(string[] patterns)
        {
            var oxygen_generator_rating = FilterPatterns(patterns, false);
            var CO2_scrubber_rating = FilterPatterns(patterns, true);
            return oxygen_generator_rating * CO2_scrubber_rating;
        }

        private static int FilterPatterns(string[] patterns, bool invertedFilter)
        {
            var patternlist = patterns.ToList();
            var bitsperpattern = patterns[0].Length;

            for (var bit = 0; bit < bitsperpattern && patternlist.Count > 1; bit++)
            {
                var tokeep = MostOnes(patternlist, bit) != invertedFilter ? '1' : '0';
                patternlist.RemoveAll(x => x[bit] != tokeep);  // remove other patterns 
            }
            return Convert.ToInt32(patternlist[0], 2);
        }
    }
}