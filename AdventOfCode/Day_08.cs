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
        var lines = _input.Split("\r\n").Select(x => x.Split("|"));
        var parsed = lines.Select(x => (x[0].Split(" "), x[1].Split(" ")));
        var sum = 0;
        foreach (var (pattern, output) in parsed)
        {
            //We know these four since they can only be one number with their length
            var one = pattern.Single(x => x.Length == 2);
            var seven = pattern.Single(x => x.Length == 3);
            var four = pattern.Single(x => x.Length == 4);
            var eight = pattern.Single(x => x.Length == 7);

            //Nine is 6  but with four and seven removed should only have the bottom one left
            var nine = pattern.Single(x =>
                x.Length == 6 &&
                x.Except(four).Except(seven).Count() == 1);

            //The onces left at 6 length are six and zero
            //But six does not contain the full one, while the zero does
            var six = pattern.Single(x =>
                x.Length == 6 &&
                x.Except(one).Count() != 4);
            var zero = pattern.Single(x =>
                x.Length == 6 && x != nine &&
                x.Except(one).Count() == 4);

            //Now we just find out what the 5 length ones are (five, two and three)
            //Three should contain the full one
            var three = pattern.Single(x =>
                x.Length == 5 &&
                x.Except(one).Count() == 3);
            //Five without nine should mean that there is nothing left, but with two there should be one left
            //Three without nine is also zero so need to make sure to not match that
            var five = pattern.Single(x =>
                x.Length == 5 && x != three && 
                !x.Except(nine).Any());
            var two = pattern.Single(x =>
                x.Length == 5 &&
                x.Except(nine).Any());

            //This would be nice if we just did indexes instead
            var board = new[] {
                zero,
                one,
                two,
                three,
                four,
                five,
                six,
                seven,
                eight,
                nine
            };
            //Now just find out what the count is for the output is
            var lineResult = output
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x =>
                    Array.FindIndex(board, n => !n.Except(x).Any() && !x.Except(n).Any()))
                .Aggregate(0, (i, n) => i * 10 + n);
            sum += lineResult;
            //Decode what the board looks like by these indexes
            //available letters a,b,c,d,e,f,g
            // 0000
            //1    2
            //1    2
            //1    2
            //1    2
            // 3333
            //4    5
            //4    5
            //4    5
            //4    5
            // 6666
            /*
                 aaaa    ....    aaaa    aaaa    ....
                b    c  .    c  .    c  .    c  b    c
                b    c  .    c  .    c  .    c  b    c
                 ....    ....    dddd    dddd    dddd
                e    f  .    f  e    .  .    f  .    f
                e    f  .    f  e    .  .    f  .    f
                 gggg    ....    gggg    gggg    ....

                  5:      6:      7:      8:      9:
                 aaaa    aaaa    aaaa    aaaa    aaaa
                b    .  b    .  .    c  b    c  b    c
                b    .  b    .  .    c  b    c  b    c
                 dddd    dddd    ....    dddd    dddd
                .    f  e    f  .    f  e    f  .    f
                .    f  e    f  .    f  e    f  .    f
                 gggg    gggg    ....    gggg    gggg

             */
        }
        return ValueTask.FromResult(sum.ToString());
    }
}
