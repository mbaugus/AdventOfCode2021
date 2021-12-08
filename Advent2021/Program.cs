// See https://aka.ms/new-console-template for more information



using System.Drawing;

Day1 day1 = new Day1();

int answerDay1P1 = day1.Part1();
int answerDay1P2 = day1.Part2();

Day2 day2 = new Day2();
int answerDay2P1 = day2.Part1();
int answerDay2P2 = day2.Part2();

Day3 day3 = new Day3();
int answerDay3P1 = day3.Part1();
int answerDay3P2 = day3.Part2();

Day4 day4 = new Day4();
int answerDay4P1 = day4.Part1();
int answerDay4P2 = day4.Part2();

Day5 day5 = new Day5();
int answerDay5P1 = day5.Part1();
int answerDay5P2 = day5.Part2();

Day6 day6 = new Day6();
int answerDay6P1 = day6.Part1();
ulong answerDay6P2 = day6.Part2();


Console.WriteLine("Day 1: Part 1: " + answerDay1P1);
Console.WriteLine("Day 1: Part 2: " + answerDay1P2);
Console.WriteLine("Day 2: Part 1: " + answerDay2P1);
Console.WriteLine("Day 2: Part 2: " + answerDay2P2);
Console.WriteLine("Day 3: Part 1: " + answerDay3P1);
Console.WriteLine("Day 3: Part 2: " + answerDay3P2);
Console.WriteLine("Day 4: Part 1: " + answerDay4P1);
Console.WriteLine("Day 4: Part 2: " + answerDay4P2);
Console.WriteLine("Day 5: Part 1: " + answerDay5P1);
Console.WriteLine("Day 5: Part 2: " + answerDay5P2);

Console.WriteLine("Day 6: Part 1: " + answerDay6P1);
Console.WriteLine("Day 6: Part 2: " + answerDay6P2);

Console.WriteLine("Press any key to continue.");
Console.ReadKey();



public class Day6
{
    class Fish
    {
        public byte InternalTimer { get; set; }
        public Fish(byte timer)
        {
            this.InternalTimer = timer;
        }
    }

    public int Part1()
    {
        return FishiesForDays(80);
    }

    public ulong Part2()
    {
        // return FishiesForDays(256); blows up after 150 days this is not good
        return WontKillYourMemoryFishies(256);
    }

    private ulong WontKillYourMemoryFishies(int days)
    {
        var lines = File.ReadAllLines("input/day6.txt");
        int daysToLive = days;
        var fishtimes = lines[0].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();
       // var fishtimes = lines[1].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();
        ulong zeroDays = 0,  oneDay = 0, twoDay = 0, threeDay = 0, fourDay = 0, fiveDay = 0, sixDay = 0, sevenDay = 0, eightDay = 0;
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
        Console.WriteLine("Initial" + (zeroDays + oneDay + twoDay + threeDay + fourDay + fiveDay + sixDay + sevenDay + eightDay) + " total fishies");
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
             Console.WriteLine("After " + day + " days: " + (zeroDays + oneDay + twoDay + threeDay + fourDay + fiveDay + sixDay + sevenDay + eightDay) + " total fishies");
        } while (day < daysToLive);

        return zeroDays + oneDay + twoDay + threeDay + fourDay + fiveDay + sixDay + sevenDay + eightDay;
        
    }


    private int FishiesForDays(int days)
    {
        var lines = File.ReadAllLines("input/day6.txt");
        int daysToLive = days;
        var fishtimes = lines[0].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();
        //var fishtimes = lines[1].Split(',').Select(l => Convert.ToInt32(l.Trim())).ToArray();  

        List<Fish> Fishies = new List<Fish>();
        List<Fish> NewFishies = new List<Fish>();

        foreach (var fishtime in fishtimes)
        {
            var fish = new Fish((byte)fishtime);
            Fishies.Add(fish);
        }

        int day = 0;
        //Console.WriteLine("Initial State: " + String.Join(',', Fishies.Select(p => p.InternalTimer.ToString())));
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
            //Console.WriteLine("Day: " + day);
            // Console.WriteLine("After " + day + " days: "  +String.Join(',', Fishies.Select(p => p.InternalTimer.ToString())));
        }
        while (day < (daysToLive - 1));

        return Fishies.Count;
    }
}

public class Day5
{
    struct SubMove
    {
        public Point A = new Point();
        public Point B = new Point();
    }

    //bresenhams line implementation from http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
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


    public int Part1()
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

