// Day 11
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

List<string> Expand(List<string> input)
{
    List<string> output = new List<string>();

    string emptyRow = "";
    for(int z = 0; z < input[0].Length; z++)
    {
        emptyRow += ".";
    }

    int emptyCount = 0;
    foreach (string row in input)
    {
        if (row.Contains("#"))
        {
            if (emptyCount > 0)
            {
                for (int i = 0; i < emptyCount * 2; i++)
                {
                    
                    output.Add(emptyRow);
                }
            }

            output.Add(row);
            emptyCount = 0;
        }
        else
        {
            emptyCount++;
        }
    }
    return output;
}

List<string> expandedRows = Expand(lines.ToList());
List<string> columns = new List<string>();
for (int i = 0; i < expandedRows[0].Length; i++)
{
    string column = "";
    foreach (string row in expandedRows)
    {
        column += row[i];
    }
    columns.Add(column);
}

List<string> expandedColumns = Expand(columns);
List<string> expanded = new List<string>();
for (int i = 0; i < expandedColumns[0].Length; i++)
{
    string row = "";
    foreach (string column in expandedColumns)
    {
        row += column[i];
    }
    expanded.Add(row);
}

List<Tuple<int,int>> galaxies = new List<Tuple<int, int>>();
for (int r = 0; r < expanded.Count; r++)
{
    for (int c = 0; c < expanded[r].Length; c++)
    {
        if (expanded[r][c] == '#')
        {
            galaxies.Add(new Tuple<int, int>(r,c));
        }
    }
}

int sum = 0;
for (int r = 0; r < galaxies.Count; r++)
{
    for (int c = r; c < galaxies.Count; c++)
    {
        if (galaxies[r] == galaxies[c])
        {
            continue;
        }
        int dist = Math.Abs(galaxies[c].Item1 - galaxies[r].Item1) + Math.Abs(galaxies[c].Item2 - galaxies[r].Item2);
        //Console.WriteLine(galaxies[r] + " to " + galaxies[c] + " " + dist);
        sum += dist;
    }
}
Console.WriteLine(sum);