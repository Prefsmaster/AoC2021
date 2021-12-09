namespace AoC2021_05
{
    class Program
    {

        static void Main(string[] args)
        {

            var lines = File.ReadAllLines(@"input.txt");

            Console.WriteLine(SolveDay5(lines));
            Console.WriteLine(SolveDay5(lines, true));
        }

        private static int SolveDay5(IEnumerable<string> lines, bool diagonalsallowed = false)
        {
            Dictionary<string, int> points = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                // get start and endpoints... (ugly!)

                var coords = line.Split(" -> ");
                var pair = coords[0].Split(',');
                var xstart = int.Parse(pair[0]);
                var ystart = int.Parse(pair[1]);
                pair = coords[1].Split(',');
                var xend = int.Parse(pair[0]);
                var yend = int.Parse(pair[1]);

                // determine slopes
                var deltax = Math.Abs(xstart - xend);
                var deltay = Math.Abs(ystart - yend);

                if (((deltax == deltay) && diagonalsallowed) || deltay == 0 || deltax == 0)
                {
                    DrawLine(points, xstart, ystart, xend, yend);
                }
            }
            return points.Where(p => p.Value >= 2).Count();
        }

        private static void DrawLine(Dictionary<string, int> points, int xstart, int ystart, int xend, int yend)
        {
            var todraw = Math.Max(Math.Abs(xstart - xend)+1, Math.Abs(ystart - yend)+1);

            var dx = xstart != xend ? xend > xstart ? 1 : -1 : 0;
            var dy = ystart != yend ? yend > ystart ? 1 : -1 : 0;

            for (var p=0;p<todraw;p++,xstart+=dx,ystart+=dy)
            { 
                var key = $"{xstart:D3}{ystart:D3}";

                if (points.ContainsKey(key))
                    points[key]++;
                else
                    points.Add(key, 1);
            }
        }
    }
}