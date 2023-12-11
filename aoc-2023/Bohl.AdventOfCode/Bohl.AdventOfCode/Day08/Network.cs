namespace Bohl.AdventOfCode.Day08;

internal class Network
{
    public static int Steps(string[] input)
    {
        var instructions = input[0].ToCharArray();

        var nodes = input.Skip(2).Select(Node.Parse).ToList();


        var steps = 0;
        var instructionIndex = 0;
        var currentNode = nodes.Single(n => n.Id == "AAA");
        while (currentNode.Id != "ZZZ")
        {
            currentNode.Visited++;
            var instruction = instructions[instructionIndex++];
            if (instructionIndex == instructions.Length)
            {
                instructionIndex = 0;
            }

            if (instruction == 'L')
            {
                currentNode = nodes.Single(n => n.Id == currentNode.Left);
            }
            else
            {
                currentNode = nodes.Single(n => n.Id == currentNode.Right);
            }

            steps++;
        }

        return steps;
    }

    public static long GhostSteps(string[] input)
    {
        var instructions = input[0].ToCharArray();
        var nodes = input.Skip(2).Select(Node.Parse).ToList();

        var stepsList = new List<long>();
        var currentNodes = nodes.Where(n => n.Id[2] == 'A').ToList();

        foreach (var thisNode in currentNodes)
        {
            var steps = 0L;
            var instructionIndex = 0;
            var currentNode = thisNode;
            while (currentNode.Id[2] != 'Z')
            {
                currentNode.Visited++;
                var instruction = instructions[instructionIndex++];
                if (instructionIndex == instructions.Length)
                {
                    instructionIndex = 0;
                }

                if (instruction == 'L')
                {
                    currentNode = nodes.Single(n => n.Id == currentNode.Left);
                }
                else
                {
                    currentNode = nodes.Single(n => n.Id == currentNode.Right);
                }

                steps++;
            }

            stepsList.Add(steps);
        }

        //var gcd = stepsList.Aggregate(GCD);

        //var modifiedStepsList = stepsList.Select(s => s / gcd);

        var res = 1L;
        foreach (var s in stepsList)
        {
            res = (s * res) / GCD(s, res);
        }
        return res;
    }

    static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

}

internal class Node
{
    public string Id { get; set; }
    public string Left { get; set; }
    public string Right { get; set; }
    public int Visited { get; set; } = 0;

    public static Node Parse(string input)
    {
        var s1 = input.Split('=');
        var i = s1[0].Replace(" ", "");
        var s2 = s1[1].Split(',');
        var l = s2[0].Replace("(", "").Replace(" ", "");
        var r = s2[1].Replace(")", "").Replace(" ", "");

        return new Node
        {
            Id = i,
            Left = l,
            Right = r
        };
    }

    public override string ToString()
    {
        return $"{Id} = ({Left}, {Right}) {Visited}";
    }
}