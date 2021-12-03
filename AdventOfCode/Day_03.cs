namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string _input;

    public Day_03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var binaryNumbers = _input.Split("\r\n");
        var splitNumbers = binaryNumbers.Select(x => x.ToCharArray());
        var gamma = "";
        var epsilon = "";
        var loopCount = splitNumbers.First().Length;
        for (int i = 0; i < loopCount; i++)
        {
            var column = splitNumbers.Select(x => int.Parse(x.ElementAt(i).ToString())).GroupBy(x => x).OrderByDescending(x => x.Count());
            var mostCommon = column.First().Key;
            var leastCommon = column.Last().Key;
            gamma += mostCommon.ToString();
            epsilon += leastCommon.ToString();
        }
        var decimalGamma = Convert.ToInt32(gamma, 2);
        var decimalEpsilon = Convert.ToInt32(epsilon, 2);
        return ValueTask.FromResult((decimalGamma * decimalEpsilon).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        
        return new ValueTask<string>("");
    }
}
