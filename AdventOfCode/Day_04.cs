using System.Collections;

namespace AdventOfCode
{

    public class Day_04 : BaseDay
    {

        private readonly string[] _input;

        private BingoController Bingo;


        public Day_04()
        {
            HashSet<int> WinnerNumbers;

            List<int[,]> Boards = new();
            WinnerNumbers = new HashSet<int>(File.ReadLines(InputFilePath)
                                    .First()
                                    .Split(',')
                                        .Select(x => int.Parse(x)));
            HashSet<string> lines = File.ReadAllLines(InputFilePath).Skip(2).ToHashSet();
            lines.RemoveWhere(x => x.Trim() == String.Empty);
            int[,] board = new int[5, 5];

            int j = 0;
            for (int i = 0; i < lines.Count; i++) {
                if (i % 5 == 0 && i > 0) {
                    Boards.Add(board);
                    board = new int[5, 5];
                    j = 0;
                }
                int[] row = lines.ElementAt(i).Trim().Replace("  ", " ").Split(' ').Select(x => int.Parse(x)).ToArray<int>();
                for (int k = 0; k < row.Length; k++) {
                    board[j, k] = row[k];
                }
                j++;
                if (i + 1 == lines.Count) {
                    Boards.Add(board);
                }
            }
            Bingo = new(WinnerNumbers, Boards);
        }

        public override ValueTask<string> Solve_1()
        {
            string result = Bingo.GetWinnersSum().ToString();
            return new(result);
        }

        public override ValueTask<string> Solve_2()
        {
            string result = Bingo.GetLastWinnersSum().ToString();
            return new(result);
        }


        internal class BingoBoard
        {
            public int[,] Board;

            public BingoBoard(int[,] numbers)
            {
                Board = numbers;
            }

            public bool HasWon(HashSet<int> numbers)
            {
                bool won = false;
                for (int i = 0; i < 5; i++) {
                    for (int j = 0; j < 5; j++) {
                        if (numbers.Contains(Board[i, j]))
                            won = true;
                        else {
                            won = false;
                            break;
                        }
                    }
                    if (won) {
                        return won;
                    }
                }

                for (int i = 0; i < 5; i++) {
                    for (int j = 0; j < 5; j++) {
                        if (numbers.Contains(Board[j, i]))
                            won = true;
                        else {
                            won = false;
                            break;
                        }
                    }
                    if (won) {
                        return won;
                    }
                }
                return false;
            }

            public int UnmarkedSum(HashSet<int> numbers)
            {
                int sum = 0;
                for (int i = 0; i < 5; i++) {
                    for (int j = 0; j < 5; j++) {
                        int i_num = Board[i, j];

                        if (!numbers.Contains(i_num)) {
                            sum += i_num;
                        }
                    }
                }
                return sum * numbers.Last();
            }

            public override string ToString()
            {
                string s = String.Empty;
                for (int i = 0; i < 5; i++) {
                    for (int j = 0; j < 5; j++) {
                        s += Board[i, j].ToString().PadRight(2, ' ') + " ";
                    }
                    s += " | ";
                }
                return s;
            }


            private void Print(int num, bool marked = false)
            {
                if (marked) {
                    Console.ForegroundColor = ConsoleColor.Green;

                }
                Console.Write(num.ToString().PadRight(2, ' ') + " ");
                Console.ResetColor();
            }
        }


        internal class BingoController
        {
            private HashSet<int> Numbers;

            private List<BingoBoard> Boards = new();

            public BingoController(HashSet<int> nums, List<int[,]> boards)
            {
                foreach (var item in boards) {
                    Boards.Add(new(item));
                }
                Numbers = nums;
            }

            public int GetWinnersSum()
            {
                HashSet<int> numbs = new();

                for (int i = 0; i < Numbers.Count; i++) {
                    numbs.Add(Numbers.ElementAt(i));
                    foreach (var board in Boards) {
                        if (board.HasWon(numbs)) {
                            return board.UnmarkedSum(numbs);
                        }
                    }
                }
                return -1;
            }

            public int GetLastWinnersSum()
            {
                
                HashSet<int> numbs = new();
                List<BingoBoard> lefts = new();
                for (int i = 0; i < Numbers.Count; i++) {
                    lefts = Boards.Where(x => !x.HasWon(numbs)).ToList();
                    numbs.Add(Numbers.ElementAt(i));
                    if (lefts.Count == 1 && lefts.First().HasWon(numbs)) {
                        return lefts.First().UnmarkedSum(numbs);
                    }
                }
                return -1;
            }
        }

    }

}
