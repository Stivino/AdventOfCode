using System.Collections;

namespace AdventOfCode
{

    public class Day_05 : BaseDay
    {
        List<Tuple<int, int>> From = new();
        List<Tuple<int, int>> To = new();

        public Day_05()
        {
            foreach (var item in File.ReadLines(InputFilePath)) {
                List<int> coordinates = item.Replace(" -> ", ",").Split(',').Select(x => int.Parse(x)).ToList();
                From.Add(new Tuple<int, int>(coordinates[0], coordinates[1]));
                To.Add(new Tuple<int, int>(coordinates[2], coordinates[3]));
            }
        }

        public override ValueTask<string> Solve_1()
        {

            SeaMap map = new SeaMap();
            for (int i = 0; i < From.Count; i++) {
                map.MarkVent(From[i], To[i]);
            }
            Console.Write(map.ToString());
            return new(map.GetNumOfCriticalAreas().ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            SeaMap map = new SeaMap();
            for (int i = 0; i < From.Count; i++) {
                map.MarkVent(From[i], To[i], true);
            }
            Console.Write(map.ToString());
            return new(map.GetNumOfCriticalAreas().ToString());
        }

        internal class SeaMap
        {
            const int MAP_SIZE = 1000;
            
            public int[,] map = new int[MAP_SIZE, MAP_SIZE];

            

            public SeaMap()
            {
                for (int i = 0; i < MAP_SIZE; i++) {
                    for (int j = 0; j < MAP_SIZE; j++) {
                        map[i, j] = 0;
                    }
                }
            }

            public void MarkVent(Tuple<int, int> from, Tuple<int, int> to, bool includeDiagonales = false)
            {
                if (from.Item1 == to.Item1) {
                    int x = from.Item1;
                    int y = GetLowest(from.Item2, to.Item2);
                    while (y <= GetBiggest(from.Item2, to.Item2)) {
                        map[x, y++] += 1;
                    }
                }
                else if (from.Item2 == to.Item2) {
                    int y = from.Item2;
                    int x = GetLowest(from.Item1, to.Item1);
                    while (x <= GetBiggest(from.Item1, to.Item1)) {
                        map[x++, y] += 1;
                    }
                }
                else if (includeDiagonales) {  // Diagonalen
                    int x = from.Item1;
                    int y = from.Item2;
                    do {
                        map[x, y] += 1;
                        if (x < to.Item1) x++; else x--;
                        if (y < to.Item2) y++; else y--;
                    } while (x != to.Item1 && y != to.Item2);
                    map[x, y] += 1;
                }
            }


            public int GetNumOfCriticalAreas()
            {
                int areas = 0;
                for (int i = 0; i < MAP_SIZE; i++) {
                    for (int j = 0; j < MAP_SIZE; j++) {
                        if (map[i, j] >= 2)
                            areas++;
                    }
                }
                return areas;
            }

            public override string ToString()
            {
                if (MAP_SIZE > 10) 
                    return "";
                string s = "";
                for (int i = 0; i < MAP_SIZE; i++) {
                    for (int j = 0; j < MAP_SIZE; j++) {
                        int vent = map[j, i];
                        s += vent > 0 ? vent : ".";
                    }
                    s += "\r\n";
                }
                return s;
            }

            private int GetLowest(int one, int two)
            {
                return one < two ? one : two;
            }

            private int GetBiggest(int one, int two)
            {
                return one > two ? one : two;
            }
        }
    }
}

