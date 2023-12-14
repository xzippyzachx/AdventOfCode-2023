// Day 14
// Part 2
string[] lines = File.ReadAllLines("Part2/input.txt");

List<string> RotateRight(List<string> lines)
{
    List<string> columns = new List<string>();
    for (int i = 0; i < lines[0].Length; i++)
    {
        string column = "";
        for (int j = lines.Count - 1; j >= 0; j--)
        {
            column += lines[j][i];
        }
        columns.Add(column);
        //Console.WriteLine(column);
    }
    return columns;
}

// List<string> RotateLeft(List<string> lines)
// {
//     List<string> columns = new List<string>();
//     for (int i = lines[0].Length - 1; i >= 0; i--)
//     {
//         string column = "";
//         for (int j = 0; j < lines.Count; j++)
//         {
//             column += lines[j][i];
//         }
//         columns.Add(column);
//         //Console.WriteLine(column);
//     }
//     return columns;
// }

Dictionary<string, int> map = new Dictionary<string, int>();
List<string> columns = lines.ToList();
bool skipDone = false;
for (int cy = 0; cy < 1000000000; cy++)
{
    for (int r = 0; r < 4; r++)
    {
        columns = RotateRight(columns);
        for (int c = 0; c < columns.Count; c++)
        {
            string column = columns[c];
            char[] newColumn = new char[column.Length];
            int dotCount = 0;
            for (int i = column.Length - 1; i >= 0; i--)
            {
                newColumn[i] = column[i];
                if (column[i] == 'O' && dotCount > 0)
                {
                    //Console.WriteLine(column.Length - (i - dotCount));
                    newColumn[i + dotCount] = 'O';
                    newColumn[i] = '.';
                }
                if (column[i] == '#')
                {
                    dotCount = 0;
                }
                if (column[i] == '.')
                {
                    dotCount++;
                }
            }
            columns[c] = new string(newColumn);
            //Console.WriteLine("-------------");
        }
    }
    
    string key = "";
    foreach (string s in columns)
    {
        key += s;
    }

    if (skipDone)
    {
        continue;
    }

    if (map.ContainsKey(key))
    {
        int cycleStart = map[key];
        int cycleLength = cy - cycleStart;
        //Console.WriteLine(cycleLength);
        cy = 1000000000 - ((1000000000 - cycleStart) % cycleLength);
        skipDone = true;
    }
    else
    {
        map.Add(key, cy);
    }
}

//columns = RotateLeft(columns);

int sum = 0;
for (int r = 0; r < columns.Count; r++)
{
    foreach (char c in columns[r])
    {
        if (c == 'O')
        {
            sum += columns.Count - r;
        }
    }
    //Console.WriteLine(columns[r]);
}
Console.WriteLine(sum);