// Day 11
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

List<Tuple<int,int>> galaxies = new List<Tuple<int, int>>();
for (int r = 0; r < lines.Length; r++)
{
    for (int c = 0; c < lines[r].Length; c++)
    {
        if (lines[r][c] == '#')
        {
            galaxies.Add(new Tuple<int, int>(r,c));
        }
    }
}

long sum = 0;
for (int f = 0; f < galaxies.Count; f++)
{
    for (int t = f; t < galaxies.Count; t++)
    {
        if (galaxies[f] == galaxies[t])
        {
            continue;
        }

        int dirR = galaxies[t].Item1 - galaxies[f].Item1 > 0 ? 1 : -1;
        int dirC = galaxies[t].Item2 - galaxies[f].Item2 > 0 ? 1 : -1;
        long dist = 0;
        for (int r = galaxies[f].Item1; r != galaxies[t].Item1; r += dirR)
        {
            if (r == galaxies[f].Item1)
            {
                continue;
            }

            if (!lines[r].Contains("#"))
            {
                dist += 1000000;
                //Console.WriteLine("R Empty " + dist);
            }
            else
            {
                dist++;
                //Console.WriteLine("R dist " + dist);
            }
        }

        for (int c = galaxies[f].Item2; c != galaxies[t].Item2; c += dirC)
        {
            if (c == galaxies[f].Item2)
            {
                continue;
            }

            string column = "";
            foreach (string row in lines)
            {
                column += row[c];
            }
            if (!column.Contains("#"))
            {
                dist += 1000000;
                //Console.WriteLine("C Empty " + dist);
            }
            else
            {
                dist++;
                //Console.WriteLine("C dist " + dist);
            }
        }
        dist += 1;
        if (galaxies[t].Item1 - galaxies[f].Item1 != 0 && galaxies[t].Item2 - galaxies[f].Item2 != 0)
        {
            dist += 1;
        }

        //Console.WriteLine(galaxies[f] + " to " + galaxies[t] + " " + dist);
        sum += dist;
    }
}
Console.WriteLine(sum);