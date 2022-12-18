// See https://aka.ms/new-console-template for more information

using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day10;
using Bohl.AdventOfCode.Day11;
using Bohl.AdventOfCode.Input;

Console.WriteLine("Advent of Code 2022");

Console.Write("Day #: ");
var val = Console.ReadLine();

if (val == "10")
{
    var program = new Processor();
    program.LoadProgram(Inputs.Day10Challenge);

    program.Run(240);

    Console.WriteLine(program.Pixels[..40]);
    Console.WriteLine(program.Pixels[40..80]);
    Console.WriteLine(program.Pixels[80..120]);
    Console.WriteLine(program.Pixels[120..160]);
    Console.WriteLine(program.Pixels[160..200]);
    Console.WriteLine(program.Pixels[200..240]);
}

if (val == "11")
{
    var monkeySolver = Inputs.Day11Challenge.Parse<MonkeySolver>();

    //Console.WriteLine("20 rounds: ");
    //monkeySolver.PerformRounds(20, false);

    //foreach (var monkey in monkeySolver.Monkeys)
    //{
    //    Console.WriteLine(monkey.ToString());
    //}
    //Console.WriteLine("");

    //Console.WriteLine("1000 rounds: ");
    //monkeySolver.PerformRounds(1000, false);

    //foreach (var monkey in monkeySolver.Monkeys)
    //{
    //    Console.WriteLine(monkey.ToString());
    //}
    //Console.WriteLine("");

    Console.WriteLine("10000 rounds: ");
    monkeySolver.PerformRounds(10000, false);

    foreach (var monkey in monkeySolver.Monkeys)
    {
        Console.WriteLine(monkey.ToString());
    }
    
    Console.WriteLine("");

    Console.WriteLine("========");
    var result = monkeySolver.MonkeyBusiness();
    Console.WriteLine($"Level of monkey business: {result}");
    Console.WriteLine("========");
}