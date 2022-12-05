namespace AdventOfCode
{

    public class Day_01 : BaseDay
    {
        private readonly int[] _input;



        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath).Select(x => x == String.Empty ? x = "0" : x = x).Select(x => Convert.ToInt32(x)).ToArray();
        }

        public override ValueTask<string> Solve_1()
        {
            int elf = 0;
            int fattestElf = 0;
            foreach (int line in _input)
            {
                elf += line;
                if (line == 0)
                {
                    fattestElf = elf > fattestElf ? elf : fattestElf;
                    elf = 0;
                }
            }
            return new(fattestElf.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            List<int> elfes = new List<int>();
            int elf = 0;
            foreach (int line in _input)
            {
                elf += line;
                if (line == 0)
                {
                    elfes.Add(elf);
                    elf = 0;
                }
            }
            elfes.Sort();
            int cnt = elfes.Count;
            int topThree = elfes[cnt - 1] + elfes[cnt - 2] + elfes[cnt - 3];
            return new(topThree.ToString());
        }
    }
}
