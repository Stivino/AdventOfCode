namespace AdventOfCode
{

    public class Day_02 : BaseDay
    {
        private List<Tuple<string, string>> _rounds = new List<Tuple<string, string>>();

        private Dictionary<string, int> _points = new Dictionary<string, int>()
        {
           { "A X", 4}, // R R 1 + 3 = 4
           { "A Y", 8}, // R P 2 + 6 = 8
           { "A Z", 3}, // R S 3 + 0 = 3
           { "B X", 1}, // P R 1 + 0 = 1
           { "B Y", 5}, // P P 2 + 3 = 5 
           { "B Z", 9}, // P S 3 + 6 = 9
           { "C X", 7}, // S R 1 + 6 = 7
           { "C Y", 2}, // S P 2 + 0 = 2
           { "C Z", 6}  // S S 3 + 3 = 6
        };
        List<string> _games;

        public enum Shape { Rock = 1, Paper = 2, Scissor = 3 }

        public Day_02()
        {
            _games = File.ReadAllLines(InputFilePath).ToList();
            foreach (string line in File.ReadAllLines(InputFilePath))
            {
                _rounds.Add(new Tuple<string, string>(line.Split(" ").First(), line.Split(" ").Last()));
            }
        }

        private int PointsPerRound(string left, string right)
        {
            int points = (int)GetShape(right);
            Shape l = GetShape(left);
            Shape r = GetShape(right);
            if (l == r)
                points += 3;
            else if (r > l)
                points += 6;
            else
                points += 0;
            return points;
        }

        public Shape GetShape(string shape)
        {
            switch (shape)
            {
                case "X":
                case "A":
                    return Shape.Rock;
                case "Y":
                case "B":
                    return Shape.Paper;
                case "Z":
                case "C":
                    return Shape.Scissor;
                default:
                    return Shape.Rock;
            }
        }

        public override ValueTask<string> Solve_1()
        {
            int points = 0;
            //foreach (var tup in _rounds)
            //{
            //    points += PointsPerRound(tup.Item1, tup.Item2);
            //}
            foreach (var line in _games)
            {
                points += _points[line];
            }
            return new ValueTask<string>(points.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            for (int i = 0; i < _games.Count; i++)
            {
                switch (_games[i].First())
                {
                    case 'A':   // rock
                        switch (_games[i].Last())
                        {
                            case 'X':   // lose
                                _games[i] = "A Z";
                                break;
                            case 'Y':   // draw
                                _games[i] = "A X";
                                break;
                            case 'Z':   // win
                                _games[i] = "A Y";
                                break;
                        }
                        break;
                    case 'B':   // paper
                        switch (_games[i].Last())
                        {
                            case 'X':
                                _games[i] = "B X";
                                break;
                            case 'Y':
                                _games[i] = "B Y";
                                break;
                            case 'Z':
                                _games[i] = "B Z";
                                break;
                        }
                        break;
                    case 'C':   // scissor
                        switch (_games[i].Last())
                        {
                            case 'X':
                                _games[i] = "C Y";
                                break;
                            case 'Y':
                                _games[i] = "C Z";
                                break;
                            case 'Z':
                                _games[i] = "C X";
                                break;
                        }

                        break;
                    default:
                        break;
                }
            }
            int points = 0;
            foreach (var line in _games)
            {
                points += _points[line];
            }
            return new ValueTask<string>(points.ToString());
        }
    }
}
