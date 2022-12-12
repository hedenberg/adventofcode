using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCode.Day10;

public class Processor
{
    public int ClockCycle { get; set; }
    public int RegisterX { get; set; } = 1;
    public int SignalStrength { get; set; } //=> ClockCycle * RegisterX;
    public int Pointer { get; set; }
    public List<Operation> Program { get; set; } = new();
    public char[] Pixels { get; set; } = new char[240];

    public void LoadProgram(string program)
    {
        Program.Clear();
        var lines = program.Rows();
        foreach (var line in lines) Program.Add(line.Parse<Operation>());
    }

    public void Run(int cycles)
    {
        var operations = Program.ToArray();

        while (Pointer < operations.Length)
        {
            var operation = operations[Pointer];
            ClockCycle++;
            operation.Cycles--;

            SignalStrength = ClockCycle * RegisterX;
            var windowCenter = (ClockCycle - 1) % 40;
            var lower = RegisterX - 1;
            var upper = RegisterX + 1;
            var spriteHit = windowCenter >= lower && windowCenter <= upper;
            Pixels[ClockCycle - 1] = spriteHit ? '#' : '.';

            if (operation.Cycles == 0)
            {
                Pointer++;

                if (operation is AddX addX) RegisterX += addX.Value;
            }

            if (ClockCycle % cycles == 0) return;
        }
    }
}

public abstract class Operation : IParsable<Operation>
{
    public abstract int Cycles { get; set; }

    public static Operation Parse(string input, IFormatProvider? provider)
    {
        var parts = input.Split(' ');
        if (parts.Length == 2)
        {
            var (_, value) = input.Split(' ');
            return new AddX
            {
                Value = int.Parse(value)
            };
        }

        return new Noop();
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Operation result)
    {
        throw new NotImplementedException();
    }
}

public class Noop : Operation
{
    public override int Cycles { get; set; } = 1;

    public override string ToString()
    {
        return "noop";
    }
}

public class AddX : Operation
{
    public override int Cycles { get; set; } = 2;

    public int Value { get; set; }

    public override string ToString()
    {
        return $"addx {Value}";
    }
}