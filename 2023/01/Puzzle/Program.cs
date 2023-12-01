using System.Collections;


var stringNumbers = new Dictionary<string, int>
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};



var getValue = (string input, int position) => 
{
    if (int.TryParse(input[position].ToString(), out int digit))
    {
        return digit;
    } 
    foreach (var number in stringNumbers.Keys)
    {
        if(position+number.Length > input.Length)
        {
            continue;
        }
        var possibleNumber = input.Substring(position, number.Length);
        if(possibleNumber == number)
        {
            return stringNumbers[number];
        }
    }
    return -1;
};


//Part 1
var input = File.ReadAllLines("input.txt");
int total = 0;
for(int l = 0; l < input.Length; l++)
{
    int firstDigit = -1;
    int lastDigit = -1;
    for (int i = 0; i < input[l].Length; i++)
    {
        var digit = getValue(input[l], i);
        if (digit != -1)
        {
            if (firstDigit == -1)
            {
                firstDigit = digit;
            }
            lastDigit = digit;
        }
    }
    if(int.TryParse($"{firstDigit}{lastDigit}", out int result))
    {
        total += result;
    }
    Console.WriteLine(total);
}
Console.WriteLine(total);
Console.ReadLine();