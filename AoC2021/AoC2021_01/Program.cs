namespace AoC2021_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = File.ReadAllLines(@"input.txt").Select(int.Parse).ToList();

            Console.WriteLine(Part1(values));
            Console.WriteLine(Part2(values));

            Console.ReadKey();
         }

        private static int Part1(List<int> numbers)
        {
            return numbers.Zip(numbers.Skip(1), (a, b) => a < b).Count(x => x);
        }

        private static int Part2(List<int> numbers)
        {
            var skipped = numbers.Zip(numbers.Skip(3), (a, b) => a < b);
            return skipped.Count(x => x);
        }
    }
}




