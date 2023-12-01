using Bohl.AdventOfCode.Input;
using System;

namespace Bohl.AdventOfCode.Day15;

public class Zone : IParsable<Zone>
{
    public List<Item> Items { get; set; }

    public static Zone Parse(string input, IFormatProvider? provider)
    {
        var masterItems = new List<Item>();
        var rows = input.Rows();

        var threads = new List<Thread>();

        var itemLists = new List<Item>[rows.Length];

        for (var index = 0; index < rows.Length; index++)
        {
            var row = rows[index];

            itemLists[index] = new List<Item>();
            var i = index;
            var thread = new Thread(() =>
            {
                var inputParts = row.Split(": closest beacon is at x=");
                var (sensorX, sensorY) = inputParts[0].Split("Sensor at x=")[1].Split(", y=").Select(int.Parse).ToArray();
                var (beaconX, beaconY) = inputParts[1].Split(", y=").Select(int.Parse).ToArray();

                var sensor = new Item
                {
                    ZoneType = ZoneType.Sensor,
                    Position = new Position
                    {
                        X = sensorX,
                        Y = sensorY
                    }
                };

                var beacon = new Item
                {
                    ZoneType = ZoneType.Beacon,
                    Position = new Position
                    {
                        X = beaconX,
                        Y = beaconY
                    }
                };


                itemLists[i].Add(sensor);

                var empty = itemLists[i].SingleOrDefault(i => Equals(i.Position, beacon.Position) && i.ZoneType is ZoneType.Empty);
                if (empty is not null)
                {
                    itemLists[i].Remove(empty);
                }

                if (!itemLists[i].Any(i => i.ZoneType is ZoneType.Beacon && i.Position.X == beaconX && i.Position.Y == beaconY))
                {
                    itemLists[i].Add(beacon);
                }

                var emptyItems = GenerateEmptyItems(sensor, sensor.Distance(beacon))
                    .Where(e => !itemLists[i].Any(i => Equals(i.Position, e.Position)));

                itemLists[i].AddRange(emptyItems);
            });
            thread.Start();
            threads.Add(thread);
        }


        for (var index = 0; index < threads.Count; index++)
        {
            var thread = threads[index];
            thread.Join();
            var items = itemLists[index].Where(i => !masterItems.Any(m => Equals(m.Position, i.Position))).ToList();
            masterItems.AddRange(items);
        }
        
        return new Zone
        {
            Items = masterItems
        };
    }

    public char[,] GetMap()
    {
        var minX = Items.Min(i => i.Position.X);
        var maxX = Items.Max(i => i.Position.X);
        var minY = Items.Min(i => i.Position.Y);
        var maxY = Items.Max(i => i.Position.Y);

        var width = maxX - minX + 1;
        var height = maxY - minY + 1;

        var map = new char[width, height];

        for (var k = 0; k < map.GetLength(0); k++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                var x = minX + k;
                var y = minY + j;
                map[k, j] = '.';
                var items = Items.Where(i => i.Position.X == x && i.Position.Y == y).ToList();
                if (items.Count == 1)
                {
                    map[k, j] = items.First().ZoneType switch
                    {
                        ZoneType.Sensor => 'S',
                        ZoneType.Beacon => 'B',
                        ZoneType.Empty => '#',
                        _ => '.'
                    };
                }
                else if (items.Any())
                {
                    var sensorItems = items.Where(i => i.ZoneType is ZoneType.Sensor);
                    var beaconItems = items.Where(i => i.ZoneType is ZoneType.Beacon);

                    if (beaconItems.Any())
                        map[k, j] = 'B';
                    else if (sensorItems.Any())
                        map[k, j] = 'S';
                    else
                        map[k, j] = '#';
                }
            }
        }

        return map;
    }

    public static List<Item> GenerateEmptyItems(Item origin, int distance)
    {
        var items = new List<Item>();

        for (var x = -distance; x <= distance; x++)
        {
            for (var y = -distance; y <= distance; y++)
            {
                var item = new Item
                {
                    ZoneType = ZoneType.Empty,
                    Position = new Position
                    {
                        X = origin.Position.X + x,
                        Y = origin.Position.Y + y
                    }
                };
                if (item.Distance(origin) <= distance)
                    items.Add(item);
            }
        }

        return items;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Zone result)
    {
        throw new NotImplementedException();
    }
}

public class Item
{
    public ZoneType ZoneType { get; set; }
    public Position Position { get; set; }

    public int Distance(Item i2)
    {
        // Eg. Sensor 8,7 and Beacon 2, 10 distance is 9
        // 8-2 = 6
        // 10-7 = 3
        // 6 + 3 = 9

        var maxX = Position.X > i2.Position.X ? Position.X : i2.Position.X;
        var minX = Position.X < i2.Position.X ? Position.X : i2.Position.X;
        var maxY = Position.Y > i2.Position.Y ? Position.Y : i2.Position.Y;
        var minY = Position.Y < i2.Position.Y ? Position.Y : i2.Position.Y;
        return maxX - minX + (maxY - minY);
    }

    public override string ToString()
    {
        return ZoneType switch
        {
            ZoneType.Sensor => $"S ({Position.X},{Position.Y})",
            ZoneType.Beacon => $"B ({Position.X},{Position.Y})",
            ZoneType.Empty => $"# ({Position.X},{Position.Y})",
            _ => $". ({Position.X},{Position.Y})"
        };
    }
}

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }
}

public enum ZoneType
{
    Unknown = 0,
    Sensor,
    Beacon,
    Empty
}