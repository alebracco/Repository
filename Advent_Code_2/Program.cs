using System;
using System.Diagnostics.Contracts;

string path = Path.Combine(Environment.CurrentDirectory, "adv_2_INPUT.txt");

Dictionary<int, string[]> keyValuePairs = new Dictionary<int, string[]>();

using (FileStream fileStream = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fileStream))
    {
        do
        {
            string line = fileReader.ReadLine();

            line = line.Replace("Game ", "");
            string game = line.Substring(0, line.IndexOf(":"));
            line = line.Remove(0, line.IndexOf(":") + 1);

            string[] throwDadi = line.Split(";");

            keyValuePairs.Add(Convert.ToInt16(game), throwDadi);

        } while (!fileReader.EndOfStream);
    }
}

List<string[]> strings = new List<string[]>();
List<string[]> win = new List<string[]>();

foreach (var kvp in keyValuePairs)
{
    string[] coloreValore;
    int blue = 0;
    int red = 0;
    int green = 0;
    int mininumCubes = 0;
    bool bWin = true;
    strings.Clear();
    foreach (var item in kvp.Value.ToList())
    {
        strings.Add(item.Split(","));
    }
    foreach (var item in strings)
    {
        foreach (var i in item)
        {
            coloreValore = i.TrimStart().Split(" ");

            if (coloreValore.Contains("red"))
            {
                if (red < Convert.ToInt16(coloreValore[0]))
                {
                    red = Convert.ToInt16(coloreValore[0]);
                }
                if (Convert.ToInt16(coloreValore[0]) > 12)
                {
                    bWin = false;
                }
            }
            else if (coloreValore.Contains("blue"))
            {
                if (blue < Convert.ToInt16(coloreValore[0]))
                {
                    blue = Convert.ToInt16(coloreValore[0]);
                }
                if (Convert.ToInt16(coloreValore[0]) > 14)
                {
                    bWin = false;
                }

            }
            else if (coloreValore.Contains("green"))
            {
                if (green < Convert.ToInt16(coloreValore[0]))
                {
                    green = Convert.ToInt16(coloreValore[0]);
                }
                if (Convert.ToInt16(coloreValore[0]) > 13)
                {
                    bWin = false;
                }

            }
        }
    }

    mininumCubes = red * green * blue;

    if (bWin)
    {
        win.Add(new string[] { mininumCubes.ToString(), "Win" });
    }
    else
    {
        win.Add(new string[] { mininumCubes.ToString(), "Lose" });
    }


}

foreach (var item in win)
{
    Console.WriteLine(item[1] + " - " + item[0]);
}

int sum = 0;
foreach (var item in win)
{

    sum += Convert.ToInt16(item[0]);

}

Console.WriteLine(sum);