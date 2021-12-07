namespace AdventOfCode;

public class Day_07 : BaseDay
{
    private readonly string _input;
    public Day_07()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var horizontalPositions = _input.Split(",").Select(int.Parse);
        var max = horizontalPositions.Max();
        var costPerPosition = new List<(int cost, int pos)>();
        for (var i = 0; i < max; i++)
        {
            //Calculate the cost of moving to every position and get the sum of that
            costPerPosition.Add((horizontalPositions.Select(x => Math.Abs(x - i)).Sum(), i));
        }
        var (cost, _) = costPerPosition.MinBy(x => x.cost);
        return ValueTask.FromResult(cost.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var horizontalPositions = _input.Split(",").Select(int.Parse);
        var max = horizontalPositions.Max();
        var costPerPosition = new List<(int cost, int pos)>();
        for (var i = 0; i < max; i++)
        {
            //Calculate the cost of moving to every position and get the sum of that
            costPerPosition.Add((horizontalPositions.Select(position => Enumerable.Range(1, Math.Abs(position - i)).Sum()).Sum(), i));
        }
        var (cost, position) = costPerPosition.MinBy(x => x.cost);
        return ValueTask.FromResult($"Move to {position} at a cost of {cost}");
    }
}
