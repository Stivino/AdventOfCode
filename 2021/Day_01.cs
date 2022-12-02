namespace AdventOfCode {

public class Day_01 : BaseDay
    {
        private readonly int[] _input;

        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath).Select(Int32.Parse).ToArray();
        }

        public override ValueTask<string> Solve_1() {
            List<int> depths = new List<int>();
            int lastDepth = -1;
            int increasedCtr = 0;
            foreach (int depth in _input) {
                if (lastDepth > 0 && depth > lastDepth) {
                    increasedCtr += 1;
                }
                lastDepth = depth;
            }
            return new (increasedCtr.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int lastDepth = -1;
            int increasedCtr = 0;
            int depth = 0;
            for (int i = 0; i < _input.Length - 2; i++) {
                depth = _input[i] + _input[i + 1] + _input[i + 2];
                if (lastDepth > 0 && depth > lastDepth)
                    increasedCtr++;
                lastDepth = depth;
            }
            return new(increasedCtr.ToString());
        }
    } 
}
