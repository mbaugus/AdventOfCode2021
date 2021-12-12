using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2021
{
    public static class Day4
    {
        public static int Run1()
        {
            var lines = File.ReadAllLines("input/day4.txt");
            var kvp = GetBoardAndMoves(lines);

            var moves = kvp.Key;
            int[][][] boards = kvp.Value;
            int[][][] scoreBoard = CreateEmptyBoard(5, 5, boards.Length);

            int returnVal = 0;
            for (int i = 0; i < moves.Length; i++)
            {
                int move = moves[i];
                ApplyMove(move, boards, scoreBoard);
                var winners = CheckForWinner(scoreBoard);
                if (winners.Length == 0) continue;

                int[][] winningScoreBoard = scoreBoard[winners[0]];
                int[][] winningBoard = boards[winners[0]];

                int sumForPuzzle = 0;
                for (int w = 0; w < winningScoreBoard.Length; w++)
                {
                    for (int x = 0; x < winningScoreBoard[w].Length; x++)
                    {
                        if (winningScoreBoard[w][x] == 0)
                            sumForPuzzle += winningBoard[w][x];
                    }
                }

                returnVal = sumForPuzzle * move;
                break;
            }

            return returnVal;
        }

        public static int Run2()
        {
            int lastToWin = LastBoardToWin();

            var lines = File.ReadAllLines("input/day4.txt");
            var kvp = GetBoardAndMoves(lines);

            var moves = kvp.Key;
            int[][][] boards = kvp.Value;
            int[][][] scoreBoard = CreateEmptyBoard(5, 5, boards.Length);
            int returnVal = 0;
            List<int> ignorethese = new List<int>();

            for (int i = 0; i < moves.Length; i++)
            {
                int move = moves[i];
                ApplyMove(move, boards, scoreBoard, ignorethese);
                var winners = CheckForWinner(scoreBoard, ignorethese);
                if (winners.Length == -1) continue;

                ignorethese.AddRange(winners);

                if (winners.Contains(lastToWin))
                {
                    int[][] winningScoreBoard = scoreBoard[lastToWin];
                    int[][] winningBoard = boards[lastToWin];

                    int sumForPuzzle = 0;
                    for (int w = 0; w < winningScoreBoard.Length; w++)
                    {
                        for (int x = 0; x < winningScoreBoard[w].Length; x++)
                        {
                            if (winningScoreBoard[w][x] == 0)
                                sumForPuzzle += winningBoard[w][x];
                        }
                    }

                    returnVal = sumForPuzzle * move;
                    break;
                }
                else
                {
                    continue;
                }
            }

            return returnVal;
        }


        private static KeyValuePair<int[], int[][][]> GetBoardAndMoves(string[] lines)
        {
            var drawn = lines[0].Split(',').Select(p => Convert.ToInt32(p)).ToArray();
            List<List<int[]>> boards = new List<List<int[]>>();

            for (int i = 2; i < lines.Length; i += 6)
            {
                List<int[]> board = new List<int[]>();

                for (int b = 0; b < 5; b++)
                {
                    // they buffered the leading zero on single digits with a space, doh.
                    string s = lines[i + b];
                    while (s.Contains("  "))
                    {
                        s = s.Replace("  ", " ");
                    }

                    var splt = s.Trim().Split(' ').ToArray();
                    var t = splt.Select(p => Convert.ToInt32(p)).ToArray();
                    board.Add(t);
                }

                boards.Add(board);
            }

            var br = boards.Select(b => b.Select(g => g.ToArray()).ToArray()).ToArray();

            return new KeyValuePair<int[], int[][][]>(drawn, br);
        }

        private static int[][][] CreateEmptyBoard(int rows, int columns, int amount)
        {
            List<List<List<int>>> board = new List<List<List<int>>>();

            for (int i = 0; i < amount; i++)
            {
                List<List<int>> scorrri = new List<List<int>>();
                for (int t = 0; t < columns; t++)
                {
                    List<int> vs = new List<int>();
                    for (int x = 0; x < rows; x++)
                    {
                        vs.Add(0);
                    }
                    scorrri.Add(vs);
                }

                board.Add(scorrri);
            }

            return board.Select(b => b.Select(g => g.ToArray()).ToArray()).ToArray();
        }

        private static void ApplyMove(int move, int[][][] boards, int[][][] scoreBoard, List<int>? ignoreThese = null)
        {
            for (int i = 0; i < boards.Length; i++)
            {
                if (ignoreThese != null)
                {
                    if (ignoreThese.Contains(i)) continue;
                }

                for (int y = 0; y < boards[i].Length; y++)
                {
                    for (int x = 0; x < boards[i][y].Length; x++)
                    {
                        int amount = boards[i][y][x];
                        if (amount == move)
                            scoreBoard[i][y][x] = 1;
                    }
                }
            }
        }

        private static int LastBoardToWin()
        {
            var lines = File.ReadAllLines("input/day4.txt");
            var kvp = GetBoardAndMoves(lines);

            var moves = kvp.Key;
            int[][][] boards = kvp.Value;
            int[][][] scoreBoard = CreateEmptyBoard(5, 5, boards.Length);

            List<int> ignorethese = new List<int>();

            for (int i = 0; i < moves.Length; i++)
            {
                int move = moves[i];
                ApplyMove(move, boards, scoreBoard);
                var winners = CheckForWinner(scoreBoard, ignorethese);
                if (winners.Length == 0) continue;

                ignorethese.AddRange(winners);
            }

            return ignorethese.Last();
        }


        private static int[] CheckForWinner(int[][][] boards, List<int>? ignoreThese = null)
        {
            List<int> winners = new List<int>();

            for (int i = 0; i < boards.Length; i++)
            {
                if (ignoreThese != null)
                {
                    if (ignoreThese.Contains(i)) continue;
                }

                int[][] board = boards[i];

                // horizontal condition
                for (int r = 0; r < 5; r++)
                {
                    if (board[r][0] == 1 && board[r][1] == 1 && board[r][2] == 1 && board[r][3] == 1 && board[r][4] == 1)
                    {
                        winners.Add(i);
                        break;
                    }
                }

                // vertical conditions
                for (int c = 0; c < 5; c++)
                {
                    if (board[0][c] == 1 && board[1][c] == 1 && board[2][c] == 1 && board[3][c] == 1 && board[4][c] == 1)
                    {
                        winners.Add(i);
                        break;
                    };
                }
            }

            return winners.Distinct().ToArray();
        }
    }
}
