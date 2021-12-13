namespace AoC2021_09
{
    class Program
    {
        static void Main(string[] args)
        {
            var ventdata = File.ReadAllLines(@"input.txt").ToArray();

            Console.WriteLine(SolveDay9_1(ventdata));
            Console.WriteLine(SolveDay9_2(ventdata));

            Console.ReadKey();
        }

        private static long SolveDay9_1(string[] ventdata)
        {
            var sum = 0;
            var w = ventdata[0].Length;
            var h = ventdata.Length;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
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
            // YES

            // locate lowest points, and flood-fill to find size.
            var w = ventdata[0].Length;
            var h = ventdata.Length;

            var basinsizes = new List<int>();
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var islowest = true;
                    var l = ventdata[y][x];

                    if (islowest && y - 1 >= 0 && ventdata[y - 1][x] <= l) islowest = false;
                    if (islowest && y + 1 < h && ventdata[y + 1][x] <= l) islowest = false;
                    if (islowest && x - 1 >= 0 && ventdata[y][x - 1] <= l) islowest = false;
                    if (islowest && x + 1 < w && ventdata[y][x + 1] <= l) islowest = false;

                    if (islowest) 
                    {
                        var visited = new bool[h, w];
                        basinsizes.Add(GetBasinSize(ventdata, visited, x, y, 0));
                    }
                }
            }
            return basinsizes.OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);
        }
        private static int GetBasinSize(string[] map, bool[,] visited,int x, int y, int size)
        {

            visited[y, x] = true;
            size++;

            if (y - 1 >= 0 && !visited[y - 1, x] && map[y - 1][x] < '9')
                size = GetBasinSize(map, visited, x, y-1, size);
            if (y + 1 < map.Length && !visited[y+1, x] && map[y + 1][x] < '9')
                size = GetBasinSize(map, visited, x, y + 1, size);
            if (x - 1 >= 0 && !visited[y, x-1] && map[y][x-1] < '9')
                size = GetBasinSize(map, visited, x - 1,y, size);
            if (x + 1 < map[0].Length && !visited[y, x+1] && map[y][x+1] < '9')
                size = GetBasinSize(map, visited, x +1, y, size);
            return size;
        }
    }
}
