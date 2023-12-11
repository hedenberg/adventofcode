namespace Bohl.AdventOfCode.Day06;

internal class BoatRace
{
    public static long NumberOfWins(string[] input, bool ignoreSpaces = false)
    {
        var timeDistances = TimeDistance.Parse(input, ignoreSpaces);

        var numberOfWins = timeDistances.Select(FindNumberOfWins).ToList();

        return numberOfWins.Aggregate(1L, (x,y) => x * y);
    }

    public static long FindNumberOfWins(TimeDistance timeDistance)
    {
        var first = 0L;
        for (long i = 0L; i < timeDistance.Time; i++)
        {
            if (i * (timeDistance.Time - i) <= timeDistance.Distance) 
                continue;
            first = i;
            break;
        }

        var last = timeDistance.Time;
        for (long i = timeDistance.Time; i > 0; i--)
        {
            if (i * (timeDistance.Time - i) <= timeDistance.Distance) 
                continue;
            last = i + 1;
            break;
        }

        return last - first;
    }
}

internal class TimeDistance
{
    public long Time { get; set; }
    public long Distance { get; set; }

    public static List<TimeDistance> Parse(string[] input, bool ignoreSpaces = false)
    {
        var timeRow = input[0].Replace("Time:", "");
        if (ignoreSpaces)
            timeRow = timeRow.Replace(" ", "");
        var distanceRow = input[1].Replace("Distance:", "");
        if (ignoreSpaces)
            distanceRow = distanceRow.Replace(" ", "");

        var timeStrings = timeRow.Split(" ");
        var times = new List<long>();
        foreach (var timeString in timeStrings)
        {
            if (long.TryParse(timeString, out var time))
                times.Add(time);
        }

        var distanceStrings = distanceRow.Split(" ");
        var distances = new List<long>();
        foreach (var distanceString in distanceStrings)
        {
            if (long.TryParse(distanceString, out var distance))
                distances.Add(distance);
        }

        var result = new List<TimeDistance>();

        for (var i = 0; i < times.Count; i++)
        {
            var time = times[i];
            var distance = distances[i];

            result.Add(new TimeDistance
            {
                Time = time,
                Distance = distance
            });
        }

        return result;
    }

    public override string ToString()
    {
        return $"Time: {Time} - Distance: {Distance}";
    }
}