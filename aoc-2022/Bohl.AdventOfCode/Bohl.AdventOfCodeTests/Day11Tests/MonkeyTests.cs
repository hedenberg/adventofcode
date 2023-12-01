using Bohl.AdventOfCode;
using Bohl.AdventOfCode.Day11;
using Bohl.AdventOfCode.Input;

namespace Bohl.AdventOfCodeTests.Day11Tests;

public class MonkeyTests
{
    [Test]
    public void ParseMonkey()
    {
        var input = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3";

        var monkey = input.Parse<Monkey>();

        Assert.IsNotNull(monkey);
        Assert.That(monkey.Id == 0);
        Assert.That(monkey.Items[0].OriginalWorryLevel == 79);
        Assert.That(monkey.Items[1].OriginalWorryLevel == 98);
        Assert.That(monkey.Operation.Operator == "*");
        Assert.That(monkey.Operation.Value == "19");
        Assert.That(monkey.Divider == 23);
        Assert.That(monkey.TargetTrue == 2);
        Assert.That(monkey.TargetFalse == 3);
    }

    [Test]
    public void Day11_Test1()
    {
        var monkeySolver = Inputs.Day11Tests.Parse<MonkeySolver>();

        monkeySolver.PerformRounds();

        var inspections = monkeySolver.MonkeyBusiness();
        Assert.That(inspections == 10605);
    }

    [Test]
    public void Day11_Challenge1()
    {
        var monkeySolver = Inputs.Day11Challenge.Parse<MonkeySolver>();

        monkeySolver.PerformRounds();

        var inspections = monkeySolver.MonkeyBusiness();
        Assert.That(inspections == 99852);
    }

    //[Test]
    //public void Day11_Test2()
    //{
    //    var monkeySolver = Inputs.Day11Tests.Parse<MonkeySolver>();

    //    monkeySolver.PerformRounds(1000, false);

    //    var monkeys = monkeySolver.Monkeys;

    //    var inspections = monkeySolver.MonkeyBusiness();
    //    Assert.That(inspections == 2713310158);
    //}
}