// See https://aka.ms/new-console-template for more information

using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day10;

Console.WriteLine("Advent of Code 2022");

Console.Write("Day #: ");

var val = "10"; // Console.ReadLine();

if (val == "10")
{
    var program = new Processor();
    program.LoadProgram(Inputs.Day10Challenge);

    Console.Write("Step: ");
    var input = "240"; //Console.ReadLine();
    //while (input != "-1")
    //{
        Console.WriteLine($"Pointer: {program.Pointer}, Clock: {program.ClockCycle}, X = {program.RegisterX}, Signal Strength: {program.SignalStrength}");
        var steps = int.Parse(input);
        while (steps != 0)
        {
            Console.WriteLine($"Run operation: {program.Program[program.Pointer]} {program.Program[program.Pointer].Cycles}");
            program.Run(1);
            Console.WriteLine($"Pointer: {program.Pointer}, Clock: {program.ClockCycle}, X = {program.RegisterX}, Signal Strength: {program.SignalStrength}");
            steps--;
        }

        //Console.Write("Step: ");
        //input = Console.ReadLine();
    //}

    var crtRow1 = program.Pixels[..40];
    var crtRow2 = program.Pixels[40..80];
    var crtRow3 = program.Pixels[80..120];
    var crtRow4 = program.Pixels[120..160];
    var crtRow5 = program.Pixels[160..200];
    var crtRow6 = program.Pixels[200..240];
    Console.WriteLine(crtRow1);
    Console.WriteLine(crtRow2);
    Console.WriteLine(crtRow3);
    Console.WriteLine(crtRow4);
    Console.WriteLine(crtRow5);
    Console.WriteLine(crtRow6);
}