namespace AoC2021_08
{
    class Program
    {
        static void Main(string[] args)
        {
            var digitdata = File.ReadAllLines(@"input.txt").ToArray();

            Console.WriteLine(SolveDay8_1(digitdata));
            Console.WriteLine(SolveDay8_2(digitdata));

            Console.ReadKey();
        }

        private static long SolveDay8_1(string[] digitdata)
        {
            int simpledigits = 0;
            foreach(var digitline in digitdata)
            {
                var parts = digitline.Split(" | ");
                var digits = parts[1].Split(' ');

                // simpledigits += digits.Where(d => d.Length == 2 || d.Length == 3 || d.Length == 4 || d.Length == 7).Count();
                simpledigits += digits.Where(d => new[] { 2,3,4,7 }.Contains(d.Length)).Count();
            }
            return simpledigits;
        }

        private static long SolveDay8_2(string[] digitdata)
        {
            // dictionary:
            // key = <l><o1+o4>
            // l = length of string
            // o1+o4 = sum of # of overlapping segments with '4' digit and '1' digit.
            // this generates unique keys for digits with 5 and 6 segments.

            var digittable = new Dictionary<int, int> {
                //             dig seg o1 o4
                {65, 0 },   //  0   6   2  3  
                {53, 2 },   //  2   5   1  2
                {55, 3 },   //  3   5   2  3
                {54, 5 },   //  5   5   1  3
                {64, 6 },   //  6   6   1  3
                {66, 9 },   //  9   6   2  4
            };
            var total = 0;
            foreach (var digitline in digitdata)
            {
                var parts = digitline.Split(" | ");

                var alldigits = parts[0].Split(' ');
                var one = alldigits.Single(p => p.Length == 2);
                var four = alldigits.Single(p => p.Length == 4);

                var valuedigits = parts[1].Split(' ');

                var value = 0;
                foreach (var digitcode in valuedigits)
                {
                    var decodeddigit = 0;
                    switch (digitcode.Length)
                    {
                        case 2: decodeddigit = 1; break;
                        case 3: decodeddigit = 7; break;
                        case 4: decodeddigit = 4; break;
                        case 7: decodeddigit = 8; break;
                        case 5:
                        case 6:
                            {
                                var key = digitcode.Length * 10 + one.Intersect(digitcode).Count();
                                key += four.Intersect(digitcode).Count();
                                
                                decodeddigit = digittable[key];
                            }
                            break;
                    }
                    value = value * 10 + decodeddigit;
                }
                total += value;
                Console.WriteLine($"{value}");
            }
            return total;
        }
    }
}
