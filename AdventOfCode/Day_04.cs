namespace AdventOfCode;

public class Day_04 : BaseDay
{
    private readonly string _input;
    public Day_04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var orderAndBoards = _input.Split("\r\n\r\n");
        var order = orderAndBoards[0].Split(",").Select(int.Parse);
        var boards = orderAndBoards.Skip(1).Select(b => new Board(b));
        WinBoard winningBoard = null;
        for (int numbersToCheck = 1; numbersToCheck <= order.Count(); numbersToCheck++)
        {
            if (winningBoard != null) break;
            var numbers = order.Take(numbersToCheck);
            foreach (var board in boards)
            {
                //Check rows 
                foreach (var row in board.Rows)
                {
                    var marked = row.Where(r => numbers.Any(x => r == x));
                    if (marked.Count() == 5)
                    {
                        //WIN
                        winningBoard = new WinBoard { Numbers = numbers, Board = board };
                        break;
                    }
                }
                //Check columns
                foreach (var column in board.Columns)
                {
                    var marked = column.Where(r => numbers.Any(x => r == x));
                    if (marked.Count() == 5)
                    {
                        //WIN
                        winningBoard = new WinBoard { Numbers = numbers, Board = board };
                        break;
                    }
                }
                if (winningBoard != null) break;
            }
        }
        var rowNumbers = winningBoard.Board.Rows.SelectMany(x => x);
        var columnNumbers = winningBoard.Board.Columns.SelectMany(x => x);
        var boardNumbers = rowNumbers.Concat(columnNumbers).Distinct();
        var unmarkedNumbers = boardNumbers.Where(x => !winningBoard.Numbers.Contains(x));
        var sumOfNumbers = unmarkedNumbers.Sum();
        var result = sumOfNumbers * winningBoard.Numbers.Last();
        return ValueTask.FromResult(result.ToString());
    }

    class Board
    {
        public Board(string board)
        {
            var rows = board.Split("\r\n").Select(x => x.Split(" ").Where(s => s != "").Select(int.Parse).ToList());
            var columns = new List<IEnumerable<int>>();
            for (int i = 0; i < 5; i++)
            {
                columns.Add(rows.Select(x => x.ElementAt(i)).ToList());
            }
            Columns = columns;
            Rows = rows;
        }
        public IEnumerable<IEnumerable<int>> Rows { get; set; }
        public IEnumerable<IEnumerable<int>> Columns { get; set; }
        public bool HasWon { get; set; } = false;
    }

    class WinBoard
    {
        public IEnumerable<int> Numbers { get; set; }    
        public Board Board { get; set; }
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var orderAndBoards = _input.Split("\r\n\r\n");
        var order = orderAndBoards[0].Split(",").Select(int.Parse);
        var boards = orderAndBoards.Skip(1).Select(b => new Board(b)).ToList();
        WinBoard winningBoard = null;
        for (int numbersToCheck = 1; numbersToCheck <= order.Count(); numbersToCheck++)
        {
            if (boards.All(b => b.HasWon)) break;
            var numbers = order.Take(numbersToCheck);
            var nonWonBoards = boards.Where(x => !x.HasWon).ToList();
            foreach (var board in nonWonBoards)
            {
                //Check rows 
                foreach (var row in board.Rows)
                {
                    var marked = row.Where(r => numbers.Any(x => r == x));
                    if (marked.Count() == 5)
                    {
                        //WIN
                        board.HasWon = true;
                        winningBoard = new WinBoard { Numbers = numbers, Board = board };
                        break;
                    }
                }
                //Check columns
                foreach (var column in board.Columns)
                {
                    var marked = column.Where(r => numbers.Any(x => r == x));
                    if (marked.Count() == 5)
                    {
                        //WIN
                        board.HasWon = true;
                        winningBoard = new WinBoard { Numbers = numbers, Board = board };
                        break;
                    }
                }
            }
        }
        var rowNumbers = winningBoard.Board.Rows.SelectMany(x => x);
        var columnNumbers = winningBoard.Board.Columns.SelectMany(x => x);
        var boardNumbers = rowNumbers.Concat(columnNumbers).Distinct();
        var unmarkedNumbers = boardNumbers.Where(x => !winningBoard.Numbers.Contains(x));
        var sumOfNumbers = unmarkedNumbers.Sum();
        var result = sumOfNumbers * winningBoard.Numbers.Last();
        return ValueTask.FromResult(result.ToString());
    }
}
