namespace AdventOfCode;

public class Day_08 : BaseDay
{
    private readonly string _input;
    public Day_08()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var lines = _input.Split("\r\n").Select(x => x.Split("|"));
        var parsed = lines.Select(x => (x[0].Split(" "), x[1].Split(" ")));
        var instances = 0;
        foreach (var line in parsed)
        {
            var pattern = line.Item1;
            var output = line.Item2;
            foreach (var value in output)
            {
                switch (value.Length)
                {
                    case 3:
                        //7
                        instances++;
                        break;
                    case 2:
                        //1
                        instances++;
                        break;
                    case 4:
                        //4
                        instances++;
                        break;
                    case 7:
                        //8
                        instances++;
                        break;
                    default:
                        break;
                }
            }
        }

        return ValueTask.FromResult(instances.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        return ValueTask.FromResult($"");
    }
}
