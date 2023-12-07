using System.Text.RegularExpressions;

string path = Path.Combine(Environment.CurrentDirectory, "adv_3_INPUT.txt");

int sizeRow = 140;
int sizeColumn = 140;
//int sizeRow = 10; 
//int sizeColumn = 10;
char[,] schema = new char[sizeRow, sizeColumn];
Regex num = new(@"[0-9]");
Regex gearChar = new(@"[*]");


using (FileStream fileStream = File.OpenRead(path))
{
    using (StreamReader fileReader = new StreamReader(fileStream))
    {
        int row = 0;
        do
        {
            string line = fileReader.ReadLine();
            int column = 0;
            foreach (var i in line)
            {
                schema[row, column] = i;
                column++;
            }
            row++;
        } while (!fileReader.EndOfStream);
    }
}

int contRow = 0;
int contColumn = 0;
string number = "";
bool prevIsNum = false;
int sum = 0;
bool added = false;
bool ripristinaValori = false;
Dictionary<string, string> numberCoordinatePair = new Dictionary<string, string>();

foreach (var c in schema)
{
    //Entra solo se il carattere in questione non è un numero
    if (!num.IsMatch(c.ToString()))
    {
        //Qui dovrà controllare se intorno ha un carattere speciale
        if (prevIsNum)
        {
            //Correzione contatore colonne righe se il numero precedente era all'ultima colonna
            if (contColumn == 0)
            {
                contRow--;
                contColumn = 140;
                ripristinaValori = true;
            }

            //Preparazione righe e colonne da controllare standard
            int startRow = -1;
            int startColumn = -1;
            int endRow = 2;
            int endColumn = number.Length + 1;

            //Modifiche per righe e colonne quando il valore è nella prima o l'ultima
            if (contColumn - number.Length == 0)
                startColumn = 0;
            if (contColumn == sizeColumn - 1 || contColumn == sizeColumn)
                endColumn = number.Length - 1;
            if (contRow == 0)
                startRow = 0;
            if (contRow == sizeRow - 1)
                endRow = 1;
            //Verifico se intorno al numero c'è un carattere speciale partendo dal carattere in alto a sinistra intorno al numero e finendo con quello in basso a destra
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = startColumn; j < endColumn; j++)
                {
                    //E' un carattere speciale?? Allo il valore va contato e skippa gli altri controlli.
                    if (gearChar.IsMatch(schema[contRow + i, contColumn - (number.Length - j)].ToString()))
                    {
                        string coordinate = $"{contRow + i},{contColumn - (number.Length - j)}";
                        if (numberCoordinatePair.ContainsKey($"{contRow + i},{contColumn - (number.Length - j)}"))
                        {
                            string supportNumber = "";
                            numberCoordinatePair.TryGetValue(coordinate, out supportNumber);
                            sum += Convert.ToInt32(number) * Convert.ToInt32(supportNumber);
                            numberCoordinatePair.Remove(coordinate);
                            Console.WriteLine("");
                            foreach (var item in numberCoordinatePair)
                            {
                                Console.WriteLine(item.Value + ", " + item.Key);
                            }
                            Console.WriteLine("");

                        }
                        else
                            numberCoordinatePair.Add(key: coordinate, value: number);

                        added = true;
                        break;
                    }
                }
                if (added)
                {
                    added = false;
                    break;
                }
            }
        }
        //Ripristino i valori originali di righe e colonne (Per controllare il contatore righe e colonne)
        if (ripristinaValori)
        {
            contRow++;
            contColumn = 0;
            ripristinaValori = false;
        }
        number = "";
        prevIsNum = false;
    }

    //Controlla se il carattere attuale è un numero e lo aggiunge a quelli precedenti
    if (num.IsMatch(c.ToString()))
    {
        prevIsNum = true;
        number += c;
    }

    //Aggiorno contatore righe e colonne
    contColumn++;
    if (contColumn == sizeColumn)
    {
        contColumn = 0;
        contRow++;
    }
}

Console.WriteLine(sum);