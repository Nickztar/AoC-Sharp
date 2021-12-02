namespace AdventOfCode;

public class Day_02 : BaseDay
{
    private readonly string _input;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var instructions = _input.Split("\r\n").Select(x => x.Split(" "));
        var horizontalPosition = 0;
        var depth = 0;
        foreach (var instruction in instructions)
        {
            var command = instruction[0];
            var amount = int.Parse(instruction[1]);
            switch (command)
            {
                case "forward":
                    horizontalPosition += amount;
                    break;
                case "down":
                    depth += amount;
                    break;
                case "up":
                    depth -= amount;
                    break;
            }
        }
        var result = horizontalPosition * depth;
        return ValueTask.FromResult(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var instructions = _input.Split("\r\n").Select(x => x.Split(" "));
        var horizontalPosition = 0;
        var depth = 0;
        var aim = 0;
        foreach (var instruction in instructions)
        {
            var command = instruction[0];
            var amount = int.Parse(instruction[1]);
            switch (command)
            {
                case "forward":
                    horizontalPosition += amount;
                    depth += aim * amount;
                    break;
                case "down":
                    aim += amount;
                    break;
                case "up":
                    aim -= amount;
                    break;
            }
        }
        var result = horizontalPosition * depth;
        return new ValueTask<string>(result.ToString());
    }
}
