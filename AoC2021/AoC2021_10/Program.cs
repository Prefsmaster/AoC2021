namespace AoC2021_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var chunkdata = File.ReadAllLines(@"input.txt").ToArray();

            Solve_1_2(chunkdata);

            Console.ReadKey();
        }

        private static void Solve_1_2(string[] chunkdata)
        {
            var scores = new Dictionary<char, int> {
                {')',3 },
                {']',57 },
                {'}',1197 },
                {'>',25137 },
            };

            var opening = "([{<";
            var closing = ")]}>";
            
            var score = 0;
            var scorelist = new List<long>();

            foreach (var chunklist in chunkdata)
            {
                var stack = new Stack<char>();
                char badcloser = ' ';

                foreach (var c in chunklist)
                {
                    if (opening.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else 
                    {
                        var opener = stack.Pop();
                        if (opener != opening[closing.IndexOf(c)])
                        {
                            badcloser = c;
                            break;
                        }
                    }
                }
                if (badcloser != ' ')
                {
                    score += scores[badcloser];
                }
                else
                {
                    if (stack.Count > 0)
                    {
                        var score2 = 0L;
                        while(stack.Count > 0)
                        {
                            score2 = score2 * 5 + opening.IndexOf(stack.Pop())+1;
                        }
                        scorelist.Add(score2);
                    }
                }
            }
            Console.WriteLine($"Part 1: {score}");
            Console.WriteLine($"Part 2: {scorelist.OrderBy(x => x).ToList()[scorelist.Count / 2]}");
        }
    }
}
