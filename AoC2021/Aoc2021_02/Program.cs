namespace AoC2021_02
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = File.ReadAllLines(@"input.txt");

            Console.WriteLine(Part1(commands));
            Console.WriteLine(Part2(commands));

            Console.ReadKey();
        }

        private static int Part1(string[] commands)
        {
            var depth = 0;
            var distance = 0;

            foreach (var command in commands)
            {
                var parts = command.Split(' ');
                switch (parts[0], int.Parse(parts[1]))
                {
                    case ("down", var value): depth += value; break;
                    case ("up", var value): depth -= value; break;
                    case ("forward", var value): distance += value; break;
                }
            }
            return depth*distance;
        }

        private static int Part2(string[] commands)
        {
            var aim = 0;
            var distance = 0;
            var depth = 0;

            foreach (var command in commands)
            {
                var parts = command.Split(' ');
                switch (parts[0], int.Parse(parts[1]))
                {
                    case ("down", var value): aim += value; break;
                    case ("up", var value): aim -= value; break;
                    case ("forward", var value): distance += value; depth += aim * value; break;
                }
            }
            return depth * distance;
        }
    }
}