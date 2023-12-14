//Lista dei semi da piantare

//Destination start, source start, length

//lowest location number

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

string path = Path.Combine(Environment.CurrentDirectory, "adv_5_INPUT - Copia.txt");
path = Path.Combine(Environment.CurrentDirectory, "adv_5_INPUT.txt");

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

foreach (var s in seedAndLength)
{
    Int64 seed = s.Value;
    Int64 seedToOther = s.Value;
    Int64 len = Convert.ToInt64(s.Key);
    for (int i = 0; i < len; i++)
    {
        seed = seedToOther;
        foreach (string h in headers)
        {
            foreach (var m in map[h].ToList())
            {
                var istruzioni = m;
                Int64 startDestination = Convert.ToInt64(istruzioni[0]);
                Int64 startSource = Convert.ToInt64(istruzioni[1]);
                Int64 length = Convert.ToInt64(istruzioni[2]);
                seed = ElaboraValori(startDestination, startSource, length, seed, out bool changed);
                if (changed)
                    break;
            }
        }
        location.Add(seed);
        seedToOther++;
    }
    Console.WriteLine("Nextseed");
}

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