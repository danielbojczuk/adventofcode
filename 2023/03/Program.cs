using System.Text;

var schematic = File.ReadAllLines("input.txt");
var numbers = new Dictionary<string, int>();
var totalGearPowers = 0;

var GetLeft = (int x, int y, ref int totalAdjacentNumbers, ref int adjacentNumbersMultiplication) =>
{
    var line = schematic[y];
    var leftNumber = "";
    var position = x;
    if (x - 1 >= 0)
    {
        for (var l = x - 1; l >= 0; l--)
        {
            if (int.TryParse(line[l].ToString(), out var digit))
            {
                leftNumber = $"{digit}{leftNumber}";
                position = l;
            }
            else
            {
                break;
            }
        }
        if (int.TryParse(leftNumber, out var intLeftNumber))
        {
            numbers.Add($"{y},{position}", intLeftNumber);
            totalAdjacentNumbers++;
            adjacentNumbersMultiplication = adjacentNumbersMultiplication * intLeftNumber;
        }
    }
};

var GetRight = (int x, int y, ref int totalAdjacentNumbers, ref int adjacentNumbersMultiplication) =>
{
    var line = schematic[y];
    var rifhtNumber = "";
    var position = x + 1;
    if (x + 1 < line.Length)
    {
        for (var r = x + 1; r < line.Length; r++)
        {
            if (int.TryParse(line[r].ToString(), out var digit))
            {
                rifhtNumber = $"{rifhtNumber}{digit}";
            }
            else
            {
                break;
            }
        }
        if (int.TryParse(rifhtNumber, out var intrightNumber))
        {
            numbers.Add($"{y},{position}", intrightNumber);
            totalAdjacentNumbers++;
            adjacentNumbersMultiplication = adjacentNumbersMultiplication * intrightNumber;

        }
    }
};

var GetOtherLine = (int x, int y, ref int totalAdjacentNumbers, ref int adjacentNumbersMultiplication) =>
{
    if (x < 0 || x >= schematic.Length)
    {
        return false;
    }
    var line = schematic[y];

    if (!int.TryParse(line[x].ToString(), out var centralDigit))
    {
        return false;
    }
    var number = $"{centralDigit}";
    var position = x;
    var leftPart = "";
    if (x - 1 >= 0)
    {
        for (var l = x - 1; l >= 0; l--)
        {
            if (int.TryParse(line[l].ToString(), out var digit))
            {
                leftPart = $"{digit}{leftPart}";
                position = l;
            }
            else
            {
                break;
            }
        }
        number = $"{leftPart}{number}";
    }

    var rightPart = "";
    if (x + 1 < line.Length)
    {
        for (var r = x + 1; r < line.Length; r++)
        {
            if (int.TryParse(line[r].ToString(), out var digit))
            {
                rightPart = $"{rightPart}{digit}";
            }
            else
            {
                break;
            }
        }
        number = $"{number}{rightPart}";
    }
    if (int.TryParse(number, out var intNumber))
    {
        numbers.Add($"{y},{position}", intNumber);
        totalAdjacentNumbers++;
        adjacentNumbersMultiplication = adjacentNumbersMultiplication * intNumber;

    }
    return true;
};



for (int y = 0; y <schematic.Length; y++)
{
    var line = schematic[y];
    for (var x = 0; x < line.Length; x++)
    {
        if (int.TryParse(line[x].ToString(), out var _) || line[x] == '.')
        {
            continue;
        }
        var totalAdjacentNumbers = 0;
        var adjacentNumbersMultiplication = 1;
        GetLeft(x, y, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication);
        GetRight(x, y, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication);
        if(!GetOtherLine(x, y - 1, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication))
        {
            GetLeft(x, y - 1, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication);
            GetRight(x, y - 1, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication);
        }
        if (!GetOtherLine(x, y + 1, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication))
        {
            GetLeft(x, y + 1, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication);
            GetRight(x, y + 1, ref totalAdjacentNumbers, ref adjacentNumbersMultiplication);
        }

        if (line[x] == '*' && totalAdjacentNumbers == 2)
        {
            totalGearPowers += adjacentNumbersMultiplication;
        }
    }
}
var total = 0;
foreach(var number in numbers)
{
    total += number.Value;
}
Console.WriteLine(total);
Console.WriteLine(totalGearPowers);

Console.ReadLine();