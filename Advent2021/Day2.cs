using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day2
    {
        public static int Run1()
        {
            int pos = 0;
            int depth = 0;

            var lines = File.ReadAllLines("input/day2.txt");
            var commands = lines.Select(l => GetCommand(l)).ToArray();

            foreach (var c in commands)
            {
                if (c.Key == Direction.Up)
                {
                    depth -= c.Value;
                    if (depth <= 0) depth = 0;
                }
                else if (c.Key == Direction.Down)
                {
                    depth += c.Value;
                }
                else if (c.Key == Direction.Forward)
                {
                    pos += c.Value;
                }
            }
            return pos * depth;
        }

        public static int Run2()
        {
            int pos = 0;
            int depth = 0;
            int aim = 0;

            var lines = File.ReadAllLines("input/day2.txt");
            var commands = lines.Select(l => GetCommand(l)).ToArray();

            foreach (var c in commands)
            {
                if (c.Key == Direction.Up)
                {
                    aim -= c.Value;
                }
                else if (c.Key == Direction.Down)
                {
                    aim += c.Value;
                }
                else if (c.Key == Direction.Forward)
                {
                    pos += c.Value;
                    depth += c.Value * aim;
                }
            }

            return pos * depth;
        }

        enum Direction
        {
            Up,
            Down,
            Forward,
        }

        private static KeyValuePair<Direction, int> GetCommand(string line)
        {
            var split = line.Split(' ');

            if (split[0][0] == 'f')
            {
                return new KeyValuePair<Direction, int>(Direction.Forward, Convert.ToInt32(split[1]));
            }
            else if (split[0][0] == 'd')
            {
                return new KeyValuePair<Direction, int>(Direction.Down, Convert.ToInt32(split[1]));
            }
            else if (split[0][0] == 'u')
            {
                return new KeyValuePair<Direction, int>(Direction.Up, Convert.ToInt32(split[1]));
            }
            else
            {
                throw new Exception("wtf");
            }
        }
    }
}
