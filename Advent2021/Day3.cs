using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day3
    {
        public static int Run1()
        {
            var lines = File.ReadAllLines("input/day3.txt");

            var converted = lines.Select(l => l.Select(p => Convert.ToBoolean(int.Parse(p.ToString()))).ToArray()).ToArray();
            int bits = converted.Max(c => c.Length);
            string gamma = String.Empty;
            string epsilon = String.Empty;

            for (int i = 0; i < bits; i++)
            {
                int ones = converted.Select(p => p.ElementAt(i)).Where(b => b == true).Count();
                int zeros = converted.Select(p => p.ElementAt(i)).Where(b => b == false).Count();

                string g = ones > zeros ? "1" : "0";
                string e = ones > zeros ? "0" : "1";

                gamma += g;
                epsilon += e;
            }

            var gammaBytes = gamma.Select(g => Convert.ToByte(int.Parse(g.ToString()))).ToArray();
            var epsilonBytes = epsilon.Select(g => Convert.ToByte(int.Parse(g.ToString()))).ToArray();

            return GetByteFromBitArray(gammaBytes) * GetByteFromBitArray(epsilonBytes);

            // gamma rate * epsilon rate = power consumption
            // gamma = most common bit correspondsing position
            // epsilon = opposite of gamma
        }

        public static int Run2()
        {
            var lines = File.ReadAllLines("input/day3.txt");

            var converted = lines.Select(l => l.Select(p => Convert.ToBoolean(int.Parse(p.ToString()))).ToArray()).ToArray();

            int bits = converted.Max(c => c.Length);

            var oxgen = converted.ToList();

            for (int i = 0; i < bits; i++)
            {
                List<bool[]> ones = new List<bool[]>();
                List<bool[]> zeros = new List<bool[]>();

                for (int t = 0; t < oxgen.Count; t++)
                {
                    if (oxgen[t][i] == true) ones.Add(oxgen[t]);
                    else zeros.Add(oxgen[t]);
                }

                if (ones.Count > zeros.Count || ones.Count == zeros.Count)
                {
                    oxgen = ones.ToList();
                }
                else
                {
                    oxgen = zeros.ToList();
                }
            }

            var co2 = converted.ToList();

            for (int i = 0; i < bits; i++)
            {
                List<bool[]> ones = new List<bool[]>();
                List<bool[]> zeros = new List<bool[]>();

                for (int t = 0; t < co2.Count; t++)
                {
                    if (co2[t][i] == true) ones.Add(co2[t]);
                    else zeros.Add(co2[t]);
                }

                if (ones.Count > zeros.Count || ones.Count == zeros.Count)
                {
                    co2 = zeros.ToList();
                }
                else
                {
                    co2 = ones.ToList();
                }

                if (co2.Count == 1)
                    break;
            }

            return GetByteFromBitArray(co2[0]) * GetByteFromBitArray(oxgen[0]);
        }

        private static int GetByteFromBitArray(bool[] arr)
        {
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                result *= 2;
                int num = arr[i] == true ? 1 : 0;
                result += num;
            }

            return result;
        }

        private static int GetByteFromBitArray(byte[] arr)
        {
            int result = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                result *= 2;
                result += arr[i];
            }

            return result;
        }
    }
}
