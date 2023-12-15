
using System.Text.RegularExpressions;
using static Hand;

string path = Path.Combine(Environment.CurrentDirectory, "adv_7_INPUT - Copia.txt");
//path = Path.Combine(Environment.CurrentDirectory, "adv_7_INPUT.txt");

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
game.OrderBy(x => x.combo.strength.Value);
List<Hand> finalGame = new List<Hand>();


foreach (var item in game)
{
    if (finalGame.Count() == 0)
    {
        finalGame.Add(item);
        continue;
    }

    if (item.combo.strength > item2.combo.strength)
    {
        finalGame.Insert(finalGame.IndexOf(item2), item);
    }
    else if (item.combo.strength == item2.combo.strength)
    {
        if (CompareCards(item.cards, item2.cards))
        {
            finalGame.Insert(finalGame.IndexOf(item2), item);
        }
        else
        {
            finalGame.Insert(finalGame.IndexOf(item2) + 1, item);
        }
            
    }
    
}

//game = game.OrderBy(x => x.strength).ToList();

//foreach (var item in game.DistinctBy(x => x.strength))
//{

//    Console.WriteLine("C");

//}


public class Hand
{
    public Hand(string _cards, string _money, int nCard)
    {
        this.cards = _cards.ToArray();
        this.money = Convert.ToInt32(_money);
        this.combo = WorthStrength(_cards);
        this.nCard = nCard;
    }

    public class StrengthValue
    {
        public Strength? strength { get; set; }
        public Value? value { get; set; }
    }
    public char[] cards { get; set; }
    public int money { get; set; }
    public int rank { get; set; }
    public int nCard { get; set; }
    public StrengthValue combo { get; set; }

    private StrengthValue WorthStrength(string cards)
    {
        var points = SameChar(cards);
        StrengthValue combo = new StrengthValue();
        if (points.Count() == 1)
        {
            //Tutte carte uguali
            combo.strength = Strength.seven;
            combo.value = ConvertToValue(cards[0]);

            return combo;
        }
        else if (points.Values.Contains(4) && points.Values.Contains(1))
        {
            //POKER
            combo.strength = Strength.six;
            combo.value = ConvertToValue(points.Where(x => x.Value == 4).First().Key);

            return combo;
        }
        else if (points.Values.Contains(3) && points.Values.Contains(2))
        {
            //FULL
            combo.strength = Strength.five;
            combo.value = ConvertToValue(points.Where(x => x.Value == 3).First().Key);

            return combo;
        }
        else if (points.Values.Contains(3) && points.Values.Contains(1))
        {
            //TRIS
            combo.strength = Strength.four;
            combo.value = ConvertToValue(points.Where(x => x.Value == 3).First().Key);

            return combo;
        }
        else if (points.Values.Contains(2) && points.Values.Contains(1) && points.Count == 3)
        {
            //DOPPIA COPPIA
            combo.strength = Strength.three;
            combo.value = ConvertToValue(points.Where(x => x.Value == 2).First().Key);

            return combo;
        }
        else if (points.Values.Count() == 4)
        {
            //COPPIA
            combo.strength = Strength.two;
            combo.value = ConvertToValue(points.Where(x => x.Value == 2).First().Key);

            return combo;
        }
        else if (points.Values.Count() == 5)
        {
            //TUTTE DIVERSE
            combo.strength = Strength.one;
            Dictionary<Value, int> boh = new();
            foreach (var item in points)
            {
                var c = ConvertToValue(item.Key);
                boh.Add(c, value: item.Value);
                boh.OrderBy(x => x.Key);
            }
            combo.value = ConvertToValue(points.OrderBy(x => x.Key).First().Key);
             
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




