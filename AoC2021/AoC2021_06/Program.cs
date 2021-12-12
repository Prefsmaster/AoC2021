namespace AoC2021_06
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new StreamReader(@"input.txt");

            var fish_ages = file.ReadLine().Split(',').Select(byte.Parse).ToList();

            Console.WriteLine(SolveDay6(fish_ages, 80));
            Console.WriteLine(SolveDay6(fish_ages, 256));

            Console.ReadKey();
        }

        private static long SolveDay6(List<byte> population,int generations)
        {
            long numberoffish = population.Count;

            // count initial ages
            var agecounts = new long[9];
            foreach (var age in population) 
                agecounts[age]++;
            
            for (var g = 0; g < generations; g++)
            {
                // remember #of fish that spawn
                var spawned = agecounts[0];
                // move all ages 1 down
                for (var i=1; i<9; i++)
                    agecounts[i - 1] = agecounts[i];

                // add fish that spawned to agegroup 6
                agecounts[6] += spawned;

                // new fish have age 8
                agecounts[8] = spawned;
                // and add to total
                numberoffish += spawned;
            }
            return numberoffish;
        }
    }
}
