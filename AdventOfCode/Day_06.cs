namespace AdventOfCode;

public class Day_06 : BaseDay
{
    private readonly string _input;
    public Day_06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var fishTimers = _input.Split(",").Select(x => new Fish(x)).ToList();
        var days = 80;
        while (days > 0)
        {
            var fishToAdd = 0;
            foreach (var fish in fishTimers)
            {
                if (fish.Timer == 0)
                {
                    fishToAdd++;
                    fish.Timer = 6;
                }
                else
                {
                    fish.Timer = fish.Timer - 1;
                }
            }
            if (fishToAdd > 0)
            {
                for (int i = 0; i < fishToAdd; i++)
                {
                    fishTimers.Add(new Fish("8"));
                }
            }
            days--;
        }
        return ValueTask.FromResult(fishTimers.Count.ToString());
    }
    class Fish
    {
        public Fish(string timer)
        {
            Timer = int.Parse(timer);
        }
        public int Timer { get; set; }
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        byte[] fishTimers = _input.Split(",").Select(byte.Parse).ToArray();
        var days = 256;
        var result = CountFish(days, fishTimers, 6, 8);
        return ValueTask.FromResult(result.ToString());
    }

    long CountFish(int days, byte[] fishTimers, int birthFrequency, int maxAge)
    {
        //Create array of correct size
        long[] sum = new long[maxAge + 2];
        for (int i = 0; i < fishTimers.Length; i++)
        {
            //Setup inital values in array
            sum[fishTimers[i]] += 1;
        }
        for (int i = 0; i < days; i++)
        {
            //Shift first to last position, aka just spawned
            //^1 = last index.
            sum[^1] = sum[0];
            //Add add the once that just had a baby
            sum[birthFrequency + 1] += sum[0];
            for (int k = 1; k < sum.Length; k++)
            {
                //Shift all other to be one day closer to baby time
                sum[k - 1] = sum[k];
            }
            //Clear the once that just spawned
            sum[^1] = 0;
        }

        return sum.Sum();
    }
}
