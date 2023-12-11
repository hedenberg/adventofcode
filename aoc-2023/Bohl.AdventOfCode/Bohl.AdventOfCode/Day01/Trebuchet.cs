namespace Bohl.AdventOfCode.Day01;

internal class Trebuchet
{
    public static int Sum(List<string> inputs)
    {
        return inputs
            .Select(
                input => (from c in input where char.IsDigit(c) select c - '0').ToList()
                )
            .Select(lineDigits => lineDigits.First() * 10 + lineDigits.Last()).Sum();
    }

    public static int SumWithWords(List<string> inputs)
    {
        var sum = 0;
        foreach (var input in inputs)
        {
            var lineDigits = new List<int>();
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsDigit(c))
                {
                    lineDigits.Add(c - '0');
                }
                else
                {
                    var digits = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    for (var j = 0; j < digits.Length; j++)
                    {
                        var digit = digits[j];
                        if (input.Substring(i).StartsWith(digit))
                        {
                            lineDigits.Add(j + 1);
                        }
                    }
                }
            }

            sum += lineDigits.First() * 10 + lineDigits.Last();
        }

        return sum;
    }

}