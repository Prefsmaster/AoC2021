namespace AoC2021_13
{
    class Program
    {
        private static string StartSequence;
        private static Dictionary<string, string> Inserts = new();
        private static Dictionary<string, (string,string)> Inserts2 = new();
        static void Main(string[] args)
        {
            BuildInsertData("input.txt");

            Solve_1();
            Solve_2(10);
            Solve_2(40);

            Console.ReadKey();
        }

        private static void BuildInsertData(string filename)
        {
            var file = new StreamReader(filename);

            StartSequence = file.ReadLine();
            string input;
            while ((input = file.ReadLine()) != null)
            {
                if (input.Contains('>'))
                {
                    var parts = input.Split(" -> ");
                    Inserts.Add(parts[0], parts[0][0] + parts[1]);
                    Inserts2.Add(parts[0], ( parts[0][0] + parts[1] , parts[1] + parts[0][1] ));
                }
            }
        }

        private static void Solve_1()
        {
            // brute force
            var sequence = StartSequence;
            for (int steps = 0; steps < 10; steps++)
            {
                var sb = new System.Text.StringBuilder();
                for (int i = 0; i < sequence.Length - 1; i++)
                {
                    var key = sequence.Substring(i, 2);
                    sb.Append(Inserts.ContainsKey(key) ? Inserts[key] : sequence[i]);
                }
                sb.Append(sequence[sequence.Length - 1]);
                sequence = sb.ToString();
                Console.WriteLine(sequence);
            }
            var counts = sequence.GroupBy(x => x).Select(x => x.Count()).OrderBy(x => x);
            var max = counts.Last();
            var min = counts.First();
            Console.WriteLine(max - min);
        }

        private static void Solve_2(int steps)
        {
            // better way, count pairs!
            Dictionary<string, ulong> Pairs = new();
            for (var  i=0;i<StartSequence.Length-1;i++)
            {
                var key = StartSequence.Substring(i, 2);
                if (Pairs.ContainsKey(key))
                    Pairs[key]++;
                else
                    Pairs.Add(key, 1);
            }
            // now apply the rules..
            for (var step = 0; step < steps; step++)
            {
                // clone the current state 
                var NewPairs = Pairs.ToDictionary(entry => entry.Key, entry => entry.Value);

                foreach (var insert in Inserts2)
                {
                    if (Pairs.ContainsKey(insert.Key))
                    {
                        // remove old pairs
                        var pairsaffected = Pairs[insert.Key];
                        NewPairs[insert.Key] -= pairsaffected;
                        // add/increment created pairs
                        if (NewPairs.ContainsKey(insert.Value.Item1))
                            NewPairs[insert.Value.Item1] += pairsaffected;
                        else
                            NewPairs.Add(insert.Value.Item1, pairsaffected);

                        if (NewPairs.ContainsKey(insert.Value.Item2))
                            NewPairs[insert.Value.Item2] += pairsaffected;
                        else
                            NewPairs.Add(insert.Value.Item2, pairsaffected);
                    }
                }
                Pairs = NewPairs;
            }
            var charcounts = new ulong[26];
            // count characters
            foreach(var pair in Pairs)
            {
                charcounts[pair.Key[0] - 'A'] += pair.Value;
                charcounts[pair.Key[1] - 'A'] += pair.Value;
            }
            var counts = charcounts.Where(v => v != 0).Select(v => (v + 1) / 2).OrderBy(v => v);
            Console.WriteLine(counts.Last() - counts.First());
        }
    }
}
