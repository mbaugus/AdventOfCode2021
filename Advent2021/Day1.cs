using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day1
    {

        public static int Run1()
        {
            var lines = File.ReadAllLines("input/day1.txt");
            var depths = lines.Select(p => Convert.ToInt32(p)).ToArray();
            int number = 0;
            for (int i = 0; i < depths.Length; i++)
            {
                if (i == 0) continue;

                if (depths[i] > depths[i - 1]) number++;
            }

            return number;
        }

        public static int Run2()
        {
            var lines = File.ReadAllLines("input/day1.txt");
            var depths = lines.Select(p => Convert.ToInt32(p)).ToArray();
            int number = 0;

            List<int> depthCombined = new List<int>();
            int sumMarker = 3;

            for (int i = 0; i < depths.Length; i++)
            {
                int total = 0;
                for (int n = 0; n < sumMarker && (n + i) < (depths.Length); n++)
                {
                    total += depths[i + n];
                }

                depthCombined.Add(total);
            }

            for (int i = 0; i < depthCombined.Count; i++)
            {
                if (i == 0) continue;

                if (depthCombined[i] > depthCombined[i - 1]) number++;
            }

            return number;
        }
    }
}
