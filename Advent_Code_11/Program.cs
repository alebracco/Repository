
string path = Path.Combine(Environment.CurrentDirectory, "adv_11_INPUT - Copia.txt");
//path = Path.Combine(Environment.CurrentDirectory, "adv_11_INPUT.txt");

List<string> lLine = new List<string>();
string[,] map;
int? maxRow = null;
int? maxColumn = null;

using (FileStream fl = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fl))
    {

        do
        {
            lLine.Add(fileReader.ReadLine());
            if (maxRow is null)
            {
                maxColumn = lLine.First().Length;
            }

        } while (!fileReader.EndOfStream);
        maxRow = lLine.Count();
    }
}

int cont = 0;
List<int> idColumn = new List<int>();
List<int> idRow = new List<int>();


foreach (var line in lLine)
{
    if (line.Distinct().FirstOrDefault() == '.' && line.Distinct().Count() == 1)
    {
        idColumn.Add(cont);
    }

    cont++;
}
cont = 0;

for (int i = 0; i < maxRow; i++)
{
    string supportRow = "";
    foreach (var item in lLine)
    {
        supportRow += item.Remove(0,i).FirstOrDefault();
    }

    if (supportRow.Distinct().FirstOrDefault() == '.' && supportRow.Distinct().Count() == 1)
    {
        idRow.Add(cont);
    }
    cont++;

}

Console.WriteLine();