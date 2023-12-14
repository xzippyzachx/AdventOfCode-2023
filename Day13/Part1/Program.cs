// Day 13
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

List<List<string>> patterns = new List<List<string>>();
List<string> patternLinesTemp = new List<string>();
int index = 0;
foreach (string line in lines)
{
    if (line == "" || index == lines.Length - 1)
    {
        patterns.Add(patternLinesTemp);
        patternLinesTemp = new List<string>();
        index++;
        continue;
    }
    patternLinesTemp.Add(line);
    index++;
}

int FindRelection(List<string> patternLines)
{
    int sum = 0;
    string last = "";
    for (int r = 0; r < patternLines.Count; r++)
    {
        if (patternLines[r] == last)
        {
            //Console.WriteLine("Test: " + r);

            bool notReflection = false;
            int dist = 1;
            for (int i = r - 1; i >= 0; i--)
            {
                //Console.WriteLine(i + " - " + (r - 1 + dist));
                if (r - 1 + dist >= patternLines.Count)
                {
                    break;
                }
                if (patternLines[i] != patternLines[r - 1 + dist])
                {
                    notReflection = true;
                    break;
                }
                dist += 1;
            }
            if (notReflection)
            {
                last = patternLines[r];
                continue;
            }

            // Reflection found
            sum += r;
        }
        last = patternLines[r];
    }
    return sum;
}

int sum = 0;
foreach (List<string> patternLines in patterns)
{
    // Rows
    sum += FindRelection(patternLines) * 100;

    List<string> columns = new List<string>();
    for (int i = 0; i < patternLines[0].Length; i++)
    {
        string column = "";
        foreach (string row in patternLines)
        {
            column += row[i];
        }
        columns.Add(column);
    }

    // Columns
    sum += FindRelection(columns);
}
Console.WriteLine(sum);