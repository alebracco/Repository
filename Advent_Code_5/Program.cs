//Lista dei semi da piantare

//Destination start, source start, length

//lowest location number

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

string path = Path.Combine(Environment.CurrentDirectory, "adv_5_INPUT - Copia.txt");
//path = Path.Combine(Environment.CurrentDirectory, "adv_5_INPUT.txt");

string[] values;
string[] mappatura;
Dictionary<string, List<string[]>> map = new Dictionary<string, List<string[]>>();
string header = "";
Regex num = new(@"[0-9]");


using (FileStream fileStream = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fileStream))
    {
        do
        {
            string line = fileReader.ReadLine();

            if (line != "")
            {
                mappatura = line.Split(":", StringSplitOptions.RemoveEmptyEntries);

                if (mappatura.Count() == 2)
                {
                    header = mappatura[0];
                    values = mappatura[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    map.Add(header, new List<string[]> { values });
                }
                else
                {
                    if (num.IsMatch(mappatura[0].Trim()))
                    {
                        values = mappatura[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        if (map.ContainsKey(header))
                        {
                            map[header].Add(values);
                        }
                        else
                        {
                            map.Add(header, new List<string[]> { values });
                        }
                    }
                    else
                    {
                        header = mappatura[0];
                    }
                }
            }
        } while (!fileReader.EndOfStream);
    }
}

List<Int64> location = new List<Int64>();

bool first = true;
Int64 initialSeed = 0;
string seedLength;
Dictionary<string, Int64> seedAndLength = new Dictionary<string, Int64>();

foreach (var s in map["seeds"].First())
{
    if (first)
    {
        initialSeed = Convert.ToInt64(s);
        first = false;
    }
    else
    {
        seedLength = s;
        first = true;
        seedAndLength.Add(seedLength, initialSeed);
    }
}

var headers = map.Keys.ToList().Skip(1);

List<Int64> lSeed = new List<Int64>();
List<Int64> lSeedTOFinal = new List<Int64>();
List<Int64> lSeedNeedModify = new List<Int64>();
List<Int64> lSeedTOModify = new List<Int64>();

foreach (var s in seedAndLength)
{
    Int64 seedToOther = s.Value;
    Int64 len = Convert.ToInt64(s.Key);
    for (int i = 0; i < len; i++)
    {
        lSeed.Add(seedToOther);
        seedToOther++;
    }

    foreach (string h in headers)
    {
        lSeedTOModify.Clear();
        foreach (var istruzioni in map[h].ToList())
        {
            lSeedNeedModify.Clear();
            lSeedNeedModify = lSeed.Where(seed =>  Convert.ToInt64(istruzioni[1]) <= seed && Convert.ToInt64(istruzioni[1]) + Convert.ToInt64(istruzioni[2]) >= seed).ToList();
            lSeed.RemoveAll(seed => lSeedNeedModify.Contains(seed));
            lSeedTOModify.AddRange(lSeedNeedModify.Select(seed => seed + (Convert.ToInt64(istruzioni[0]) - Convert.ToInt64(istruzioni[1]))).ToList());
        }
        lSeed.AddRange(lSeedTOModify);
    }
    lSeedTOFinal.AddRange(lSeed);
}
location.AddRange(lSeedTOFinal);

//foreach (var s in lSeed)
//{
//    Int64 seed = s;
//    foreach (string h in headers)
//    {
//        foreach (var istruzioni in map[h].ToList())
//        {
//            seed = ElaboraValori(Convert.ToInt64(istruzioni[0]), Convert.ToInt64(istruzioni[1]), Convert.ToInt64(istruzioni[2]), seed, out bool changed);

//            lSeed = lSeed.Where(x => x > Convert.ToInt64(istruzioni[1]) || Convert.ToInt64(istruzioni[0]) + Convert.ToInt64(istruzioni[2]) < x).Select(x => x + (Convert.ToInt64(istruzioni[1]) - Convert.ToInt64(istruzioni[0]))).ToList();
//            if (changed)
//                break;
//        }
//    }
//    location.Add(seed);
//}

foreach (var l in location)
{
    Console.WriteLine($"Location: {l}");
}

Console.WriteLine();

Console.WriteLine($"The lowest: {location.Min()}");

Int64 ElaboraValori(Int64 startDestination, Int64 startSource, Int64 length, Int64 seed, out bool changed)
{
    changed = false;
    Int64 res = 0;

    if (startSource > seed || startSource + length < seed)
    {
        return seed;
    }
    else
    {
        changed = true;
        res = seed - startSource;
        return res + startDestination;
    }
}

do
{

} while (true);