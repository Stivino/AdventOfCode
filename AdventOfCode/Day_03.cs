using System.Collections;

namespace AdventOfCode
{

    public class Day_03 : BaseDay
    {

        private readonly string[] _input;


        public Day_03()
        {
            _input = File.ReadAllLines(InputFilePath).ToArray();
        }

        public override ValueTask<string> Solve_1()
        {
            int gamma = 0;
            int epsilon = 0;
            int len = _input.First().Length;
            int zeros = 0, ones = 0;
            string sgamma = "";
            for (int i = 0; i < len; i++) {     // Jede Stelle
                zeros = 0;
                ones = 0;
                for (int j = 0; j < _input.Length; j++) {       // Jede Zeile
                    if (_input[j][i] == '1') ones++;
                    else zeros++;
                }
                sgamma += ones > zeros ? "1" : "0";
                //gamma += ones > zeros ? 2 ^ j : 0;
            }
            string sepsilon = "";
            for (int i = 0; i < sgamma.Length; i++) {
                sepsilon += sgamma[i] == '0' ? "1" : "0";
            }

            gamma = Convert.ToInt32(sgamma, 2);
            epsilon = Convert.ToInt32(sepsilon, 2);
            return new((epsilon * gamma).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string oxygen = "";
            List<string> oxygens = new(_input);
            List<string> co2s = new(_input);
            string co2 = "";
            int i = 0;

            while (oxygens.Count > 1) {
                GetOxygen(ref oxygens, ref i);
            }
            i = 0;
            while (co2s.Count > 1) {
                GetCO2(ref co2s, ref i);
            }
            co2 = co2s.First();
            oxygen = oxygens.First();

            return new((Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2)).ToString());
        }



        private void GetOxygen(ref List<string> list, ref int index)
        {
            int i = index;
            var reduced1 = list.Where(x => x[i] == '1').ToList();
            var reduced0 = list.Where(x => x[i] == '0').ToList();
            index++;
            list = (reduced1.ToList().Count >= reduced0.ToList().Count) ? reduced1 : reduced0;
        }

        private void GetCO2(ref List<string> list, ref int index)
        {
            int i = index;
            var reduced1 = list.Where(x => x[i] == '1').ToList();
            var reduced0 = list.Where(x => x[i] == '0').ToList();
            index++;
            list = (reduced0.ToList().Count <= reduced1.ToList().Count) ? reduced0 : reduced1;
        }

    }
}
