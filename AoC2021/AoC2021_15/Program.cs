namespace AoC2021_15
{
    class Program
    {
        private static Chiton[,] Chitons;
        private static Chiton[,] Chitons2;
        private static int W;
        private static int H;
        static void Main(string[] args)
        {
            BuildChitonData("input.txt");

            Solve(1);
            Chitons = Chitons2;
            Solve(5);

            Console.ReadKey();
        }

        private static void BuildChitonData(string filename)
        {
            var chitondata = File.ReadAllLines(filename);
            H = chitondata.Length;
            W = chitondata[0].Length;

            Chitons = new Chiton[H, W];
            Chitons2 = new Chiton[H*5, W*5];
            for (var y = 0; y < H; y++)
                for (var x = 0; x < W; x++)
                {
                    var c = chitondata[y][x]-'0';
                    Chitons[y, x] = new Chiton(x, y, c);

                    for (int ry=0;ry<5;ry++)
                    {
                        for (int rx=0;rx<5;rx++)
                        {
                            Chitons2[y+ry*H,x+rx*W] = new Chiton(x + rx * W, y + ry * H, c+ry+rx);
                        }  
                    }
                }
            //for (var y = 0; y < H*5; y++)
            //{
            //    for (var x = 0; x < W*5; x++)
            //    {
            //        Console.Write((char)(Chitons2[y, x].Level+'0'));
            //    }
            //    Console.WriteLine();
            //}

        }

        private static Queue<Chiton> queue;
        private static void Solve(int r)
        {
            // dijkstra 

            // queue of coords (chitons) to process
            queue = new Queue<Chiton>();

            Chitons[0, 0].Distance = 0; // we start here
            queue.Enqueue(Chitons[0, 0]);

            while (queue.TryDequeue(out var c))
            {
                var neighbors = GetNeighbors(c, r);

                foreach (var n in neighbors)
                {
                    // already visited?
                    if (n.Distance != ulong.MaxValue)
                    {
                        // can get to neighbor from here faster?
                        if (n.Distance > c.Distance + n.Level)
                            n.Distance = c.Distance + n.Level;

                        // can get to here from neighbour faster?
                        if (c.Distance > n.Distance + c.Level)
                            c.Distance = n.Distance + c.Level;
                    }
                    else
                    {
                        // add new node to queue
                        n.Distance = c.Distance + n.Level;
                        queue.Enqueue(n);
                    }
                }
            }

            // Plot distance to last node...
            Console.WriteLine($"\n{Chitons[(H*r) - 1, (W*r) - 1].Distance - 3}");
        }

        private static IEnumerable<Chiton> GetNeighbors(Chiton c, int r)
        {
            var x = c.X;
            var y = c.Y;

            var n = new List<Chiton>();

            // only down and right
            if (x < (W*r - 1)) n.Add(Chitons[y, x + 1]);
            if (y < (H*r - 1)) n.Add(Chitons[y + 1, x]);
            // also left and up
            if (x > 0) n.Add(Chitons[y, x - 1]);
            if (y > 0) n.Add(Chitons[y - 1, x]);

//            Console.Write(n.Count);
            return n;
        }

    }
    class Chiton
    {
        public readonly uint Level;
        public readonly int X;
        public readonly int Y;

        public ulong Distance;

        public Chiton(int x, int y, int level)
        {
            X = x;
            Y = y;

            Level = (uint)(level > 9 ? level-9:level);
            Distance = ulong.MaxValue;
        }
    }
}
