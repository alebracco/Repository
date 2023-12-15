
using System.Numerics;

string path = Path.Combine(Environment.CurrentDirectory, "adv_6_INPUT - Copia.txt");
path = Path.Combine(Environment.CurrentDirectory, "adv_6_INPUT.txt");

string[] time = new string[50];
string bigRaceTime = "";
string[] distance = new string[50];
string bigRacedistance = "";


using (FileStream fileStream = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fileStream))
    {
        do
        {
            string line = fileReader.ReadLine();

            if (line.Contains("Time"))
            {
                time = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var t in time.Skip(1))
                {
                    bigRaceTime += t;
                }
            }
            else
            {
                distance = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var d in distance.Skip(1))
                {
                    bigRacedistance += d;
                }
            }

        } while (!fileReader.EndOfStream);
    }
}

List<int> lWin = new List<int>();


Int64 cont = 0;
for (Int64 j = 0; j < Convert.ToInt64(bigRaceTime); j++)
{
    Int64 speedPerSecond = j;
    Int64 remainTime = Convert.ToInt64(bigRaceTime) - j;
    Int64 distantReached = remainTime * speedPerSecond;
    if (distantReached > Convert.ToInt64(bigRacedistance))
    {
        cont++;
    }
}

Console.WriteLine(cont);
    