        //using (var file = new StreamWriter("blah.txt"))
        //{
        //    foreach (var r in grid)
        //    {
        //        var ss = String.Join(',', r.Select(p => p.ToString()).ToArray());
        //        Console.WriteLine(ss);
        //        file.WriteLine(ss);
        //    }
        //}

        return amount;
    }

    public int Part2()
    {
        return 0;
    }

    private List<SubMove> GetMoves()
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
}

public class Day4
{

    private KeyValuePair<int[], int[][][]> GetBoardAndMoves(string[] lines)
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

    private int[][][] CreateEmptyBoard(int rows, int columns, int amount)
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

    private void ApplyMove(int move, int[][][] boards, int[][][] scoreBoard, List<int>? ignoreThese = null)
    {
        for (int i = 0; i < boards.Length; i++)
        {
            if (ignoreThese != null)
            {
                if (ignoreThese.Contains(i)) continue;
            }

            for (int y = 0; y < boards[i].Length; y++)
            {
                for(int x = 0; x < boards[i][y].Length; x++)
                {
                    int amount = boards[i][y][x];
                    if (amount == move)
                        scoreBoard[i][y][x] = 1;
                }
            }
        }
    }

    private int[] CheckForWinner(int[][][] boards, List<int>? ignoreThese = null)
    {
        List<int> winners = new List<int>();

        for(int i = 0; i < boards.Length; i++)
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
            for(int c = 0; c < 5; c++)
            {
                if(board[0][c] == 1 && board[1][c] == 1 && board[2][c] == 1 && board[3][c] == 1  && board[4][c] == 1)
                {
                    winners.Add(i);
                    break;
                };
            }
        }

        return winners.Distinct().ToArray();
    }

    public int Part1()
    {
        var lines = File.ReadAllLines("input/day4.txt");
        var kvp = GetBoardAndMoves(lines);

        var moves = kvp.Key;
        int[][][] boards = kvp.Value;
        int[][][] scoreBoard = CreateEmptyBoard(5, 5, boards.Length);

        int returnVal = 0;
        for(int i = 0; i < moves.Length; i++)
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

    private void PrintBoard(int[][] board)
    {
        for(int i = 0; i < board.Length; i++)
        {
            for(int x = 0; x < board[i].Length; x++)
            {
                Console.Write(board[i][x] + " ");
            }

            Console.WriteLine();
        }
    }

    private int LastBoardToWin()
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

    public int Part2()
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

            if(winners.Contains(lastToWin))
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
}

public class Day3
{

    public int Part2()
    {
        var lines = File.ReadAllLines("input/day3.txt");

        var converted = lines.Select(l => l.Select(p => Convert.ToBoolean(int.Parse(p.ToString()))).ToArray()).ToArray();
        
        int bits = converted.Max(c => c.Length);

        var oxgen = converted.ToList();

        for (int i = 0; i < bits; i++)
        {
            List<bool[]> ones = new List<bool[]>();
            List<bool[]> zeros = new List<bool[]>();

            for(int t = 0; t < oxgen.Count; t++)
            {
                if (oxgen[t][i] == true) ones.Add(oxgen[t]);
                else zeros.Add(oxgen[t]);
            }

            if(ones.Count > zeros.Count || ones.Count == zeros.Count)
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


    public int Part1()
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

    private int GetByteFromBitArray(bool[] arr)
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

    private int GetByteFromBitArray(byte[] arr)
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

public class Day2
{
    enum Direction {
        Up,
        Down,
        Forward,
    }

    private KeyValuePair<Direction, int> GetCommand(string line)
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

    public int Part2()
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

    public int Part1()
    {
        int pos = 0;
        int depth = 0;

        var lines = File.ReadAllLines("input/day2.txt");
        var commands = lines.Select(l => GetCommand(l)).ToArray();

        foreach (var c in commands)
        {
            if(c.Key == Direction.Up)
            {
                depth -= c.Value;
                if (depth <= 0) depth = 0;
            }
            else if(c.Key == Direction.Down)
            {
                depth += c.Value;
            }
            else if(c.Key == Direction.Forward)
            {
                pos += c.Value;
            }
        }
        return pos * depth;
    }


}

public class Day1
{
    public int Part1()
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


    public int Part2()
    {
        var lines = File.ReadAllLines("input/day1.txt");
        var depths = lines.Select(p => Convert.ToInt32(p)).ToArray();
        int number = 0;

        List<int> depthCombined = new List<int>();
        int resetter = 0;
        int sumMarker = 3;

        //int leftover = depths.Length % sumMarker;

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
