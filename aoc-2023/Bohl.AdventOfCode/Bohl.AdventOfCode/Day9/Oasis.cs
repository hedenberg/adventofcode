namespace Bohl.AdventOfCode.Day9;

internal class Oasis
{
    public static long Sum(string[] input)
    {
        long sum = 0;

        foreach (string s in input)
        {
            var history = s.Split(' ').Select(long.Parse).ToList();

            var prediction = Prediction(history);

            sum += prediction.Last();
        }


        return sum;
    }

    public static List<long> Prediction(List<long> history)
    {
        var diff = 0L;
        if (history.Any(h => h != 0))
        {
            var diffs = new List<long>();
            for (int i = 1; i < history.Count; i++)
            {
                diffs.Add(history[i] - history[i - 1]);
            }

            var next = Prediction(diffs);
            diff = next.Last();
        }

        history.Add(history.Last() + diff);

        return history;
    }
    public static long SumBackwards(string[] input)
    {
        long sum = 0;

        foreach (string s in input)
        {
            var history = s.Split(' ').Select(long.Parse).ToList();

            var prediction = PredictionBackwards(history);

            sum += prediction.First();
        }


        return sum;
    }

    public static List<long> PredictionBackwards(List<long> history)
    {
        var diff = 0L;
        if (history.Any(h => h != 0))
        {
            var diffs = new List<long>();
            for (int i = 1; i < history.Count; i++)
            {
                diffs.Add(history[i] - history[i - 1]);
            }

            var next = PredictionBackwards(diffs);
            diff = next.First();
        }

        var res = new List<long> { history.First() - diff };
        res.AddRange(history);

        return res;
    }
}