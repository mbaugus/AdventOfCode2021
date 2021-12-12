using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day5
    {


        public static int Run1()
        {
            var moves = GetMoves();

            int maxAX = moves.Max(m => m.A.X);
            int maxAY = moves.Max(m => m.A.Y);
            int maxBX = moves.Max(m => m.B.X);
            int maxBY = moves.Max(m => m.B.Y);

            int mX = (maxAX > maxBX ? maxAX : maxBX) + 1;
            int mY = (maxAY > maxBY ? maxAY : maxBY) + 1;

            int[][] grid = new int[mY][];

            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new int[mX];
            }

            foreach (var move in moves)
            {
                if (move.A.X == move.B.X || move.A.Y == move.B.Y) // only straight lines
                {
                    var points = GetPointsOnLine(move.A.X, move.A.Y, move.B.X, move.B.Y).ToArray();
                    foreach (var point in points)
                    {
                        grid[point.Y][point.X]++;
                    }
                }
            }

            int amount = 0;
            foreach (var r in grid)
            {
                foreach (var c in r)
                {
                    if (c > 1) amount++;
                }
            }

            return amount;
        }

        public static int Run2()
        {
            var moves = GetMoves();

            int maxAX = moves.Max(m => m.A.X);
            int maxAY = moves.Max(m => m.A.Y);
            int maxBX = moves.Max(m => m.B.X);
            int maxBY = moves.Max(m => m.B.Y);

            int mX = (maxAX > maxBX ? maxAX : maxBX) + 1;
            int mY = (maxAY > maxBY ? maxAY : maxBY) + 1;

            int[][] grid = new int[mY][];

            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new int[mX];
            }

            foreach (var move in moves)
            {

                var points = GetPointsOnLine(move.A.X, move.A.Y, move.B.X, move.B.Y).ToArray();
                foreach (var point in points)
                {
                    grid[point.Y][point.X]++;
                }
            }

            int amount = 0;
            foreach (var r in grid)
            {
                foreach (var c in r)
                {
                    if (c > 1) amount++;
                }
            }

            return amount;
        }



        private static List<SubMove> GetMoves()
        {
            List<SubMove> moves = new List<SubMove>();
            var lines = File.ReadAllLines("input/day5.txt");
            foreach (var line in lines)
            {
                var split = line.Split("->");
                var left = split[0].Trim().Split(",").ToArray();
                var right = split[1].Trim().Split(",").ToArray();
                SubMove move = new SubMove();

                move.A.X = Convert.ToInt32(left[0]);
                move.A.Y = Convert.ToInt32(left[1]);

                move.B.X = Convert.ToInt32(right[0]);
                move.B.Y = Convert.ToInt32(right[1]);

                moves.Add(move);
            }

            return moves;
        }

        struct SubMove
        {
            public Point A = new Point();
            public Point B = new Point();
        }

        //bresenhams line implementation from
        //Eric Woroshow
        //http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
        public static IEnumerable<Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                int t;
                t = x0; // swap x0 and y0
                x0 = y0;
                y0 = t;
                t = x1; // swap x1 and y1
                x1 = y1;
                y1 = t;
            }
            if (x0 > x1)
            {
                int t;
                t = x0; // swap x0 and x1
                x0 = x1;
                x1 = t;
                t = y0; // swap y0 and y1
                y0 = y1;
                y1 = t;
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                yield return new Point((steep ? y : x), (steep ? x : y));
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
            yield break;
        }
    }
}
