using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

string path = Path.Combine(Environment.CurrentDirectory, "adv_4_INPUT.txt");
List<string[]> supportString = new List<string[]>();
Dictionary<int, int> amountNextsScratchCards = new Dictionary<int, int>();
string[] winNumber;
string[] myNumber;
int points = 0;
string card = "";
int nOfCard = 0;
int nextCard = 0;
int numberCopies = 0;

using (FileStream fileStream = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fileStream))
    {
        do
        {
            string line = fileReader.ReadLine();

            //Salvo in card il numero della carta
            card = line.Substring(0, line.IndexOf(':'));
            card = card.Remove(line.IndexOf('C'), 4).Trim();

            //Salvo in supportString tutti i numeri numeri 
            line = line.Remove(0, line.IndexOf(':')+1);
            supportString.Add(line.Split("|"));

            var item = supportString.SingleOrDefault();

            //Salvo i numeri vincenti
            winNumber = item[0].TrimStart().TrimEnd().Split(' ');
            winNumber = winNumber.Where(x => x != "").ToArray();

            //Salvo i miei numeri
            myNumber = item[1].TrimStart().TrimEnd().Split(' ');
            myNumber = myNumber.Where(x => x != "").ToArray();

            nOfCard = Convert.ToInt32(card);
            nextCard = nOfCard + 1;
            int cont = nOfCard;

            //Controllo se la carta che stiamo esaminando già esiste nel dictionary
            if (amountNextsScratchCards.ContainsKey(nOfCard))
            {
                amountNextsScratchCards.TryGetValue(nOfCard, out numberCopies);
                amountNextsScratchCards[key: nOfCard] = ++numberCopies;
            }
            else
                amountNextsScratchCards.Add(key: nOfCard, value: 1);

            foreach (var n in myNumber)
            {
                if (path == "C:\\GIT\\Advent_Code_4\\bin\\Debug\\net6.0\\adv_4_INPUT - Copia.txt" && nextCard > 6)
                    nextCard = 6;
                else if (path == "C:\\GIT\\Advent_Code_4\\bin\\Debug\\net6.0\\adv_4_INPUT.txt" && nextCard > 211)
                    nextCard = 211;

                //Controllo se i miei numeri sono tra i numeri vincenti
                if (winNumber.Contains(n))
                {
                    if (amountNextsScratchCards.ContainsKey(nextCard))
                    {
                        amountNextsScratchCards.TryGetValue(nOfCard, out numberCopies);
                        amountNextsScratchCards[key: nextCard] += numberCopies;
                    }
                    else
                    {
                        amountNextsScratchCards.TryGetValue(nOfCard, out numberCopies);
                        amountNextsScratchCards.Add(key: nextCard, value: numberCopies);
                    }
                    nextCard++;
                }
            }
            supportString.Clear();
        } while (!fileReader.EndOfStream);
    }
}

foreach (var item in amountNextsScratchCards)
{
    points += item.Value;
}

Console.WriteLine(points);

do
{
} while (true);
