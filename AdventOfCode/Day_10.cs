namespace AdventOfCode;

public class Day_10 : BaseDay
{
    private readonly string _input;
    string openers = "([{<";
    string closers = ")]}>";
    List<(char, int)> scoreList = new List<(char, int)>() { (')', 3), (']', 57), ('}', 1197), ('>', 25137) };
    public Day_10()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        //Parse instructions
        var lines = _input.Split("\r\n");
        var result = 0;
        foreach (string line in lines)
        {
            Stack<char> stack = new();
            foreach (char ch in line)
            {
                if (openers.Contains(ch))
                {
                    stack.Push(ch);
                }
                else 
                {
                    var opener = stack.Pop();
                    if (openers.IndexOf(opener) != closers.IndexOf(ch))
                    {
                        //Get the score
                        var score = scoreList.First(x => x.Item1 == ch);
                        result += score.Item2;
                        break;
                    }
                }
            }
        }
        return ValueTask.FromResult(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        //Parse instructions
        var lines = _input.Split("\r\n");
        var completionScore = lines
            .Where(LineValid)
            .Select(CompleteLine).ToList();
        var result = completionScore
            .OrderBy(x => x)
            .ElementAt(completionScore.Count / 2);

        return ValueTask.FromResult(result.ToString());
    }

    long CompleteLine(string line)
    {
        Stack<char> stack = new();
        foreach (var ch in line)
        {
            if (openers.Contains(ch))
            {
                stack.Push(ch);
            }
            else
            {
                _ = stack.Pop();
            }
        }
        long score = 0;
        // For each leftover unclosed character
        foreach (char c in stack)
        {
            score *= 5;
            score += openers.IndexOf(c) + 1;
        }
        return score;
        //return lineScore.Aggregate(0, (acc, cur) => acc * 5 + cur);
        //Compute score
    }

    bool LineValid(string line)
    {
        Stack<char> stack = new();
        foreach (char ch in line)
        {
            if (openers.Contains(ch))
            {
                stack.Push(ch);
            }
            else
            {
                var opener = stack.Pop();
                if (openers.IndexOf(opener) != closers.IndexOf(ch))
                {
                    return false;
                }
            }
        }
        return true;
    }
}
