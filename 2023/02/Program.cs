using System.Text.RegularExpressions;



var games = File.ReadAllLines("input.txt");

//Part1
var totalRed = 12;
var totalGreen = 13;
var totalBlue = 14;

var idSum = 0;
var power = 0;
foreach (var game in games)
{
    var gameInfo = game.Split(':');
    if(!int.TryParse(gameInfo[0].Split(" ")[1], out var id))
    {
        throw new InvalidOperationException("Couldn't retrieve id");
    }

    var rounds = gameInfo[1].Split(";");

    var minimumCubes = new Dictionary<string, int>()
    {
        { "green",0 },
        { "blue",0 },
        { "red",0 }
    };

    var valid = true;
    foreach (var round in rounds)
    {
        var totalCubes = new Dictionary<string, int>()
        {
            { "green",0 },
            { "blue",0 },
            { "red",0 }
        };

        var cubesSet = round.Split(",");
        foreach (var cubes in cubesSet)
        {
            var cubesInfo = cubes.Trim().Split(" ");
            if (!int.TryParse(cubesInfo[0], out var qtd))
            {
                throw new InvalidOperationException("Couldn't retrieve qtd");
            }
            totalCubes[cubesInfo[1]] += qtd;

            if(qtd > minimumCubes[cubesInfo[1]]) {
                minimumCubes[cubesInfo[1]] = qtd;
            }
        }
        if (totalCubes["red"] > totalRed || totalCubes["blue"] > totalBlue || totalCubes["green"] > totalGreen)
        {
            valid = false;
        }
    }
    //part1
    if(valid)
    {
        idSum += id;
    }

    //part2
    var minimumRed = 1;
    var minimumGreen = 1;
    var minimumBlue = 1;

    if (minimumCubes["red"] != 0)
    {
        minimumRed = minimumCubes["red"];
    }

    if (minimumCubes["green"] != 0)
    {
        minimumGreen = minimumCubes["green"];
    }

    if (minimumCubes["blue"] != 0)
    {
        minimumBlue = minimumCubes["blue"];
    }
    power += minimumRed * minimumGreen * minimumBlue;
}
//part1
Console.WriteLine(idSum);

//part2
Console.WriteLine(power);

