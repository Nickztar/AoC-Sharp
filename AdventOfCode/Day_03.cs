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
        var binaryNumbers = _input.Split("\r\n");
        var splitNumbers = binaryNumbers.Select(x => x.ToCharArray());
        //Figure out the oxygen generator rating
        var loopCount = splitNumbers.First().Length;
        var oxygenResult = "";
        var co2Result = "";
        for (int i = 0; i < 2; i++)
        {
            var isOxygen = i == 0;
            var result = "";
            int idxCheck = 0;
            List<char[]> currentNumbers = new List<char[]>(splitNumbers);
            while (true)
            {
                
                var groups = currentNumbers.GroupBy(x => x[idxCheck]);
                var zeroCount = 0;
                var oneCount = 0;
                foreach (var group in groups)
                {
                    if (group.Key == '0')
                        zeroCount = group.Count();
                    else
                        oneCount = group.Count();
                }
                if (isOxygen)
                {
                    //Most common value is prefered
                    if (oneCount >= zeroCount)
                    {
                        currentNumbers.RemoveAll(x => x[idxCheck] == '0');
                    }
                    else
                    {
                        currentNumbers.RemoveAll(x => x[idxCheck] == '1');
                    }
                }
                else
                {
                    //Least common value is prefered
                    if (zeroCount > oneCount)
                    {
                        currentNumbers.RemoveAll(x => x[idxCheck] == '0');
                    }
                    else
                    {
                        currentNumbers.RemoveAll(x => x[idxCheck] == '1');
                    }
                }

                //As there is only one number left, stop;
                if (currentNumbers.Count == 1)
                {
                    result = string.Join("", currentNumbers.First());
                    break;
                }

                if (idxCheck == loopCount - 1) break;
                else idxCheck++;
            }
            if (isOxygen)
                oxygenResult = result;
            else 
                co2Result = result;
        }

        var decimalOxygen = Convert.ToInt32(oxygenResult, 2);
        var decimalCO2 = Convert.ToInt32(co2Result, 2);
        return new ValueTask<string>((decimalOxygen * decimalCO2).ToString());
    }
}
