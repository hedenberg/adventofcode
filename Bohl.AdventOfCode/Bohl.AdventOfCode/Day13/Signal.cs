using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day13;

public class Signal : IParsable<Signal>
{
    public List<Packet> Lefts { get; set; }
    public List<Packet> Rights { get; set; }

    public int SumAreEven()
    {
        for (var i = 0; i < Lefts.Count; i++)
        {
            var leftPacket = Lefts[i];
            var rightPacket = Rights[i];

            if (leftPacket.Value is null)
            {

            }
        }
        throw new NotImplementedException();
    }

    public static Signal Parse(string input, IFormatProvider? provider)
    {
        var sections = input.Sections();

        var lefts = new List<Packet>();
        var rights = new List<Packet>();

        foreach (var section in sections)
        {
            var (left, right) = section.Rows();

            var leftTokens = Token.Tokenizer(left);
            var rightTokens = Token.Tokenizer(right);

            var leftParser = new Parser(leftTokens);
            var rightParser = new Parser(rightTokens);

            var leftPacket = leftParser.Walk();
            var rightPacket = rightParser.Walk();

            lefts.Add(leftPacket);
            rights.Add(rightPacket);
        }

        return new Signal
        {
            Lefts = lefts,
            Rights = rights,
        };
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Signal result)
    {
        throw new NotImplementedException();
    }
}

public class Parser
{
    public int Current { get; set; } = 0;
    public List<Token> Tokens { get; set; }

    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
    }

    public Packet Walk()
    {
        var token = Tokens[Current];

        if (token.TokenType == TokenType.StartBracket)
        {
            Current++;
            var packets = new List<Packet>();

            while (token.TokenType != TokenType.EndBracket)
            {
                var packet = Walk();
                packets.Add(packet);
                token = Tokens[Current];
                if (token.TokenType == TokenType.Comma)
                {
                    Current++;
                    token = Tokens[Current];
                }
            }
            Current++;

            return new Packet
            {
                Packets = packets
            };
        }

        if (token.TokenType == TokenType.Integer)
        {
            var numberStr = "";
            while (token.TokenType == TokenType.Integer)
            {
                numberStr += token.Value;
                Current++;
                if (Current == Tokens.Count)
                    break;
                token = Tokens[Current];
            }

            if (int.TryParse(numberStr, out var number))
            {
                return new Packet
                {
                    Value = number
                };
            }
        }


        return new Packet();
    }
}

public class Packet
{
    public int? Value { get; set; }
    public List<Packet> Packets { get; set; }


    public override string ToString()
    {
        return $"Value: {Value} Packets: {string.Join(",", Packets)}";
    }
}

public enum PacketType
{
    List,
    Integer
}

public class Token
{
    public TokenType TokenType { get; set; }
    public char Value { get; set; }

    public static List<Token> Tokenizer(string input)
    {
        var tokens = new List<Token>();

        foreach (var character in input)
        {
            switch (character)
            {
                case '[':
                    tokens.Add(new Token
                    {
                        TokenType = TokenType.StartBracket,
                        Value = character
                    });
                    continue;
                case ']':
                    tokens.Add(new Token
                    {
                        TokenType = TokenType.EndBracket,
                        Value = character
                    });
                    continue;
                case ',':
                    tokens.Add(new Token
                    {
                        TokenType = TokenType.Comma,
                        Value = character
                    });
                    continue;
            }
            if (int.TryParse(character.ToString(), out var value))
            {
                tokens.Add(new Token
                {
                    TokenType = TokenType.Integer,
                    Value = character
                });
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return tokens;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}

public enum TokenType
{
    StartBracket,
    EndBracket,
    Comma,
    Integer
}