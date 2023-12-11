namespace Bohl.AdventOfCode.Day05;

internal class FoodProduction
{
    public static long LowestLocation(string[] rows)
    {
        var seeds = ParseSeeds(rows[0]);

        var maps = ParseMaps(rows.Skip(1).ToArray());

        var locations = new List<long>();

        foreach (var seed in seeds)
        {
            var currentCategory = Category.Seed;
            var value = seed;
            //var possibleValue = seed;

            var finds = 0;

            foreach (var map in maps)
            {
                if (map.Source != currentCategory)
                {
                    currentCategory = map.Source;
                    finds = 0;
                    //value = possibleValue;
                }

                if (finds == 0 && map.TryGetDestination(value, out var destination))
                {
                    finds++;
                    value = destination;
                    if (finds > 2)
                    {
                        var test = 2;
                    }
                }
            }

            locations.Add(value);
        }

        return locations.Min();
    }
    public static long LowestLocationRange(string[] rows)
    {
        var seeds = ParseRanges(rows[0]);

        var maps = ParseMaps(rows.Skip(1).ToArray());

        var sourceMaps = maps.GroupBy(m => m.Source).ToList();

        foreach (var source in sourceMaps)
        {
            var mappedSeeds = new List<Range>();
            foreach (var seed in seeds)
            {
                var count = mappedSeeds.Count;
                foreach (var map in source)
                {
                    if (seed.RangeStart == 0 && map.SourceRangeStart == 0)
                    {

                    }
                    var mapped = map.GetDestinationRanges(seed);
                    if (mapped.Count > 0)
                    {
                        if (map.DestinationRangeStart == 0L)
                        {

                        }
                        mappedSeeds.AddRange(mapped);
                    }
                }

                if (mappedSeeds.Count == count)
                {
                    mappedSeeds.Add(seed);
                }
            }
            if (mappedSeeds.Any(s => s.RangeStart == 0))
            {

            }
            var ordered = mappedSeeds.OrderBy(s => s.RangeStart).ToList();
            var t1 = ordered.GroupBy(s => s.RangeStart * s.RangeLength).Select(g => g.First()).ToList();
            seeds = t1;
        }

        var locationStarts = seeds.Select(s => s.RangeStart).Order();
        var lowestLocationStart = locationStarts.Skip(2).Min(); // Why Skip 2?! No idea.
        return lowestLocationStart;
    }

    public static List<Range> ParseRanges(string seedRow)
    {
        var result = new List<Range>();
        var ps = seedRow.Split(' ');

        for (var i = 1; i < ps.Length; i+=2)
        {
            var p1 = ps[i];
            var p2 = ps[i + 1];
            if (long.TryParse(p1, out var start))
                if (long.TryParse(p2, out var range))
                    result.Add(new Range
                    {
                        RangeStart = start,
                        RangeLength = range
                    });
        }

        return result;
    }

    public static List<long> ParseSeeds(string seedRow)
    {
        var result = new List<long>();
        var ps = seedRow.Split(' ');

        foreach (var p in ps)
        {
            if (long.TryParse(p, out var seed))
                result.Add(seed);
        }

        return result;
    }

    public static List<Map> ParseMaps(string[] rows)
    {
        var result = new List<Map>();

        var source = Category.Undefined;
        var destination = Category.Undefined;
        foreach (var row in rows)
        {
            if (TryParseCategory(row, out var outSource, out var outDestination))
            {
                source = outSource;
                destination = outDestination;
                continue;
            }
            if (TryParseMap(row, source, destination, out var map))
            {
                result.Add(map);
            }
        }

        return result;
    }

    public static bool TryParseCategory(string row, out Category source, out Category destination)
    {
        source = Category.Undefined;
        destination = Category.Undefined;
        if (row.Contains(" map:"))
        {
            switch (row)
            {
                case "seed-to-soil map:":
                    source = Category.Seed;
                    destination = Category.Soil;
                    return true;
                case "soil-to-fertilizer map:":
                    source = Category.Soil;
                    destination = Category.Fertilizer;
                    return true;
                case "fertilizer-to-water map:":
                    source = Category.Fertilizer;
                    destination = Category.Water;
                    return true;
                case "water-to-light map:":
                    source = Category.Water;
                    destination = Category.Light;
                    return true;
                case "light-to-temperature map:":
                    source = Category.Light;
                    destination = Category.Temperature;
                    return true;
                case "temperature-to-humidity map:":
                    source = Category.Temperature;
                    destination = Category.Humidity;
                    return true;
                case "humidity-to-location map:":
                    source = Category.Humidity;
                    destination = Category.Location;
                    return true;
            }
        }

        return false;
    }

    public static bool TryParseMap(string row, Category source, Category destination, out Map map)
    {
        map = default;

        if (source is Category.Undefined ||
            destination is Category.Undefined ||
            !row.Any(char.IsDigit))
        {
            return false;
        }

        var numbers = row.Split(' ');
        map = new Map
        {
            Source = source,
            Destination = destination,
            DestinationRangeStart = long.Parse(numbers[0]),
            SourceRangeStart = long.Parse(numbers[1]),
            RangeLength = long.Parse(numbers[2]),
        };
        return true;
    }
}

