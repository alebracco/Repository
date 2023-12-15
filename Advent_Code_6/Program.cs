
string path = Path.Combine(Environment.CurrentDirectory, "adv_6_INPUT - Copia.txt");
path = Path.Combine(Environment.CurrentDirectory, "adv_6_INPUT.txt");

string[] time = new string[50];
string[] distance = new string[50];

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
            }
            else
            {
                distance = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

        } while (!fileReader.EndOfStream);
    }
}

Dictionary<string, int> TimeDistance = new Dictionary<string, int>();
List<int> lWin = new List<int>();

for (int i = 1; i < time.Length; i++)
{
    TimeDistance.Add(time[i], Convert.ToInt16(distance[i]));
    foreach (var td in TimeDistance)
    {
        int cont = 0;
        for (int j = 0; j < td.Value; j++)
        {
            int speedPerSecond = j;
            int remainTime = Convert.ToInt16(td.Key) - j;
            int distantReached = remainTime * speedPerSecond;
            if (distantReached > td.Value)
            {
                cont++;
            }
        }
        lWin.Add(cont);
    }
    TimeDistance.Remove(time[i]);

}

int total = 1;
foreach (var w in lWin)
{
    total *= w;
}

Console.WriteLine(total);
    
