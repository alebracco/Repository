
using System.Text.RegularExpressions;
using static Hand;

string path = Path.Combine(Environment.CurrentDirectory, "adv_7_INPUT - Copia.txt");
path = Path.Combine(Environment.CurrentDirectory, "adv_7_INPUT.txt");

List<Hand> game = new List<Hand>();

using (FileStream fl = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fl))
    {
        do
        {
            string? line = fileReader.ReadLine();
            string[] splittedLine = line.Split(" ");
            Hand hand = new Hand(splittedLine[0], splittedLine[1], splittedLine[0].Count());
            game.Add(hand);

        } while (!fileReader.EndOfStream);
    }
}
game = game.OrderBy(x => x.strength).ThenBy(x => x.cards[0]).ThenBy(x => x.cards[1]).ThenBy(x => x.cards[2]).ThenBy(x => x.cards[3]).ThenBy(x => x.cards[4]).ToList();

int cont = 1;
foreach (Hand hand in game)
{
    hand.rank = cont;
    Console.WriteLine($"Mano: {hand.descrCards}, Money:{hand.money}, Valore alto: {hand.strength}, Rank: {hand.rank}");
    cont++;
}

int total = 0;
foreach (Hand hand in game)
{
    total += hand.rank * hand.money;
}

Console.WriteLine($"{total}");

//game = game.OrderBy(x => x.strength).ToList();

//foreach (var item in game.DistinctBy(x => x.strength))
//{

//    Console.WriteLine("C");

//}


public class Hand
{


    public Hand(string _cards, string _money, int nCard)
    {
        int cont = 0;
        foreach (var c in _cards)
        {
            this.cards[cont] = ConvertToValue(c);
            cont++;
        }
        this.money = Convert.ToInt32(_money);
        this.strength = WorthStrength(_cards);
        this.descrCards = _cards;
    }
    public Value[] cards = new Value[5];
    public Strength strength { get; set; }
    public int money { get; set; }
    public int rank { get; set; }
    public string descrCards { get; set; }

    private Strength WorthStrength(string cards)
    {
        var points = SameChar(cards);
        Strength combo = new Strength();
        if (points.Count() == 1)
        {
            //Tutte carte uguali
            combo = Strength.seven;
            return combo;
        }
        else if (points.Values.Contains(4) && points.Values.Contains(1))
        {
            //POKER
            combo = Strength.six;

            return combo;
        }
        else if (points.Values.Contains(3) && points.Values.Contains(2))
        {
            //FULL
            combo = Strength.five;

            return combo;
        }
        else if (points.Values.Contains(3) && points.Values.Contains(1))
        {
            //TRIS
            combo = Strength.four;

            return combo;
        }
        else if (points.Values.Contains(2) && points.Values.Contains(1) && points.Count == 3)
        {
            //DOPPIA COPPIA
            combo = Strength.three;

            return combo;
        }
        else if (points.Values.Count() == 4)
        {
            //COPPIA
            combo = Strength.two;

            return combo;
        }
        else if (points.Values.Count() == 5)
        {
            //TUTTE DIVERSE
            combo = Strength.one;
             
            return combo;
        }

        return combo;
    }
    public static Value ConvertToValue(char card)
    {
        switch (card)
        {
            case '2':
                return Value.due;
            case '3':
                return Value.tre;
            case '4':
                return Value.quattro;
            case '5':
                return Value.cinque;
            case '6':
                return Value.sei;
            case '7':
                return Value.sette;
            case '8':
                return Value.otto;
            case '9':
                return Value.nove;
            case 'T':
                return Value.T;
            case 'J':
                return Value.J;
            case 'Q':
                return Value.Q;
            case 'K':
                return Value.k;
            case 'A':
                return Value.A;
        }
        return Value._;
    }

    public static Dictionary<char,int> SameChar(string card)
    {
        Dictionary<char, int> charCount = new Dictionary<char, int>();
        foreach (var item in card.Distinct())
        {
            charCount.Add(item, card.Where(x => x == item).Count());
        }
        return charCount;
    }

    public static bool CompareCards(char[] cards, char[] cards2)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (ConvertToValue(cards[i]) > ConvertToValue(cards2[i]))
            {
                return true;
            }
            else if (ConvertToValue(cards[i]) < ConvertToValue(cards2[i]))
            {
                return false;
            } 
        }
        return false;
    }
    public enum Strength
    {
        one = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5,
        six = 6,
        seven = 7,
    }

    public enum Value
    {
        _ = 0,
        due = 2,
        tre = 3,
        quattro = 4,
        cinque = 5,
        sei = 6,
        sette = 7,
        otto = 8,
        nove = 9,
        T = 10,
        J = 11,
        Q = 12,
        k = 13,
        A = 14,
    }
}




