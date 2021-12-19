namespace AoC2021_15
{
    class Program
    {
        private static string[] bitstreams;
        private static int sum;
        static void Main(string[] args)
        {
            bitstreams = File.ReadAllLines("input.txt");

            Solve_1(bitstreams[0]);

            Console.ReadKey();
        }

        private static void Solve_1(string data)
        {
            var bits = new BitStream(data);

            var result = CalcOperator(bits);

            Console.WriteLine($"Streamlength: {result.bitsread}");
            Console.WriteLine($"Versionsum: {sum}");
            Console.WriteLine($"result: {result.value}");
            Console.WriteLine();
        }


        private static (int bitsread, long value) ReadNumberOfPackets(BitStream bits, int packets, Operator op)
        {
            var bitsread = 0;
            var args = new List<long>();
            for (var p = 0; p < packets; p++)
            {
                var result = CalcOperator(bits);
                bitsread += result.bitsread;
                args.Add(result.value);
            }
            var value = CalcValue(args, op);
            return (bitsread, value);
        }

        private static (int bitsread, long value) ReadNumberOfBits(BitStream bits, int toread, Operator op)
        {
            var bitsread = 0;
            var args = new List<long>();
            while (bitsread != toread)
            {
                var result = CalcOperator(bits);
                bitsread += result.bitsread;
                args.Add(result.value);
            }
            var value = CalcValue(args, op);
            return (bitsread, value);
        }

        private static (int bitsread, long value) CalcOperator(BitStream bits)
        {
            var version = bits.ReadVersion;
            sum += version;
            var op = (Operator)bits.ReadType;
            var bitsread = 6;
            (int bitsread, long value) result;

            if (op == Operator.Val)
            {
                result = bits.ReadLiteral();
                Console.WriteLine($"Literal: {result.value}");
            }
            else
            {
                if (bits.ReadLengthType == 1)
                {
                    var subpackets = bits.ReadSubPackets;
                    bitsread += 12;

                    Console.WriteLine($"Operator {op} Subpackets {subpackets}");
                    result = ReadNumberOfPackets(bits, subpackets, op);

                }
                else
                {
                    var bitstoread = bits.ReadLength;
                    bitsread += 16;

                    Console.WriteLine($"Operator {op} Subbits {bitstoread}");
                    result = ReadNumberOfBits(bits, bitstoread, op);

                }
            }
            bitsread += result.bitsread;
            return (bitsread, result.value);
        }
        private static long CalcValue(List<long> args, Operator Op)
        {
            var result = 0L;
            switch (Op)
            {
                case Operator.Sum:
                    result = args.Sum();
                    break;
                case Operator.Product:
                    result = args.Aggregate((x, y) => x * y);
                    break;
                case Operator.Min:
                    result = args.Min();
                    break;
                case Operator.Max:
                    result = args.Max();
                    break;
                case Operator.GT:
                    result = args[0] > args[1] ? 1 : 0;
                    break;
                case Operator.LT:
                    result = args[0] < args[1] ? 1 : 0;
                    break;
                case Operator.EQ:
                    result = args[0] == args[1] ? 1 : 0;
                    break;

            }
            Console.WriteLine($"Result: {result}");
            return result;
        }
    }

    enum Operator
    {
        Sum = 0,
        Product = 1,
        Min = 2,
        Max = 3,
        Val = 4,
        GT = 5,
        LT = 6,
        EQ = 7,
    }

    class BitStream
    {
        private readonly string HexData;
        private int pos;

        public BitStream(string data)
        {
            HexData = data;
            Reset();
        }

        private void Reset()
        {
            pos = 0;
        }

        private int readvalue(int bits)
        {
            var val = 0;
            for (int i = 0; i < bits; i++)
            {
                val = (val << 1) + getnextbit();
            }
            return val;
        }
        private readonly string hexchars = "0123456789ABCDEF";
        private int getnextbit()
        {
            var hc = hexchars.IndexOf(HexData[pos / 4]);
            var mask = 1 << (3 - (pos % 4));
            var val = (hc & mask);
            pos++;
            return val == 0 ? 0 : 1;
        }

        public int ReadVersion => readvalue(3);
        public int ReadType => readvalue(3);

        public (int bitsread, long val) ReadLiteral()
        {
            var result = 0L;
            var moredata = true;
            var bitsread = 0;
            while (moredata)
            {
                moredata = readvalue(1) == 1;
                result = (result << 4) + readvalue(4);
                bitsread += 5;
            }
            return (bitsread, result);
        }
        public int ReadLengthType => readvalue(1);
        public int ReadLength => readvalue(15);
        public int ReadSubPackets => readvalue(11);
    }
}
