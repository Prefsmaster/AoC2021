namespace AoC2021_13
{
    class Program
    {
        private static List<Coord> Coords = new();
        private static List<Fold> Folds = new();
        static void Main(string[] args)
        {
            var folddata = File.ReadAllLines(@"input.txt").ToArray();

            BuildFoldData(folddata);

            Console.WriteLine(Solve_1());

            Solve_2();

            Console.ReadKey();
        }

        private static void BuildFoldData(string[] folddata)
        {
            foreach (var foldline in folddata)
            {
//                if (string.IsNullOrEmpty(foldline)) continue;
                if (foldline.Contains(','))
                {
                    var values = foldline.Split(',');
                    Coords.Add(new Coord { x = int.Parse(values[0]), y = int.Parse(values[1]) });
                }
                if (foldline.Contains('='))
                {
                    Folds.Add(new Fold { pos = int.Parse(foldline[13..]), dir = foldline[11] });
                }
            }
        }

        private static long Solve_1()
        {
            var coords = Folds[0].Execute(Coords);
            return coords.Count;
        }
        private static void Solve_2()
        {
            foreach(var f in Folds)
            {
                Coords = f.Execute(Coords);
            }

            var w = Coords.Max(c => c.x)+1;
            var h = Coords.Max(c => c.y)+1;

            var pixels = new byte[h,w];
            foreach (var c in Coords)
            {
                pixels[c.y, c.x] = 1; 
            }

            for (var y = 0; y < h;y++)
            {
                for (var x = 0; x < w;x++)
                {
                    Console.Write(pixels[y, x] == 0 ? ' ' : '*');
                }
                Console.WriteLine();
            }
        }
    }

    record Coord
    {
        public int x;
        public int y;
    }
    class Fold
    {
        public int pos;
        public char dir;

        public List<Coord> Execute(List<Coord> coords)
        {
            if (dir == 'y')
            {
                foreach (var c in coords.Where(c => c.y > pos))
                {
                    c.y = 2 * pos - c.y;
                }
            }
            if (dir == 'x')
            {
                foreach (var c in coords.Where(c => c.x > pos))
                {
                    c.x = 2 * pos - c.x;
                }
            }
            // remove duplicates?
            var newcoords = coords.Distinct().ToList();
            return newcoords;
        }
    }
}
