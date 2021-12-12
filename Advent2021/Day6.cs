using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day6
    {

        public static int Run1()
        {
            return FishiesForDays(80);
        }

        public static ulong Run2()
        {
            return WontKillYourMemoryFishies(256);
        }

        class Fish
        {
            public byte InternalTimer { get; set; }
            public Fish(byte timer)
            {
                this.InternalTimer = timer;
            }
        }

        // this could be done using an array of longs without typed names, i like this better though it helped me figure it out visually
        private static ulong WontKillYourMemoryFishies(int days)
        {
            var lines = File.ReadAllLines("input/day6.txt");
            int daysToLive = days;
            var fishtimes = lines[0].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();
          
         

            ulong zeroDays = 0, oneDay = 0, twoDay = 0, threeDay = 0, fourDay = 0, fiveDay = 0, sixDay = 0, sevenDay = 0, eightDay = 0;
            ulong newSpawns = 0;

            foreach (var fish in fishtimes)
            {
                if (fish == 0) zeroDays++;
                if (fish == 1) oneDay++;
                if (fish == 2) twoDay++;
                if (fish == 3) threeDay++;
                if (fish == 4) fourDay++;
                if (fish == 5) fiveDay++;
                if (fish == 6) sixDay++;
                if (fish == 7) sevenDay++;
                if (fish == 8) eightDay++;
            }

            int day = 0;
            do
            {
                newSpawns = zeroDays;
                zeroDays = oneDay;
                oneDay = twoDay;
                twoDay = threeDay;
                threeDay = fourDay;
                fourDay = fiveDay;
                fiveDay = sixDay;
                sixDay = sevenDay;
                sevenDay = eightDay;

                // any new spawn becomes 6ers
                sixDay += newSpawns;
                eightDay = newSpawns;

                day++;
            } while (day < daysToLive);

            return zeroDays + oneDay + twoDay + threeDay + fourDay + fiveDay + sixDay + sevenDay + eightDay;

        }

        /// <summary>
        /// killed my cpu after like 160 days
        /// </summary>
        private static int FishiesForDays(int days)
        {
            var lines = File.ReadAllLines("input/day6.txt");
            int daysToLive = days;
            var fishtimes = lines[0].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();

            List<Fish> Fishies = new List<Fish>();
            List<Fish> NewFishies = new List<Fish>();

            foreach (var fishtime in fishtimes)
            {
                var fish = new Fish((byte)fishtime);
                Fishies.Add(fish);
            }

            int day = 0;
            do
            {
                for (int i = 0; i < Fishies.Count; i++)
                {
                    Fishies[i].InternalTimer--;

                    // the bytes roll up to 255 when you go down from zero
                    if (Fishies[i].InternalTimer < 0 || Fishies[i].InternalTimer == 255)
                    {
                        NewFishies.Add(new Fish(8));
                        Fishies[i].InternalTimer = 6;
                    }
                }

                Fishies.AddRange(NewFishies);
                NewFishies.Clear();
                day++;
            }
            while (day < (daysToLive - 1));

            return Fishies.Count;
        }
    }
}
