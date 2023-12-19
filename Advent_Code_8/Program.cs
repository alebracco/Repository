using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;

string path = Path.Combine(Environment.CurrentDirectory, "adv_8_INPUT - Copia.txt");
path = Path.Combine(Environment.CurrentDirectory, "adv_8_INPUT.txt");

string instructions = "";
Dictionary<string, string[]> map = new Dictionary<string, string[]>();
Regex specialChar = new Regex(@"[=(,)]");


bool header = false;

using (FileStream fl = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fl))
    {
        do
        {
            string? line = fileReader.ReadLine();

            if (!header)
            {
                instructions += line;
                if (line == "")
                {
                    header = true;
                }
            }
            else
            {
                line = specialChar.Replace(line, " ");
                string[] splitted = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                map.Add(key: splitted[0], value: new string[]{ splitted[1], splitted[2]});

            }

        } while (!fileReader.EndOfStream);
    }
}

bool stop = false;
UInt64 cont = 0;
string reaching = map.FirstOrDefault().Key;
//reaching = "ZZZ";
//do
//{

//    cont++;
//    reaching = map.Where(x => x.Value.Contains(reaching)).FirstOrDefault().Key;

//    if (reaching == "RTF")
//    {
//        Console.WriteLine(cont);
//        stop = true;
//        break;
//    }

//} while (!stop);

do
{
    foreach (var ins in instructions)
    {
        cont++;

        reaching = map[reaching][ins == 'L' ? 0 : 1];

        if (map[reaching].Contains("ZZZ"))
        {
            Console.WriteLine(reaching);
        }

        if (reaching == "ZZZ")
        {
            Console.WriteLine(cont);
            stop = true;
            break;
        }
    }
} while (!stop);

Console.WriteLine(cont);