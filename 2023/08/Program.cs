using System.Text.RegularExpressions;

class NodeInformation
{
    public string Left { get; set; }
    public string Right { get; set; }

    public string GetNode(char side)
    {
        return side switch
        {
            'L' => Left,
            'R' => Right,
            _ => throw new InvalidOperationException("Sides need to be L or F"),
        };
    }
}

class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt");

        var directions = input[0];

        var parsingRegex = new Regex("([A-Z]+) = \\(([A-Z]+), ([A-Z]+)\\)");
        
        var nodes = new Dictionary<string, NodeInformation>();
        for (var i = 2; i< input.Length; i++) 
        {
            var parsing = parsingRegex.Match(input[i]);
            var nodeValue = parsing.Groups[1].Value;
            var left = parsing.Groups[2].Value;
            var right = parsing.Groups[3].Value;
            nodes.Add(nodeValue, new NodeInformation { Left = left, Right = right });
        }

        var currentNode = "AAA";
        var instructionPointer = 0;
        var instructionMaxPointer = directions.Length - 1;
        var steps = 0;
        while(currentNode != "ZZZ")
        {
            currentNode = nodes[currentNode].GetNode(directions[instructionPointer]);
            steps++;

            if(instructionPointer < instructionMaxPointer)
            {
                instructionPointer++;
            } else
            {
                instructionPointer = 0;
            }
            
        }
        Console.WriteLine(steps);
    }
}