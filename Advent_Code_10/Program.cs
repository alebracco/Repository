/* | --> row + 1 / row - 1
 * - --> column + 1 / column - 1
 * L --> row + 1 column + 1 / row - 1 column - 1
 * J --> row + 1 column - 1 / row - 1 column + 1
 * 7 --> row - 1 column - 1 / row + 1 column + 1
 * F --> row - 1 column + 1 / row + 1 column - 1
 * . --> Stop
 * S --> Starting Point
 */

string path = Path.Combine(Environment.CurrentDirectory, "adv_10_INPUT - Copia.txt");
//path = Path.Combine(Environment.CurrentDirectory, "adv_10_INPUT.txt");

List<string> lLine = new List<string>();
string[,] map;
int? maxRow = null;
int? maxColumn = null;
Coordinata startingPoint = new(0, 0);

using (FileStream fl = File.OpenRead(path))
{
    int cont = 0;
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

int actualRow = 0;
int actualColumn = 0;

map = new string[maxRow.Value, maxColumn.Value];

foreach (var line in lLine)
{
    Console.Write($"[{actualRow}]");

    foreach (var c in line)
    {
        if (c == 'S')
        {
            startingPoint = new(actualRow, actualColumn);
        }

        Console.Write(c);

        map[actualRow, actualColumn++] = c.ToString();
    }

    Console.WriteLine();
    actualRow++;
    actualColumn = 0;
}


int x = startingPoint.x;
int y = startingPoint.y;
int _try = 0;

Coordinata newDirection = NextPipeFromStartingPoint(x, y, map);

do
{
    newDirection = CoordPrevToSucc(newDirection.x, newDirection.y, map, newDirection, _try);
    _try++;

} while (map[newDirection.x, newDirection.y] != "S");

//Totale dei pipes diviso 2
Console.WriteLine((_try + 1) / 2);



//Classi e metodi
//Classi e metodi
//Classi e metodi


Coordinata NextPipeFromStartingPoint(int x, int y, string[,] map)
{
    string pipe = "";
    Coordinata coord = new(0,0);
    if (x > 0 && x < maxRow && y > 0 && y < maxColumn)
    {
        pipe = map[x - 1, y];
        if (pipe == "7" || pipe == "F" || pipe == "|")
        {
            coord = new(x - 1, y, Coordinata.Direzione.up);
            return coord;
        }
        pipe = map[x + 1, y];
        if (pipe == "L" || pipe == "J" || pipe == "|")
        {
            coord = new(x + 1, y, Coordinata.Direzione.down);
            return coord;
        }
        pipe = map[x, y + 1];
        if (pipe == "-" || pipe == "J" || pipe == "7")
        {
            coord = new(x, y + 1, Coordinata.Direzione.right);
            return coord;
        }
        pipe = map[x, y - 1];
        if (pipe == "-" || pipe == "L" || pipe == "F")
        {
            coord = new(x, y - 1, Coordinata.Direzione.left);
            return coord;
        }
    }
    else if (x == 0)
    {
        pipe = map[x + 1, y];
        if (pipe == "L" || pipe == "J" || pipe == "|")
        {
            coord = new(x + 1, y, Coordinata.Direzione.down);
            return coord;
        }
        pipe = map[x, y + 1];
        if (pipe == "-" || pipe == "J" || pipe == "7")
        {
            coord = new(x, y + 1, Coordinata.Direzione.right);
            return coord;
        }
        pipe = map[x, y - 1];
        if (pipe == "-" || pipe == "L" || pipe == "F")
        {
            coord = new(x, y - 1, Coordinata.Direzione.left);
            return coord;
        }
    }
    else if (x == maxRow)
    {
        pipe = map[x - 1, y];
        if (pipe == "7" || pipe == "F" || pipe == "|")
        {
            coord = new(x - 1, y, Coordinata.Direzione.up);
            return coord;
        }
        pipe = map[x, y + 1];
        if (pipe == "-" || pipe == "J" || pipe == "7")
        {
            coord = new(x, y + 1, Coordinata.Direzione.right);
            return coord;
        }
        pipe = map[x, y - 1];
        if (pipe == "-" || pipe == "L" || pipe == "F")
        {
            coord = new(x, y - 1, Coordinata.Direzione.left);
            return coord;
        }
    }
    else if (y == 0)
    {
        pipe = map[x - 1, y];
        if (pipe == "7" || pipe == "F" || pipe == "|")
        {
            coord = new(x - 1, y, Coordinata.Direzione.up);
            return coord;
        }
        pipe = map[x + 1, y];
        if (pipe == "L" || pipe == "J" || pipe == "|")
        {
            coord = new(x + 1, y, Coordinata.Direzione.down);
            return coord;
        }
        pipe = map[x, y + 1];
        if (pipe == "-" || pipe == "J" || pipe == "7")
        {
            coord = new(x, y + 1, Coordinata.Direzione.right);
            return coord;
        }
    }

    else if (y == maxColumn)
    {
        pipe = map[x - 1, y];
        if (pipe == "7" || pipe == "F" || pipe == "|")
        {
            coord = new(x - 1, y, Coordinata.Direzione.up);
            return coord;
        }
        pipe = map[x + 1, y];
        if (pipe == "L" || pipe == "J" || pipe == "|")
        {
            coord = new(x + 1, y, Coordinata.Direzione.down);
            return coord;
        }

        pipe = map[x, y - 1];
        if (pipe == "-" || pipe == "L" || pipe == "F")
        {
            coord = new(x, y - 1, Coordinata.Direzione.left);
            return coord;
        }
    }

    return coord;
}

Coordinata CoordPrevToSucc(int x, int y, string[,] map, Coordinata newDirection, int _try)
{
    bool itsOk = false;
    Coordinata coord = new(0, 0);
    if (map[x, y] == "S" && _try != 0)
    {

    }
    if (newDirection.direzione == Coordinata.Direzione.up)
    {
        if (map[x, y] == "7")
        {
            y--;
            coord.direzione = Coordinata.Direzione.left;
        }
        else if (map[x, y] == "F")
        {
            y++;
            coord.direzione = Coordinata.Direzione.right;
        }
        else if (map[x, y] == "|")
        {
            x--;
            coord.direzione = Coordinata.Direzione.up;
        }
    }
    else if (newDirection.direzione == Coordinata.Direzione.down)
    {
        if (map[x, y] == "L")
        {
            y++;
            coord.direzione = Coordinata.Direzione.right;
        }
        else if (map[x, y] == "J")
        {
            y--;
            coord.direzione = Coordinata.Direzione.left;
        }
        else if (map[x, y] == "|")
        {
            x++;
            coord.direzione = Coordinata.Direzione.down;
        }
    }
    else if (newDirection.direzione == Coordinata.Direzione.right)
    {
        if (map[x, y] == "7")
        {
            x++;
            coord.direzione = Coordinata.Direzione.down;
        }
        else if (map[x, y] == "J")
        {
            x--;
            coord.direzione = Coordinata.Direzione.up;
        }
        else if (map[x, y] == "-")
        {
            y++;
            coord.direzione = Coordinata.Direzione.right;
        }
    }
    else if (newDirection.direzione == Coordinata.Direzione.left)
    {
        if (map[x, y] == "L")
        {
            x--;
            coord.direzione = Coordinata.Direzione.up;
        }
        else if (map[x, y] == "F")
        {
            x++;
            coord.direzione = Coordinata.Direzione.down;
        }
        else if (map[x, y] == "-")
        {
            y--;
            coord.direzione = Coordinata.Direzione.left;
        }
    }

    coord.y = y;
    coord.x = x;

    return coord;
}

public class Coordinata
{
    public Coordinata(int x, int y, Direzione direzione = Direzione.None)
    {
        this.x = x;
        this.y = y;
        this.direzione = direzione;
    }
    public int x { get; set; }
    public int y { get; set; }
    public Direzione direzione { get; set; }

    public enum Direzione
    {
        None = 0,
        up,
        down,
        right,
        left,
    }
}