namespace AoC2021_17
{
    class Program
    {
        static void Main(string[] args)
        {
            var areainfo = File.ReadAllLines("input.txt");
            // target area: x=20..30, y=-10..-5
            var coords = areainfo[0].Split("x=")[1].Split(", y=");
            // "20..30 "-10..-5"

            Solve_1(coords);

            Console.ReadKey();
        }

        private static void Solve_1(string[] data)
        {

            var area = new AocRect(
                int.Parse(data[0].Split("..")[0]),  // Left
                int.Parse(data[1].Split("..")[1]),  // Top
                int.Parse(data[0].Split("..")[1]),  // Right
                int.Parse(data[1].Split("..")[0])   // Bottom
            );
            // Part 1:
            // max Y velocity = -Bottom -1
            // max height = (maxyvelocity+1)*maxyvelocity/2
            var maxyvelocity = -area.B - 1;
            Console.WriteLine($"maxheight: {(maxyvelocity + 1) * maxyvelocity / 2}");

            // Part 2:
            // try all trajectories with y velocity [Bottom .. maxyvelocity]
            // and                       x velocity [ Floor(sqr(Left*2)) .. Right]
            var minxvelocity = (int)(Math.Sqrt(area.L * 2));
            var hittingvectors = 0;
            for (var vx = minxvelocity; vx <= area.R; vx++)
                for(var vy = area.B; vy <= maxyvelocity; vy++)
                {
                    var hit = false;
                    // always start at 0,0
                    var x = 0;
                    var y = 0;
                    var dy = vy;
                    var dx = vx;
                    do
                    {
                        x += dx;
                        y += dy;
                        if (dx > 0) dx--;
                        dy--;

                        hit = area.Contains(x, y);
                    } while (!hit && (x <= area.R) && (y >= area.B));
                    if (hit) hittingvectors++;
                }
            Console.WriteLine($"hittingvelocities: {hittingvectors}");
        }



    }
    class AocRect
    {
        public readonly int L, R, T , B;
        public AocRect(int l, int t, int r, int b)
        {
            L = l;
            R = r;
            T = t;
            B = b;
        }

        public bool Contains (int x, int y)
        {
            return (x >= L && x <= R && y>= B && y<= T);
        }
    }
}
