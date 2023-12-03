// Day 3
// Puzzel 2

string[] lines = File.ReadAllLines("Puzzel2/input.txt");

int GetNumber(char[] line, int startIndex)
{
    string number = "";
    if (char.IsNumber(line[startIndex - 1]))
    {
        if (char.IsNumber(line[startIndex - 2]))
        {
            number += line[startIndex - 2];
        }
        number += line[startIndex - 1];
    }
    number += line[startIndex];
    if (char.IsNumber(line[startIndex + 1]))
    {
        number += line[startIndex + 1];
        if (char.IsNumber(line[startIndex + 2]))
        {
            number += line[startIndex + 2];
        }
    }

    //Console.WriteLine(number);
    return int.Parse(number);
}

int GetGearRatio (string[] box)
{
    int partIndex = 0;
    int[] partNums = new int[2];

    char[] aboveLine = box[0].ToCharArray();
    char[] line = box[1].ToCharArray();
    char[] belowLine = box[2].ToCharArray();

    if (aboveLine.Length > 0)
    {
        bool newNum = true;
        for (int i = 2; i < 5; i++)
        {
            if (newNum && char.IsNumber(aboveLine[i]))
            {
                if (partIndex == 2)
                {
                    return 0;
                }

                newNum = false;
                partNums[partIndex] = GetNumber(aboveLine, i);
                partIndex++;
            }
            else if (!char.IsNumber(aboveLine[i]))
            {
                newNum = true;
            }
        }
    }

    if (char.IsNumber(line[2]))
    {
        if (partIndex == 2)
        {
            return 0;
        }

        partNums[partIndex] = GetNumber(line, 2);
        partIndex++;
    }

    if (char.IsNumber(line[4]))
    {
        if (partIndex == 2)
        {
            return 0;
        }

        partNums[partIndex] = GetNumber(line, 4);
        partIndex++;
    }

    if (belowLine.Length > 0)
    {
        bool newNum = true;
        for (int i = 2; i < 5; i++)
        {
            if (newNum && char.IsNumber(belowLine[i]))
            {
                if (partIndex == 2)
                {
                    return 0;
                }

                newNum = false;
                partNums[partIndex] = GetNumber(belowLine, i);
                partIndex++;
            }
            else if (!char.IsNumber(belowLine[i]))
            {
                newNum = true;
            }
        }
    }

    //Console.WriteLine("-----------------");
    return partNums[0] * partNums[1];
}

int sum = 0;
for (int l = 0; l < lines.Length; l++)
{
    string lineAbove = l != 0 ? "..."  + lines[l-1] + "..." : "";
    string lineBelow = l + 1 < lines.Length ? "..."  + lines[l+1] + "..." : "";
    string line = "..."  + lines[l] + "...";
    for (int c = 0; c < line.Length; c++)
    {
        if (line[c] == '*')
        {
            int start = c > 2 ? c - 3 : c;
            int length = start + 7 < line.Length ? 7 : line.Length - start;
            string newLineAbove = lineAbove != "" ? lineAbove.Substring(start, length) : lineAbove;
            string newLineBelow = lineBelow != "" ? lineBelow.Substring(start, length) : lineBelow;

            sum += GetGearRatio(new string[] {newLineAbove, line.Substring(start, length), newLineBelow});
        }
    }
}

Console.WriteLine(sum);