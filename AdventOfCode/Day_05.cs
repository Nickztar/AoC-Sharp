namespace AdventOfCode;

public class Day_05 : BaseDay
{
    private readonly string _input;
    public Day_05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var lines = _input.Split("\r\n");
        var ranges = lines
            .Select(x => 
                x.Split(" -> ")
                .Select(p => 
                    p.Split(",")
                        .Select(int.Parse)
                )
            );
        var points = new Dictionary<(int x, int y), int>();
        foreach (var line in ranges)
        {
            var first = line.First();
            var x1 = first.First();
            var y1 = first.Last();
            var last = line.Last();
            var x2 = last.First();
            var y2 = last.Last();
            //Only check horizontal and vertical lines
            if (x1 == x2)
            {
                //Generate the position in the range
                for (int number = Math.Min(y1, y2); number <= Math.Max(y1, y2); number++)
                {
                    var pos = (x1, number);
                    if (points.TryGetValue(pos, out int count))
                    {
                        points[pos] = count + 1;
                    }
                    else
                    {
                        points[pos] = 1;
                    }
                }
            }
            else if (y1 == y2)
            {
                //Generate the position in the range
                for (int number = Math.Min(x1, x2); number <= Math.Max(x1, x2); number++)
                {
                    var pos = (number, y1);
                    if (points.TryGetValue(pos, out int count))
                    {
                        points[pos] = count + 1;
                    }
                    else
                    {
                        points[pos] = 1;
                    }
                }
            }
        }
        var result = points.Count(x => x.Value > 1);
        return ValueTask.FromResult(result.ToString());
    }
    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var lines = _input.Split("\r\n");
        var ranges = lines
            .Select(x =>
                x.Split(" -> ")
                .Select(p =>
                    p.Split(",")
                        .Select(int.Parse)
                )
            );
        var points = new Dictionary<(int x, int y), int>();
        void AddPosition((int x, int y) pos)
        {
            if (points.TryGetValue(pos, out int count))
            {
                points[pos] = count + 1;
            }
            else
            {
                points[pos] = 1;
            }
        }
        foreach (var line in ranges)
        {
            var first = line.First();
            var x1 = first.First();
            var y1 = first.Last();
            var last = line.Last();
            var x2 = last.First();
            var y2 = last.Last();
            //Only check horizontal and vertical lines
            if (x1 == x2)
            {
                //Vertical
                for (int number = Math.Min(y1, y2); number <= Math.Max(y1, y2); number++)
                {
                    var pos = (x1, number);
                    AddPosition(pos);
                }
            }
            else if (y1 == y2)
            {
                //Horizontal
                for (int number = Math.Min(x1, x2); number <= Math.Max(x1, x2); number++)
                {
                    var pos = (number, y1);
                    AddPosition(pos);
                }
            }
            else
            {
                //Generate all position diagonal
                if (x1 > x2)
                {
                    (x1, y1, x2, y2) = (x2, y2, x1, y1);
                }

                if (y1 < y2)
                {
                    //decending diagonal
                    for (int number = 0; number < x2 + 1 - x1; number++)
                    {
                        var pos = (x1 + number, y1 + number);
                        AddPosition(pos);
                    }
                }
                else
                {
                    //ascending diagonal
                    for (int number = 0; number < x2 + 1 - x1; number++)
                    {
                        var pos = (x1 + number, y1 - number);
                        AddPosition(pos);
                    }
                }
            }
        }
        var result = points.Count(x => x.Value > 1);
        return ValueTask.FromResult(result.ToString());
    }
}
