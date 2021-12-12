using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day9
    {
        public static int Run1()
        {
            var lines = File.ReadAllLines("input/day9.txt").ToArray();
            int horizontalLen = lines[0].Length;
            int[,] map = new int[lines.Length, horizontalLen];
            for(int r = 0; r < lines.Length; r++)
            { 
                for(int i = 0; i < lines[r].Length; i++)
                {
                    map[r, i] = Convert.ToInt32(lines[r][i].ToString());
                }
            }

            int ylen = lines.Length;
            int xlen = horizontalLen;

            int risktotal = 0;
            List<KeyValuePair<int, int>> risks = new List<KeyValuePair<int, int>>();
            for(int y = 0; y < ylen; y++)
            {
                for (int x = 0; x < xlen; x++)
                {
                    int num = map[y, x];
                    if (IsLowPoint(map, x, y, xlen, ylen))
                    {
                        risks.Add(new KeyValuePair<int, int>(y, x));
                        int risk = num + 1;
                        risktotal += risk;

                    }
                }
            }

            return risktotal;
        }

        public static int Run2()
        {
            var lines = File.ReadAllLines("input/day9.txt").ToArray();
            int horizontalLen = lines[0].Length;
            int[,] map = new int[lines.Length, horizontalLen];
            for (int r = 0; r < lines.Length; r++)
            {
                for (int i = 0; i < lines[r].Length; i++)
                {
                    map[r, i] = Convert.ToInt32(lines[r][i].ToString());
                }
            }

            int ylen = lines.Length;
            int xlen = horizontalLen;

            int risktotal = 0;
            List<int> basins = new List<int>();
            List<KeyValuePair<int, int>> risks = new List<KeyValuePair<int, int>>();
            for (int y = 0; y < ylen; y++)
            {
                for (int x = 0; x < xlen; x++)
                {
                    int num = map[y, x];
                    if (IsLowPoint(map, x, y, xlen, ylen))
                    {
                        List<Point> explored = new List<Point>();
                        risks.Add(new KeyValuePair<int, int>(y, x));
                        int risk = num + 1;
                        risktotal += risk;

                        int basinScore = BasinScoreForLowPoint(map, new Point(x, y), xlen, ylen, ref explored);
                        basins.Add(basinScore);
                    }
                }
            }

            var three = basins.OrderByDescending(p => p).Take(3).ToArray();
            int multiply = three[0];
            for(int i = 1; i < three.Length; i++)
            {
                multiply *= three[i];
            }

            return multiply;
        }

        private static int BasinScoreForLowPoint(int[,] map, Point origin, int lenX, int lenY, ref List<Point> explored)
        {
            List<Point> surroundingPoints = new List<Point>();

            explored.Add(origin);

            // only check in basic ordinal directions, n, w, s, e

            // to west
            if (origin.X > 0)
            {
                surroundingPoints.Add(new Point(origin.X -1 ,origin.Y));
            }

            // to north
            if (origin.Y > 0)
            {
                surroundingPoints.Add(new Point(origin.X, origin.Y - 1));
            }

            // to northwest
            //if (origin.Y > 0 && origin.X > 0)
            //{
            //    surroundingPoints.Add(new Point(origin.X - 1, origin.Y - 1));
            //}

            // to east
            if (origin.X < lenX - 1)
            {
                surroundingPoints.Add(new Point(origin.X + 1, origin.Y));
            }

            // to northeast
            //if (origin.X < lenX - 1 && origin.Y > 0)
            //{
            //    surroundingPoints.Add(new Point(origin.X + 1, origin.Y - 1));
            //}

            // to southwest
            //if (origin.Y < lenY - 1 && origin.X > 0)
            //{
            //    surroundingPoints.Add(new Point(origin.X - 1, origin.Y + 1));
            //}

            // to south
            if (origin.Y < lenY - 1)
            {
                surroundingPoints.Add(new Point(origin.X, origin.Y + 1));
            }

            // to southeast
            //if (origin.Y < lenY - 1 && origin.X < lenX - 1)
            //{
            //    surroundingPoints.Add(new Point(origin.X + 1, origin.Y + 1));
            //}

            
            int originValue = map[origin.Y, origin.X];
            int surroundingPointScore = 1;
            foreach (var point in surroundingPoints)
            {
               
                int exploringValue = map[point.Y, point.X];

                if (explored.Contains(point) || exploringValue == 9 || exploringValue <= originValue) continue;
                
                surroundingPointScore += BasinScoreForLowPoint(map, point, lenX, lenY, ref explored);
            }

            return surroundingPointScore;
        }

        private static bool IsLowPoint(int[,] map, int posx, int posy, int lenX, int lenY)
        {
            // variable names easier for me to read and understand
            int nw = -1, n = -1, ne = -1, w = -1, e = -1, sw = -1, s = -1, se = -1;
            List<int> includedInComparison = new List<int>();
            if(posx > 0)
            {
                w = map[posy, posx-1];
                includedInComparison.Add(w);
            }
            if(posy > 0)
            {
                n = map[posy-1, posx];
                includedInComparison.Add(n);
            }

            if(posy > 0 && posx > 0)
            {
                nw = map[posy - 1, posx - 1];
                includedInComparison.Add(nw);
            }

            if(posx < lenX - 1)
            {
                e = map[posy, posx + 1];
                includedInComparison.Add(e);
            }

            if(posx < lenX - 1 && posy > 0)
            {
                ne = map[posy - 1, posx + 1];
                includedInComparison.Add(ne);
            }

            if (posy < lenY - 1 && posx > 0)
            {
                sw = map[posy + 1, posx - 1];
                includedInComparison.Add(sw);
            }
            if (posy < lenY - 1)
            {
                s = map[posy + 1, posx];
                includedInComparison.Add(s);
            }
            if (posy < lenY - 1 && posx < lenX - 1 ) 
            {
                se = map[posy + 1, posx + 1];
                includedInComparison.Add(se);
            }

            int comparison = map[posy, posx];
            return includedInComparison.All(p => comparison < p);
        }
        
    }
}
