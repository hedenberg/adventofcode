// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day10;
using Bohl.AdventOfCode.Day11;
using Bohl.AdventOfCode.Input;

Console.WriteLine("Advent of Code 2022");

const bool debug = true;

var val = "";
if (debug)
{
    val = "12";
}
else
{
    Console.Write("Day #: ");
    val = Console.ReadLine();
}
Console.WriteLine();

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

if (val == "12")
{
    Bohl.AdventOfCode.Day12.Program.Run();
}