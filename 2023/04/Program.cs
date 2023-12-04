using System.Collections.Generic;
using System.Text.RegularExpressions;

var cards = File.ReadAllLines("input.txt.");
var cardRegex = new Regex("Card +[0-9]+: +([0-9 ]+)\\| +([0-9 ]+)");

var cardsAmmount = new int[cards.Length];
for(var i =0; i< cardsAmmount.Length; i++)
{
    cardsAmmount[i] = 1;
}
//Part1
var totalPower = 0;
for(var i = 0; i < cards.Length; i++)
{
    var card = cards[i];
    var cardRegexResult = cardRegex.Match(card);
    var winningNumbers= Regex.Replace(cardRegexResult.Groups[1].Value," +", " ").Trim().Split(" ");
    var numbers = Regex.Replace(cardRegexResult.Groups[2].Value, " +", " ").Trim().Split(" ");


    int foundNumbers = 0;
    foreach(var number in numbers)
    {
        var found = Array.Find(winningNumbers, winningNumber => winningNumber == number);
        if (found != null)
        {
            foundNumbers++;
            cardsAmmount[i + foundNumbers] = cardsAmmount[i + foundNumbers] + cardsAmmount[i];
        }
    }

    totalPower += 1 * (int)Math.Pow(2,foundNumbers-1);
}

Console.WriteLine(totalPower);
Console.WriteLine(cardsAmmount.Sum());

Console.ReadLine();