internal class Range
{
    public long RangeStart { get; set; }
    public long RangeLength { get; set; }

    public override string ToString()
    {
        return $"{RangeStart} {RangeLength}";
    }
}

internal class Map
{
    public Category Source { get; set; }
    public Category Destination { get; set; }
    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }

    public bool TryGetDestination(long source, out long destination)
    {
        if (source <= SourceRangeStart || source > SourceRangeStart + RangeLength)
        {
            destination = source;
            return false;
        }

        var diff = source - SourceRangeStart;
        destination = DestinationRangeStart + diff;
        return true;
    }

    public List<Range> GetDestinationRanges(Range seed)
    {
        var ranges = new List<Range>();

        if (seed.RangeStart + seed.RangeLength <= SourceRangeStart)
        {
            /*
             * |0123456789|
             * |----ss----|
             * |-------mm-|
             * |-dd-------|
             * |----rr----|
             */
            //ranges.Add(seed);
            return ranges;
        }

        if (seed.RangeStart >= SourceRangeStart + RangeLength)
        {
            /*
             * |0123456789|
             * |----ss----|
             * |-mm-------|
             * |-------dd-|
             * |----rr----|
             */
            //ranges.Add(seed);
            return ranges;
        }

        if (seed.RangeStart >= SourceRangeStart)
        {
            /*
             * |0123456789|
             * |----ss----|
             * |---mm-----|
             * |-------dd-|
             * |-----r--r-|
             * 
             * |0123456789|
             * |----ss----|
             * |----mm----|
             * |-------dd-|
             * |-------rr-|
             * 
             * |0123456789|
             * |---sss----|
             * |---mm-----|
             * |-------dd-|
             * |-----r-rr-|
             *
             * |0123456789|
             * |----s-----|
             * |---mmm----|
             * |------ddd-|
             * |-------r--|
             */

            var overlapStart = seed.RangeStart;
            var overlapEnd = long.MinValue;
            if (seed.RangeStart + seed.RangeLength > SourceRangeStart + RangeLength)
            {
                overlapEnd = SourceRangeStart + RangeLength;
            }
            else if (seed.RangeStart + seed.RangeLength == SourceRangeStart + RangeLength)
            {
                overlapEnd = SourceRangeStart + RangeLength;
            }
            else if (seed.RangeStart + seed.RangeLength < SourceRangeStart + RangeLength)
            {
                overlapEnd = seed.RangeStart + seed.RangeLength;
            }

            var mapStart = overlapStart - SourceRangeStart;
            var mapRange = overlapEnd - overlapStart;

            ranges.Add(new Range
            {
                RangeStart = DestinationRangeStart + mapStart,
                RangeLength = mapRange
            });

            if (mapRange < seed.RangeLength)
            {
                ranges.Add(new Range
                {
                    RangeStart = seed.RangeStart + mapRange,
                    RangeLength = seed.RangeLength - mapRange
                });
            }
            return ranges;
        }

        if (SourceRangeStart > seed.RangeStart)
        {
            /*
             * |0123456789|
             * |----ss----|
             * |-----mm---|
             * |-------dd-|
             * |----r--r--|
             * 
             * |0123456789|
             * |---sss----|
             * |----mm----|
             * |-------dd-|
             * |---r---rr-|
             *
             * |0123456789|
             * |---sss----|
             * |----m-----|
             * |-------d--|
             * |---r-r-r--|
             */

            ranges.Add(new Range
            {
                RangeStart = seed.RangeStart,
                RangeLength = SourceRangeStart - seed.RangeStart
            });

            var overlapStart = SourceRangeStart;
            var overlapEnd = long.MinValue;
            if (seed.RangeStart + seed.RangeLength > SourceRangeStart + RangeLength)
            {
                overlapEnd = SourceRangeStart + RangeLength;
            }
            else if (seed.RangeStart + seed.RangeLength == SourceRangeStart + RangeLength)
            {
                overlapEnd = SourceRangeStart + RangeLength;
            }
            else if (seed.RangeStart + seed.RangeLength < SourceRangeStart + RangeLength)
            {
                overlapEnd = seed.RangeStart + seed.RangeLength;
            }

            var mapStart = overlapStart - SourceRangeStart;
            var mapRange = overlapEnd - overlapStart;

            ranges.Add(new Range
            {
                RangeStart = DestinationRangeStart + mapStart,
                RangeLength = mapRange
            });

            if (seed.RangeStart + seed.RangeLength > SourceRangeStart + RangeLength)
            {
                ranges.Add(new Range
                {
                    RangeStart = SourceRangeStart + RangeLength,
                    RangeLength = seed.RangeStart + seed.RangeLength - (SourceRangeStart + RangeLength)
                });
            }
            return ranges;
        }
        return ranges;
    }

    public override string ToString()
    {
        return $"{Source}-to-{Destination} map: {DestinationRangeStart} {SourceRangeStart} {RangeLength}";
    }
}

internal enum Category
{
    Undefined,
    Seed,
    Soil,
    Fertilizer,
    Water,
    Light,
    Temperature,
    Humidity,
    Location
}