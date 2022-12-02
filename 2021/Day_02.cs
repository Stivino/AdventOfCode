namespace AdventOfCode
{

    public class Day_02 : BaseDay
    {

        private readonly string[][] _input;


        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath).Select(line => line.Split(' ')).ToArray();
        }

        public override ValueTask<string> Solve_1()
        {
            Int64 horizontal = 0;
            Int64 vertical = 0;

            for (int i = 0; i < _input.Length; i++) {
                int val = int.Parse(_input[i][1]);
                switch (_input[i][0]) {
                    case "forward":
                        horizontal += val;
                        break;
                    case "down":
                        vertical += val;
                        break;
                    case "up":
                        vertical -= val;
                        break;
                    default:
                        break;
                }
            }
            return new((horizontal * vertical).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            Int64 horizontal = 0;
            Int64 vertical = 0;
            Int64 aim = 0;

            for (int i = 0; i < _input.Length; i++) {
                int val = int.Parse(_input[i][1]);
                switch (_input[i][0]) {
                    case "forward":
                        horizontal += val;
                        if (aim <= 0)
                            break;
                        vertical += aim * val;
                        break;
                    case "down":
                        aim += val;
                        break;
                    case "up":
                        aim -= val;
                        break;
                    default:
                        break;
                }
            }
            return new((horizontal * vertical).ToString());
        }
    }
}
