using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day7
    {
        public static int Run1()
        {
            var lines = File.ReadAllLines("input/day7.txt").ToArray();
            var positions = lines[0].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();
            int mmax = positions.Max();
            int mmin = positions.Min();

            Dictionary<int, int> costs = new Dictionary<int, int>();
            for(int i = mmin; i < mmax; i++)
            {
                int fuelCost = 0;
                for(int p = 0; p < positions.Length; p++)
                {
                    int crabsPosition = positions[p];
                    int cost = Math.Abs(i - crabsPosition);
                    fuelCost += cost;
                }
                costs[i] = fuelCost;
            }

            int lowest = int.MaxValue;
            int lowestkey = 0;
            foreach (var key in costs.Keys)
            {
                if(costs[key] < lowest)
                {
                    lowest = costs[key];
                    lowestkey = key;
                }
            }

            return lowest;   
        }

        
        public static int Run2()
        {
          
            var lines = File.ReadAllLines("input/day7.txt").ToArray();
            var positions = lines[0].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();
            int mmax = positions.Max();
            int mmin = positions.Min();

            // premature optimization
            Dictionary<int, int> preComputedCostTable = new Dictionary<int, int>();
            for (int i = 0; i < mmax+1; i++)
            {
                int cost = 0;
                for(int p = 0; p < i; p++)
                {
                    cost += (p + 1);
                }

                preComputedCostTable[i] = cost;
            }
        
            Dictionary<int, int> costs = new Dictionary<int, int>();
            for (int i = mmin; i < mmax; i++)
            {
                int fuelCost = 0;
                for (int p = 0; p < positions.Length; p++)
                {
                    int crabsPosition = positions[p];
                    int moves = Math.Abs(i - crabsPosition);
                    int cost = preComputedCostTable[moves];
                    fuelCost += cost;
                }
                costs[i] = fuelCost;
            }

            int lowest = int.MaxValue;
            int lowestkey = 0;
            foreach (var key in costs.Keys)
            {
                if (costs[key] < lowest)
                {
                    lowest = costs[key];
                    lowestkey = key;
                }
            }

            return lowest;
        }
    }
}
