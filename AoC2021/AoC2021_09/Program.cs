namespace AoC2021_09
{
    class Program
    {
        static void Main(string[] args)
        {
            var ventdata = File.ReadAllLines(@"input.txt").ToArray();

            Console.WriteLine(SolveDay9_1(ventdata));

            Console.ReadKey();
        }

        private static long SolveDay9_1(string[] ventdata)
        {
            var sum = 0;
            var w = ventdata[0].Length;
            var h = ventdata.Length;

            for (var y = 0; y < w; y++)
            {
                for (var x = 0; x < h; x++)
                {
                    var islowest = true;
                    var l = ventdata[y][x];

                    if (islowest && y - 1 >= 0 && ventdata[y - 1][x] <= l) islowest = false;
                    if (islowest && y + 1 <  h && ventdata[y + 1][x] <= l) islowest = false;
                    if (islowest && x - 1 >= 0 && ventdata[y][x - 1] <= l) islowest = false;
                    if (islowest && x + 1 <  w && ventdata[y][x + 1] <= l) islowest = false;

                    if (islowest) sum += l - '0' + 1;
                }
            }
            return sum;
        }
        private static long SolveDay9_2(string[] ventdata)
        {
            // do lowest points identify unique basins?
            // NO

            var sum = 0;
            for (var y = 0; y < ventdata.Length; y++)
            {
                for (var x = 0; x < ventdata[y].Length; x++)
                {
                    var islowest = true;
                    var h = ventdata[y][x];
                    if (islowest && y - 1 >= 0 && ventdata[y - 1][x] <= h) islowest = false;
                    if (islowest && y + 1 < ventdata.Length && ventdata[y + 1][x] <= h) islowest = false;
                    if (islowest && x - 1 >= 0 && ventdata[y][x - 1] <= h) islowest = false;
                    if (islowest && x + 1 < ventdata[y].Length && ventdata[y][x + 1] <= h) islowest = false;
                    if (islowest)
                    {
                        //                        Console.WriteLine($"{x:D3} {y:D3} {h}");
                        sum += h - '0' + 1;
                    }
                }
            }
            return sum;
        }

    }
}
