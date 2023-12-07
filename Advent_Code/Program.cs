//Prendere riga per riga
//Estrarre tutte le digit
//Tenere la prima e l'ultima della riga
//Unirle per formare un numero a due cifre
//Sommare tutti i numeri tra di loro

using System.Text.RegularExpressions;
string pathFile = Path.Combine(Environment.CurrentDirectory, "adv_1_INPUT.txt");

int getNumeri(string pathFile)
{

    //Path del file di input
    List<int> lRow = new List<int>();
    //Regola REGEX per escludere i caratteri
    Regex avoidChar = new(@"[a-z]");
    Dictionary<int, string> numberLettere = new Dictionary<int, string>()
    {
        { 1, "one"},
        { 2, "two"},
        { 3, "three" },
        { 4, "four"},
        { 5, "five" },
        { 6, "six" },
        { 7, "seven" },
        { 8, "eight" },
        { 9, "nine" },
    };


    using (FileStream str = File.OpenRead(pathFile))
    {
        string row;
        using (StreamReader sr = new StreamReader(str))
        {
            do
            {
                string generateRow = "";
                string number = "";

                //Prendo ogni riga del file
                row = sr.ReadLine();
                foreach (var g in row)
                {
                    generateRow += g;
                    //Se il carattere è un numero prendilo e skippa
                    if (!avoidChar.IsMatch(g.ToString()))
                    {
                        number += g;
                        break;
                    }
                    else
                    {
                        foreach (var nl in numberLettere)
                        {
                            if (generateRow.Contains(nl.Value))
                            {
                                number += nl.Key;
                                break;
                            }
                        }
                        if (number.Length == 1)
                        {
                            break;
                        }
                    }
                }

                generateRow = "";
                foreach (var g in row.Reverse())
                {
                    generateRow = string.Concat(g, generateRow);
                    //Se il carattere è un numero prendilo e skippa
                    if (!avoidChar.IsMatch(g.ToString()))
                    {
                        number += g;
                        break;
                    }
                    else
                    {
                        foreach (var nl in numberLettere)
                        {
                            if (generateRow.Contains(nl.Value))
                            {
                                number += nl.Key;
                                break;
                            }
                        }
                        if (number.Length == 2)
                        {
                            break;
                        }
                    }
                }

                //Prendo solo la prima e l'ultima cifra
                lRow.Add(Convert.ToInt32(number));

            } while (!sr.EndOfStream);
        }
    }

    int sum = 0;
    foreach (var r in lRow)
    {
        sum += r;
    }

    return sum;
}

Console.WriteLine(getNumeri(pathFile));