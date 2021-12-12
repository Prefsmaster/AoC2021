namespace AoC2021_07
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new StreamReader(@"input.txt");

            var positions = file.ReadLine().Split(',').Select(int.Parse).ToList();

            Console.WriteLine(SolveDay7_1(positions));
            Console.WriteLine(SolveDay7_2(positions));

            Console.ReadKey();
        }

        private static long SolveDay7_1(List<int> positions)
        {
            long fuelused = 0;
            // sort
            var sorted = positions.OrderBy(x => x).ToList();
            // median
            var median = sorted[(int)(positions.Count/2)];
            // calculate differences
            foreach (var p in positions)
                fuelused += Math.Abs(p - median);
            return fuelused;
        }

        private static long SolveDay7_2(List<int> positions)
        {
            // use average destination may be anywhere!

            var average = (int)positions.Average();

            long leastfuel = long.MaxValue;

            // there must be a more C#/linq like solution, but this works...
            for (int i = average;i<average+2;i++)
            {
                long fueltohere = 0L;
                foreach (var from in positions)
                {
                    var distance = Math.Abs(i-from);
                    fueltohere += (distance * (distance + 1)) / 2;
                }
                if (fueltohere < leastfuel)
                    leastfuel = fueltohere;
            }
            return leastfuel;
        }
    }
}
