// Day 12
// Part 1

using System.Collections;

string[] lines = File.ReadAllLines("Part1/input.txt");

bool Valid(string springs, int[] sizes)
{
    int springCount = 0;
    int length = 0;
    bool space = true;
    foreach (char c in springs)
    {
        if (c == '.')
        {
            space = true;
            if (length > 0) // Spring too short
            {
                return false;
            }
        }
        if (c == '#')
        {
            length++;
            if (space == false) // Spring too long
            {
                return false;
            }

            if (springCount == sizes.Length) // Too many springs
            {
                return false;
            }
        }

        if (space == true && springCount != sizes.Length && length == sizes[springCount])
        {
            space = false;
            length = 0;
            springCount++;
        }
    }
    return springCount == sizes.Length;
}

List<char[]> GenerateArrangements(int length)
{
    List<char[]> arrangements = new List<char[]>();
    for (int i = 0; i < Math.Pow(2, length); i++)
    {
        bool[] bitArray = new BitArray(new int[] { i }).Cast<bool>().ToArray();
        char[] newArrangement = new char[bitArray.Length];
        for (int b = 0; b < bitArray.Length; b++)
        {
            newArrangement[b] = bitArray[b] == true ? '#' : '.';
        }
        arrangements.Add(newArrangement);
    }
    return arrangements;
}

int sum = 0;
foreach (string line in lines)
{
    string springs =  line.Split(" ")[0];
    int[] sizes =  Array.ConvertAll(line.Split(" ")[1].Split(","), int.Parse);
    
    string temp = springs.Replace("?", "");
    List<char[]> arrangements = GenerateArrangements(springs.Length - temp.Length);
    foreach (char[] arrangement in arrangements)
    {
        string temp2 = "";
        int i = 0;
        for (int c = 0; c < line.Length; c++)
        {
            if (line[c] == '?')
            {
                temp2 += arrangement[i];
                i++;
            }
            else
            {
                temp2 += line[c];
            }
        }

        if (Valid(temp2, sizes))
        {
            //Console.WriteLine(temp2);
            sum++;
        }
    }
    //Console.WriteLine("--------------------");
}
Console.WriteLine(sum);