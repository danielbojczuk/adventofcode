// See https://aka.ms/new-console-template for more information
using System.Runtime.ExceptionServices;

class Hand
{
    private Dictionary<char, string> cardsWeight = new Dictionary<char, string>()
    {
        { '2',"02" },
        { '3',"03" },
        { '4',"04" },
        { '5',"05" },
        { '6',"06" },
        { '7',"07" },
        { '8',"08" },
        { '9',"09" },
        { 'T',"10" },
        { 'J',"11" },
        { 'Q',"12" },
        { 'K',"13" },
        { 'A',"14" },
    };
    public Dictionary<char,int> Cards { get; private set; }
    public int Bid { get; private set; }
    public Int64 Weight { get; private set; }

    public Hand(char[] cards, int bid)
    {
        Cards = new Dictionary<char, int>();
        Bid = bid;

        
        var weight = string.Empty;
        foreach (var card in cards)
        { 
            weight += cardsWeight[card];
        }

        foreach (var card in cards.OrderBy(c => int.Parse(cardsWeight[c])).ToArray())
        {
            if (Cards.ContainsKey(card))
            {
                Cards[card]++;
            }
            else
            {
                Cards[card] = 1;
            }
        }

        var orderedCards = Cards.OrderBy(c => c.Value).ToArray();
        //Five of a kind
        if (orderedCards.Count() == 1)
        {
            weight = "7"+ weight;
        }
        
        if (orderedCards.Count() == 2)
        {
            //Four of a kind
            if (orderedCards[0].Value == 1)
            {
                weight = "6" + weight;
            }
            //Full house
            if (orderedCards[0].Value == 2)
            {
                weight = "5" + weight;
            }
        }

       
        if (orderedCards.Count() == 3)
        {
            //Three of a kind
            if (orderedCards[0].Value == 1 && orderedCards[1].Value == 1)
            {
                weight = "4" + weight;
            }
            //Two pair
            if (orderedCards[0].Value == 1 && orderedCards[1].Value == 2)
            {
                weight = "3" + weight;
            }
        }

        //One pair
        if (orderedCards.Count() == 4)
        {
            weight = "2" + weight;
        }

        //High card
        if (orderedCards.Count() == 5)
        {
            weight = "1" + weight;
        }

        Weight = Int64.Parse(weight);
    }

}

class Program {

    public static void Main()
    {
        var input = File.ReadAllLines("input.txt");
        var hands = new Hand[input.Length];

        var counter = 0;
        foreach(var line in input)
        {
            var handInformatiom = line.Split(' ');
            hands[counter] = new Hand(handInformatiom[0].ToCharArray(), int.Parse(handInformatiom[1]));
            counter++;
        }
        hands = hands.OrderBy(h => h.Weight).ToArray();

        var totalWinnings = 0;
        for(var i =0; i < hands.Length; i++)
        {
            totalWinnings += (i + 1) * hands[i].Bid;
        }
        Console.WriteLine(totalWinnings);
        Console.ReadLine();
    }
}