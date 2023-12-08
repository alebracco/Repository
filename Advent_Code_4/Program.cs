using System.Drawing;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

string path = Path.Combine(Environment.CurrentDirectory, "adv_4_INPUT.txt");
List<string[]> supportString = new List<string[]>();
string[] winNumber;
string[] myNumber;
int sameNumber = 0;
int points = 0;

using (FileStream fileStream = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fileStream))
    {
        do
        {
            string line = fileReader.ReadLine();
            line = line.Remove(0, line.IndexOf(':')+1);
            supportString.Add(line.Split("|"));

            var item = supportString.SingleOrDefault();
            winNumber = item[0].TrimStart().TrimEnd().Split(' ');
            winNumber = winNumber.Where(x => x != "").ToArray();
            myNumber = item[1].TrimStart().TrimEnd().Split(' ');
            myNumber = myNumber.Where(x => x != "").ToArray();

            foreach (var n in myNumber)
            {
                if (winNumber.Contains(n))
                {
                    sameNumber++;
                }
            }
            points += CalculatePoint(sameNumber);
            sameNumber = 0;
            supportString.Clear();
        } while (!fileReader.EndOfStream);
    }
}

Console.WriteLine(points);

do
{
} while (true);
int CalculatePoint(int sameNumber)
{
    int points = 1;

    if (sameNumber != 0)
    {
        for (int i = 1; i < sameNumber; i++)
        {
            points *= 2;
        }
    }
    else
    {
        points = 0;
    }

    return points;
}