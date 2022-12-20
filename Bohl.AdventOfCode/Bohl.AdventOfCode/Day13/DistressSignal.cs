//using Bohl.AdventOfCode.Input;

//namespace Bohl.AdventOfCode.Day13;

//public class DistressSignal : IParsable<DistressSignal>
//{
//    public List<PacketPair> PacketPairs { get; set; }

//    public int SumCorrectOrder()
//    {
//        var indexes = new List<int>();
//        for (var i = 0; i < PacketPairs.Count; i++)
//        {
//            var packetPair = PacketPairs[i];

//            if (packetPair.IsCorrectOrder())
//                indexes.Add(i);
//        }

//        return indexes.Sum();
//    }

//    public static DistressSignal Parse(string input, IFormatProvider? provider)
//    {
//        var sections = input.Sections();

//        var pairs = new List<PacketPair>();
//        foreach (var section in sections)
//        {
//            var (first, second) = section.Rows();

//            var firstPacket = first.Parse<Packet>();
//            var secondPacket = second.Parse<Packet>();
//            pairs.Add(new PacketPair
//            {
//                First = firstPacket,
//                Second = secondPacket,
//            });
//        }

//        return new DistressSignal
//        {
//            PacketPairs = pairs
//        };
//    }

//    public static bool TryParse(string? s, IFormatProvider? provider, out DistressSignal result)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class PacketPair
//{
//    public Packet First { get; set; }
//    public Packet Second { get; set; }

//    public bool IsCorrectOrder()
//    {
//        return true;
//    }
//}

//public class Packet : IParsable<Packet>
//{
//    public int Value { get; set; }
//    public List<Packet> InternalPackets { get; set; }

//    public void Read(string input)
//    {
//        /*
//            [1,1,3,1,1]
//            [1,1,5,1,1]

//            [[1],[2,3,4]]
//            [[1],4]

//            [9]
//            [[8,7,6]]

//            [[4,4],4,4]
//            [[4,4],4,4,4]

//            [7,7,7,7]
//            [7,7,7]

//            []
//            [3]

//            [[[]]]
//            [[]]

//            [1,[2,[3,[4,[5,6,7]]]],8,9]
//            [1,[2,[3,[4,[5,6,0]]]],8,9]
//         */
//        while (input.Length > 0)
//        {
//            if (input[0] == '[')
//            {

//            }
//        }

//    }

//    public static Packet Parse(string input, IFormatProvider? provider)
//    {
//        if (int.TryParse(input, out var value))
//        {
//            return new Packet
//            {
//                Value = value
//            };
//        }

//        var internalPackets = new List<Packet>();
//        if (input[0] == '[')
//        {
//            // Start of list
//            var endIndex = 0;
//            for (var i = input.Length - 1; i >= 0; i--)
//            {
//                var character = input[i];
//                if (character == ']')
//                {
//                    endIndex = i;
//                    break;
//                }
//            }
//            var packetSubString = input[1..(endIndex-1)];
//            internalPackets.Add(packetSubString.Parse<Packet>());
//            return new Packet
//            {
//                InternalPackets = internalPackets
//            };
//        }

//        var subString = "";
//        foreach (var character in input)
//        {
//            if (character == ',')
//            {
//                internalPackets.Add(subString.Parse<Packet>());
//                continue;
//            }

//            if (character == '[')
//            {

//            }
//            subString += character;
//        }

//        return new Packet
//        {
//        };
//    }

//    public static bool TryParse(string? s, IFormatProvider? provider, out Packet result)
//    {
//        throw new NotImplementedException();
//    }
//}