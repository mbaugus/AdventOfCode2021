using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day10
    {
        private static Dictionary<char, char> legalPairs = new Dictionary<char, char>()
        {
            { '{', '}' },
            { '[', ']' },
            { '(', ')' },
            { '<', '>' }
        };

        private static Dictionary<char, int> corruptpoints = new Dictionary<char, int>()
        {
            { '}', 1197 },
            { ']', 57 },
            { ')', 3 },
            { '>', 25137 }
        };

        private static Dictionary<char, int> autocompletepoints = new Dictionary<char, int>()
        {
            { '}', 3 },
            { ']', 2 },
            { ')', 1 },
            { '>', 4 }
        };

        public static int Run1()
        {
            Dictionary<char, int> numberOfIllegals = new Dictionary<char, int>()
            {
                { '}', 0 },
                { ']', 0 },
                { ')', 0 },
                { '>', 0 }
            };
            var lines = File.ReadAllLines("input/day10.txt");
            foreach (var line in lines)
            {
                Stack<char> progressLeftSide = new Stack<char>();
                foreach (char k in line)
                {
                    // if left side
                    if (legalPairs.ContainsKey(k))
                    {
                        progressLeftSide.Push(k);
                    }
                    // if right side the last left side must match
                    else
                    {
                        if(progressLeftSide.TryPeek(out char result))
                        {
                            if(legalPairs[result] == k) // left and right match
                            {
                                progressLeftSide.Pop();
                            }
                            else
                            {
                                // corrupt
                                numberOfIllegals[k] = numberOfIllegals[k] + 1;
                                break;
                            }
                        }
                        else
                        {
                            // incomplete 
                            break;
                        }
                    }
                    // if right side
                }
            }

            int tally = 0;
            foreach (var k in numberOfIllegals.Keys)
            {
                tally += (corruptpoints[k] * numberOfIllegals[k]);
            }

            return tally;
        }

        public static ulong Run2()
        {
            var lines = File.ReadAllLines("input/day10.txt");
            List<Stack<char>> incompleteLines = new List<Stack<char>>();
            foreach (var line in lines)
            {
                Stack<char> progressLeftSide = new Stack<char>();
                bool illegal = false;
                foreach (char k in line)
                {
                    // if left side
                    if (legalPairs.ContainsKey(k))
                    {
                        progressLeftSide.Push(k);
                    }
                    // if right side the last left side must match
                    else
                    {
                        if (progressLeftSide.TryPeek(out char result))
                        {
                            if (legalPairs[result] == k) // left and right match
                            {
                                progressLeftSide.Pop();
                            }
                            else
                            {
                                // corrupt
                                illegal = true;
                                break;
                            }
                        }
                    }
                }

                if(progressLeftSide.Count > 0 && !illegal)
                {
                    incompleteLines.Add(progressLeftSide);
                }
               
            }

            List<ulong> scores = new();

            foreach (var line in incompleteLines)
            {
                List<char> addons = new();
                while( line.TryPeek(out char result))
                {
                    char c = line.Pop();
                    addons.Add(legalPairs[c]);
                }

                ulong totalPoints = 0;
                addons.Reverse();
                for(int i = 0; i < addons.Count; i++)
                {
                    char c = addons[i];
                    totalPoints *= 5;
                    int points = autocompletepoints[c];
                    if(points < 0)
                    {

                    }

                    totalPoints += (ulong)points;
                    if (totalPoints < 0)
                    {

                    }
                }
                scores.Add(totalPoints);
            }

            scores.Sort();
            return scores[(scores.Count / 2) + 1];
        }
    }
}
