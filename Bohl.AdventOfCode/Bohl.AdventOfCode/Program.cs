// See https://aka.ms/new-console-template for more information

using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day10;

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