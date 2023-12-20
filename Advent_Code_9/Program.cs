
string path = Path.Combine(Environment.CurrentDirectory, "adv_9_INPUT - Copia.txt");
path = Path.Combine(Environment.CurrentDirectory, "adv_9_INPUT.txt");

List<string> lLine = new List<string>();

using (FileStream fl = File.OpenRead(path))
{
    int cont = 0;
    using (StreamReader fileReader = new StreamReader(fl))
    {
        do
        {
            lLine.Add(fileReader.ReadLine());

        } while (!fileReader.EndOfStream);
    }
}

List<int[]> map = new List<int[]>();
int? totalSum = 0;
int? leftover = 0;

foreach (var line in lLine)
{

    int[] _char = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    map.Add(_char);
    List<int> supportChar = new List<int>();
    do
    {
        supportChar.Clear();
        _char = map.Last();
        int index = 0;

        do
        {
            supportChar.Add(_char[index + 1] - _char[index]);
        } while (index++ != _char.Length - 2);

        map.Add(supportChar.ToArray());
    } while (supportChar.Distinct().First() != 0 || supportChar.Distinct().Count() != 1);

    int reverse = 0;
    int? firstValue = 0;
    int? secondValue = null;
    int? totalValue = 0;

    do
    {
        if (secondValue is null)
        {
            secondValue  = map.ElementAt(map.Count() - ++reverse).First();
        }
        firstValue = map.ElementAt(map.Count() - ++reverse).First();

        totalValue = firstValue - secondValue;
        secondValue = totalValue;

    } while (map.Count - leftover != reverse);
    leftover += reverse;
    totalSum += totalValue;
}

Console.WriteLine(totalSum);

