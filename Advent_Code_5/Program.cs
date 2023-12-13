//Lista dei semi da piantare

//Destination start, source start, length

//lowest location number

using System.Collections.Generic;
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

List<UInt32> location = new List<UInt32>();

bool first = true;
UInt32 initialSeed = 0;
string seedLength;
Dictionary<string, UInt32> seedAndLength = new Dictionary<string, UInt32>();

foreach (var s in map["seeds"].First())
{
    if (first)
    {
        initialSeed = Convert.ToUInt32(s);
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
    UInt32 seed = s.Value;
    UInt32 seedToOther = s.Value;
    UInt32 len = Convert.ToUInt32(s.Key);
    for (int i = 0; i < len; i++)
    {
        seed = seedToOther;
        foreach (string h in headers)
        {
            foreach (var m in map[h].ToList())
            {
                var istruzioni = m;
                UInt32 startDestination = Convert.ToUInt32(istruzioni[0]);
                UInt32 startSource = Convert.ToUInt32(istruzioni[1]);
                UInt32 length = Convert.ToUInt32(istruzioni[2]);
                seed = ElaboraValori(startDestination, startSource, length, seed, out bool changed);
                if (changed)
                    break;
            }

            if (h == map.Keys.Last())
            {
                location.Add(seed);
            }
            Console.WriteLine($"{h} - {seed}");
        }
        seedToOther++;
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine("FINE SEME DI PARTENZA");
    Console.WriteLine();
}

Console.WriteLine();

foreach (var l in location)
{
    Console.WriteLine($"Location: {l}");
}

Console.WriteLine();

Console.WriteLine($"The lowest: {location.Min()}");

UInt32 ElaboraValori(UInt32 startDestination, UInt32 startSource, UInt32 length, UInt32 seed, out bool changed)
{
    changed = false;
    for (UInt32 i = 0; i < length; i++)
    {
        if (startSource > seed || startSource + length < seed)
        {
            return seed;
        }

        if (seed == startSource)
        {
            changed = true;
            return startDestination;
        }
        startDestination++;
        startSource++;
        
    }
    return seed;
}

do
{

} while (true);