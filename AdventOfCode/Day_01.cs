namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string _input;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var numberedInputs = _input.Split("\n").Select(x => int.Parse(x));
        var previousMeasurement = numberedInputs.First();
        var increasedMeasurements = 0;
        foreach (var measurement in numberedInputs) {
            if (measurement > previousMeasurement)
            {
                increasedMeasurements++;
            }
            previousMeasurement = measurement;
        }
        return ValueTask.FromResult(increasedMeasurements.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var numberedInputs = _input.Split("\n").Select(x => int.Parse(x));
        var previousWindowCount = 0;
        var increasedWindow = 0;
        var previousWindowIdx = 0;
        while (true)
        {
            var windows = numberedInputs.Skip(previousWindowIdx).Take(3);
            if (windows.Count() < 3) break;
            var measuredCount = windows.Sum();
            if (previousWindowCount != 0)
            {
                if (measuredCount > previousWindowCount)
                    increasedWindow++;
            }
            previousWindowCount = measuredCount;
            previousWindowIdx += 1;
        }

        return ValueTask.FromResult(increasedWindow.ToString());
    }
}
