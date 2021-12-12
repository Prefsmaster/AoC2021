namespace AoC2021_06
{
    class Program
    {
        static long[] replicationtable;

        static List<byte> lanternfish;

        static void Main(string[] args)
        {
            var file = new StreamReader(@"input.txt");

            var fish_ages = file.ReadLine().Split(',').Select(byte.Parse).ToList();

            Console.WriteLine(Part2(fish_ages, 80));
            Console.WriteLine(Part2(fish_ages, 256));

            Console.ReadKey();
        }

        private static long Part2(List<byte> population,int generations)
        {
            long numberoffish = population.Count;
            var agecounts = new long[9];

            // count initial ages
            foreach (var age in population) 
                agecounts[age]++;

            
            for (var g = 0; g < generations; g++)
            {
                // remember #of fish that spawn
                var spawned = agecounts[0];
                // move all ages 1 down
                for (var i=1; i<9; i++)
                    agecounts[i - 1] = agecounts[i];
                // add spawns to agegroups 6

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
