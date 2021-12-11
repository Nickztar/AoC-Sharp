namespace AdventOfCode;

public class Day_11 : BaseDay
{
    private readonly string _input;
    public Day_11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var lines = _input.Split("\r\n").Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray()).ToArray();
        var steps = 100;
        var totalFlashes = 0;
        for (int step = 0; step < steps; step++)
        {
            HashSet<(int, int)> didFlash = new();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int k = 0; k < lines[i].Length; k++)
                {
                    lines[i][k]++;
                }
            }
            var temp = -1;
            while (temp != totalFlashes)
            {
                temp = totalFlashes;
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int k = 0; k < lines[i].Length; k++)
                    {
                        if (lines[i][k] == 10 && !didFlash.Contains((i, k)))
                        {
                            UpdateNeighbours(lines, i, k);
                            totalFlashes++;
                            didFlash.Add((i, k));
                            lines[i][k] = 0;
                        }
                    }
                }
            }
        }
        return ValueTask.FromResult(totalFlashes.ToString());
    }

    static void UpdateNeighbours(int[][] lines, int row, int column)
    {
        int rows = lines.Length;
        int columns = lines[row].Length;

        for (int j = row - 1; j <= row + 1; j++)
        {
            for (int i = column - 1; i <= column + 1; i++)
            {
                if (i >= 0 && j >= 0 && i < columns && j < rows && !(j == row && i == column))
                {
                    if (lines[j][i] != 10 && lines[j][i] > 0)
                    {
                        lines[j][i]++;
                    }
                }
            }
        }
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var lines = _input.Split("\r\n").Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray()).ToArray();
        var currentStep = 0;
        var totalFlashes = 0;
        while (true)
        {
            HashSet<(int, int)> didFlash = new();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int k = 0; k < lines[i].Length; k++)
                {
                    lines[i][k]++;
                }
            }
            var temp = -1;
            while (temp != totalFlashes)
            {
                temp = totalFlashes;
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int k = 0; k < lines[i].Length; k++)
                    {
                        if (lines[i][k] == 10 && !didFlash.Contains((i, k)))
                        {
                            UpdateNeighbours(lines, i, k);
                            totalFlashes++;
                            didFlash.Add((i, k));
                            lines[i][k] = 0;
                        }
                    }
                }
            }
            currentStep++;
            if (didFlash.Count == 10 * 10)
            {
                break;
            }
        }
        return ValueTask.FromResult(currentStep.ToString());
    }
}
