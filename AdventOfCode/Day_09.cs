namespace AdventOfCode;

public class Day_09 : BaseDay
{
    private readonly string _input;
    public Day_09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var lines = _input.Split("\r\n").Select(x => x.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray()).ToArray();
        var adjacentCount = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var row = lines[i];
            for (int k = 0; k < row.Length; k++)
            {
                var height = row[k];
                //up, down, left, and right
                var canUP = i != 0;
                var canDOWN = i != lines.Length - 1;
                var canLEFT = k != 0;
                var canRIGHT = k != row.Length - 1;
                var up = int.MaxValue;
                var down = int.MaxValue;
                var left = int.MaxValue;
                var right = int.MaxValue;
                if (canUP)
                {
                    up = lines[i - 1][k];
                }
                if (canDOWN)
                {
                    down = lines[i + 1][k];
                }
                if (canLEFT)
                {
                    left = row[k - 1];
                }
                if (canRIGHT)
                {
                    right = row[k + 1];
                }
                var numberToCheck = new[] { up, down, left, right };
                if (numberToCheck.Where(x => x != int.MaxValue).All(x => height < x))
                {
                    adjacentCount += height + 1;
                }
            }
        }
        return ValueTask.FromResult(adjacentCount.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var lines = _input.Split("\r\n").ToArray();
        int width = lines[0].Length;
        int height = lines.Length;
        int[,] data = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                data[x, y] = (int)char.GetNumericValue(lines[y][x]);
            }
        }
        int[,] basinSizes = new int[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int currentX = x;
                int currentY = y;
                int currentHeight = data[currentX, currentY];
                if (currentHeight == 9) continue; // Skip borders
                (int downhillX, int downhillY) = lowestNeighbour(x, y);

                // Walk downhill
                while (data[downhillX, downhillY] < currentHeight)
                {
                    currentX = downhillX;
                    currentY = downhillY;
                    currentHeight = data[currentX, currentY];
                    (downhillX, downhillY) = lowestNeighbour(currentX, currentY);
                }

                basinSizes[currentX, currentY]++;
            }
        }
        (int x, int y) lowestNeighbour(int x, int y)
        {
            int min = int.MaxValue;
            int minX = x;
            int minY = y;
            int val;

            if (x > 0 && (val = data[x - 1, y]) < min)
            {
                min = val;
                minX = x - 1;
                minY = y;
            }

            if (y > 0 && (val = data[x, y - 1]) < min)
            {
                min = val;
                minX = x;
                minY = y - 1;
            }

            if (x < width - 1 && (val = data[x + 1, y]) < min)
            {
                min = val;
                minX = x + 1;
                minY = y;
            }

            if (y < height - 1 && (val = data[x, y + 1]) < min)
            {
                min = val;
                minX = x;
                minY = y + 1;
            }

            return (minX, minY);
        }
        var result = basinSizes.OfType<int>().OrderByDescending(x => x).Take(3).Aggregate(1, (x, a) => x * a);
        return ValueTask.FromResult(result.ToString());
    }
}
