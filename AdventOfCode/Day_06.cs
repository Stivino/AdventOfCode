using System.Collections;
using System.Linq;

namespace AdventOfCode
{

    public class Day_06 : BaseDay
    {
        
        readonly List<int> Laternfish = new();
        readonly Dictionary<int, ulong> Fish = new();
        readonly int DaysOfReproduction = 80;
        public Day_06()
        {
            Laternfish.AddRange(File.ReadLines(InputFilePath).First().Split(',').Select(x => int.Parse(x)).ToArray());  
            for (int i = 0; i < 9; i++) {
                Fish.Add(i, 0);
                for (int j = 0; j < Laternfish.Count; j++) {
                    if (i == Laternfish[j]) {
                        Fish[i]++;
                    }
                }
            }
        }

        /// <summary>
        /// Currently slow as hell. Maybe because of the List. To lazy to improve speed, it's christmas time!
        /// </summary>
        /// <returns></returns>
        public override ValueTask<string> Solve_1()
        {
            int newFishes = 0;
            for (int i = 0; i < DaysOfReproduction; i++) {
                for (int j = 0; j < Laternfish.Count; j++) {
                    if (Laternfish[j] == 0)
                        Laternfish[j] = 6;
                    else
                        Laternfish[j]--;
                }
                while (newFishes > 0) { Laternfish.Add(8); newFishes--; }
                newFishes = Laternfish.Where(x => x == 0).ToList().Count;
            }
            return new(Laternfish.Count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            ulong newFish = 0;
            ulong restart = 0;
            for (int i = 0; i < 256; i++) {
                restart = Fish[0];
                Fish[0] = Fish[1];
                Fish[1] = Fish[2];
                Fish[2] = Fish[3];
                Fish[3] = Fish[4];
                Fish[4] = Fish[5];
                Fish[5] = Fish[6];
                Fish[6] = Fish[7] + restart;
                Fish[7] = Fish[8];
                Fish[8] = newFish;
                newFish = Fish[0];
            }
            ulong sum = 0;
            foreach (var fish in Fish.Values) {
                sum += fish;
            }
            return new(sum.ToString());
        }
    }
}
