namespace AoC2021_11
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputdata = File.ReadAllLines(@"input.txt").ToArray();

            Console.WriteLine(Solve_1(inputdata));
            Console.WriteLine(Solve_2(inputdata));

            Console.ReadKey();
        }

        private static long Solve_1(string[] ventdata)
        {
            // fill array of ints
            var w = ventdata[0].Length;
            var h = ventdata.Length;
            var energylevels = new int[h, w];

            var y = 0;
            foreach (var row in ventdata)
            {
                for (var x = 0; x < w; x++)
                    energylevels[y,x] = row[x]-'0';
                y++;
            }

            var totalflashes = 0;
            for (var steps = 1; steps <= 100; steps++)
            {
                totalflashes += Step(energylevels, w, h);
                if (steps < 10 || steps % 10 == 0)
                {
                    Console.WriteLine($"situation after step {steps}:");
                    Plot(energylevels, w, h);
                }
            }
            return totalflashes;
        }
        private static long Solve_2(string[] ventdata)
        {
            var w = ventdata[0].Length;
            var h = ventdata.Length;
            var energylevels = new int[h, w];

            var y = 0;
            foreach (var row in ventdata)
            {
                for (var x = 0; x < w; x++)
                    energylevels[y, x] = row[x] - '0';
                y++;
            }

            var stepstaken = 0;
            do
            {
                stepstaken++;

            } while (Step(energylevels, w, h) != w * h);
            return stepstaken;
        }

        private static void Plot(int[,] e, int w, int h)
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    char plot;
                    if (e[y, x] < 10)
                        plot = e[y, x] != 0 ? (char)(e[y, x] + '0') : '.';
                    else
                        plot = '*';
                    Console.Write($"{plot}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private static int Step(int[,] e, int w, int h)
        {
            var hasflashed = new bool[h, w];

            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                    e[y, x]++;

            var flashed = true;
            while (flashed)
            {
                //Plot(e, w, h);

                flashed = false;
                for (var y = 0; y < h; y++)
                    for (var x = 0; x < w; x++)
                        if (e[y, x] > 9 && !hasflashed[y, x])
                        {
                            hasflashed[y, x] = flashed = true;
                            var xstart = x > 0 ? x - 1 : 0;
                            var xend = x == w - 1 ? w - 1 : x + 1;
                            var ystart = y > 0 ? y - 1 : 0;
                            var yend = y == h - 1 ? h - 1 : y + 1;
                            for (var dx = xstart; dx <= xend; dx++)
                                for (var dy = ystart; dy <= yend; dy++)
                                    if (!(dx == x && dy == y))
                                        e[dy, dx]++;

                        }
            }

            var flashescount = 0;
            for (var y = 0; y < h; y++)
                for (var x = 0; x < w; x++)
                    if (e[y, x] > 9)
                    {
                        e[y, x] = 0;
                        flashescount++;
                    }

            return flashescount;
        }
    }
}
