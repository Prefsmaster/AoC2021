namespace AoC2021_12
{
    class Program
    {
        private static readonly List<Cave> Caves = new();
        private static readonly List<string> Paths = new();
        static void Main(string[] args)
        {
            var cavedata = File.ReadAllLines(@"input.txt").ToArray();

            BuildCaveNetwork(cavedata);

            Console.WriteLine(Solve_1());
            Console.WriteLine(Solve_2());

            Console.ReadKey();
        }

        private static void BuildCaveNetwork(string[] cavedata)
        {
            foreach (var cavelink in cavedata)
            {
                var caveIds = cavelink.Split('-');
                var caves = new Cave[2];

                for (var i = 0; i<2;i++)
                {
                    if ((caves[i] = Caves.FirstOrDefault(c => c.Id == caveIds[i])) == null)
                    {
                        caves[i] = new Cave(caveIds[i]);
                        Caves.Add(caves[i]);
                    }
                }
                caves[0].AddConnection(caves[1]);
                caves[1].AddConnection(caves[0]);
            }
        }

        private static long Solve_1()
        {
            // find start cave
            Paths.Clear();
            SolveNetwork(Caves.Single(c => c.Id == "start"), "");
            return Paths.Count;
        }
        private static long Solve_2()
        {
            Paths.Clear();
            var startcave = Caves.Single(c => c.Id == "start");
            var littlecaves = Caves.Where(c => c.Id != "start" && c.Id != "start" && c.IsSmall);
            foreach (var littlecave in littlecaves)
            {
                littlecave.visits = 2;
                SolveNetwork(startcave, "");
                littlecave.visits = 1;
            }
            return Paths.Distinct().Count();
        }

        private static void SolveNetwork(Cave c, string path)
        {
            if (c.Id=="end")
            {
                Paths.Add(path+",end");
//                Console.WriteLine($"{path + ",end"}");
                return;
            }
            else
            {
                c.Visit();
                var connnnn = c.Connections.Where(conn => conn.Canvisit);
                foreach (var connection in connnnn)
                {
                    SolveNetwork(connection, path + (path==""?"":",") +c.Id);
                }
                c.Unvisit();
                return;
            }
        }
    }

    class Cave
    {
        public string Id;
        public List<Cave> Connections;
        public bool Canvisit => visits != 0;
        public bool IsSmall => Id[0] >= 'a' && Id[0] <= 'z';

        public int visits; 

        public Cave(string id)
        {
            Id = id;
            Connections = new List<Cave>();
            visits = Id[0] >= 'a' && Id[0] <= 'z' ? 1 : -1;
        }

        public void Visit()
        {
            if (visits > 0 && IsSmall)
                visits--;
        }
        public void Unvisit()
        {
            if (visits >= 0 && IsSmall)
                visits++;
        }

        public void AddConnection(Cave c)
        {
            Connections.Add(c);
        }
    }
}
