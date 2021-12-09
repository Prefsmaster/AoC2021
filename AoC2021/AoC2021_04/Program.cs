namespace AoC2021_04
{
    class Program
    {

        static void Main(string[] args)
        {

            var file = new StreamReader(@"input.txt");

            var BingoNumbers = file.ReadLine().Split(',').Select(int.Parse).ToArray();

            var BingoCards = ReadBingoCards(file);

            Console.WriteLine(Part1(BingoNumbers, BingoCards));
            Console.WriteLine(Part2(BingoNumbers, BingoCards));
        }

        private static int Part1(int[] bingonumbers, List<BingoCard> bingocards)
        {
            BingoCard? winner = null;
            var numberindex = 0;
            while (winner == null && numberindex < bingonumbers.Length  )
            {
                bingocards.ForEach(card => card.CheckNumber(bingonumbers[numberindex]));
                winner = bingocards.SingleOrDefault(card => card.HasBingo());
                if (winner == null) 
                    numberindex++;
            }

            if (winner!=null)
            {
                var sum = winner.GetSumNotDrawn();
                return bingonumbers[numberindex] * sum;
            }

            return -1;
        }

        private static int Part2(int[] bingonumbers, List<BingoCard> bingocards)
        {
            BingoCard? lastwinner = null;
            var lastwinningnumber = 0;

            for (int n=0;n< bingonumbers.Length; n++)
            {
                foreach(BingoCard card in bingocards)
                {
                    if (card.CheckNumber(bingonumbers[n]) && card.HasBingo())
                    {
                        lastwinner = card;
                        lastwinningnumber = bingonumbers[n];
                        card.active = false;
                    }
                }
            }

            if (lastwinner != null)
            {
                var sum = lastwinner.GetSumNotDrawn();
                return lastwinningnumber * sum;
            }
            return -1;
        }


        private static List<BingoCard> ReadBingoCards(StreamReader file)
        {
            var cards = new List<BingoCard>();
            // skip empty line
            while (file.ReadLine() != null)
            {
                var lines = new string[5];
                for (int line = 0; line < 5; line++)
                    lines[line] = file.ReadLine();
            
                cards.Add(new BingoCard(lines));
            }
            return cards;   
        }

        private class BingoCard
        {
            int[][] values;
            bool[][] drawn;

            int[] rowdrawn;
            int[] coldrawn;

            public bool active;

            public BingoCard(string[] numbers)
            {
                active = true;

                values = new int[numbers.Length][];
                drawn = new bool[numbers.Length][];
                rowdrawn = new int[numbers.Length];
                for (var i=0;i<numbers.Length;i++)
                {
                    var parts = GetParts(numbers[i], 5,2,3);
                    values[i] = parts.Select(int.Parse).ToArray();
                    drawn[i] = new bool[values[i].Length];
                }
                coldrawn = new int[values[0].Length];
            }

            public bool CheckNumber (int number)
            {
                if (!active) return false;
 
                for (int x=0;x<values[0].Length;x++)
                {
                    for (int y=0;y<values.Length; y++)
                    {
                        if (values[y][x] == number && !drawn[y][x])
                        {
                            rowdrawn[y]++;
                            coldrawn[x]++;
                            drawn[y][x] = true;
                            return true;
                        }
                    }
                }
                return false;
            }
            public int GetSumNotDrawn()
            {
                var sum = 0;
                for (int x = 0; x < values[0].Length; x++)
                {
                    for (int y = 0; y < values.Length; y++)
                    {
                        if (!drawn[y][x])
                        {
                            sum += values[y][x];
                        }
                    }
                }
                return sum;
            }
            public bool HasBingo()
            {
                return active && (rowdrawn.Any(x => x == values[0].Length) || coldrawn.Any(y => y == values.Length));    
            }

            private string[] GetParts (string str, int parts, int partSize,int stepSize)
            {
                return Enumerable.Range(0, parts)
                    .Select(i => str.Substring(i * stepSize, partSize)).ToArray();
            }
        }
    }
}