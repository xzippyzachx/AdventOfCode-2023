// Day 3
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

bool IsSymbol(char c)
{
    char[] symbols = {'#','$','+','*','/','=','&','%','@','-'};
    return Array.IndexOf(symbols, c) != -1;
}

bool HasAdjacentSymbol (int startIndex, int numLength, char[] aboveLine, char[] line, char[] belowLine)
{
    if (startIndex > 0 && IsSymbol(line[startIndex-1]))
    {
        return true;
    }
    if (startIndex + numLength < line.Length && IsSymbol(line[startIndex+numLength]))
    {
        return true;
    }

    int start = startIndex > 0 ? startIndex - 1 : startIndex;
    int end = startIndex + numLength < line.Length ? startIndex + numLength + 1 : startIndex + numLength;
    if (aboveLine.Length > 0)
    {
        for (int i = start; i < end; i++)
        {
            if (IsSymbol(aboveLine[i]))
            {
                return true;
            }
        }
    }
    if (belowLine.Length > 0)
    {
        for (int i = start; i < end; i++)
        {
            if (IsSymbol(belowLine[i]))
            {
                return true;
            }
        }
    }

    return false;
}

int sum = 0;
for (int l = 0; l < lines.Length; l++)
{
    int startIndex = 0;
    bool foundNumber = false;
    string number = "";
    char[] line = lines[l].ToCharArray();
    for (int c = 0; c < line.Length; c++)
    {
        if (Char.IsNumber(line[c]))
        {
            if (!foundNumber)
            {
                foundNumber = true;
                startIndex = c;
            }
            number += line[c];
        }
        if (foundNumber && (!Char.IsNumber(line[c]) || c + 1 == line.Length))
        {
            char[] lineAbove = l != 0 ? lines[l-1].ToCharArray() : new char[0];
            char[] lineBelow = l + 1 < lines.Length ? lines[l+1].ToCharArray() : new char[0];
            if (HasAdjacentSymbol(startIndex, c - startIndex, lineAbove, line, lineBelow))
            {
                //Console.WriteLine(number);
                //Console.WriteLine("------");
                sum += int.Parse(number);
            }
            foundNumber = false;
            number = "";
        }

    }
}

Console.WriteLine(sum);