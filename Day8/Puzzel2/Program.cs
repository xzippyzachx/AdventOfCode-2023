// Day 8
// Puzzel 2

string[] lines = File.ReadAllLines("Puzzel2/input.txt");
Dictionary<string, Tuple<string, string>> elementLookup = new Dictionary<string, Tuple<string, string>>();

// Split off L/R instructions
char[] instructions = lines[0].ToCharArray();

// Split off look ups into > Dict of Tuples
string[] lookupLines = new string[lines.Length - 2];
Array.Copy(lines, 2, lookupLines, 0, lines.Length - 2);
foreach (string line in lookupLines)
{
    string key = line.Split(" ")[0];
    string item1 = line.Split(" ")[2].Replace("(","").Replace(",","");
    string item2 = line.Split(" ")[3].Replace(")","");

    elementLookup.Add(key, new Tuple<string, string>(item1, item2));
}

// foreach (char c in instructions)
// {
//     Console.WriteLine(c);
// }

List<string> elements = new List<string>();
foreach (string key in elementLookup.Keys)
{
    if (key.EndsWith('A'))
    {
        elements.Add(key);
    }
}

// foreach (string e in elements)
// {
//     Console.WriteLine(e);
// }

long[] steps = new long[elements.Count];
// Follow instructions until ZZZ reached > Count steps needed
for (int i = 0; i < elements.Count; i++)
{
    int index = 0;
    while (!elements[i].EndsWith('Z'))
    {
        char c = instructions[index];
        if (c == 'L')
        {
            elements[i] = elementLookup[elements[i]].Item1;
        }
        else
        {
            elements[i] = elementLookup[elements[i]].Item2;
        }
        
        steps[i]++;

        if (index == instructions.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }
}

// foreach (int c in steps)
// {
//     Console.WriteLine(c);
// }

Console.WriteLine(LCMArray(steps));

long LCMArray(long[] arr)
{
    long lcm = arr[0];
    for (long i = 1; i < arr.Length; i++)
    {
        lcm = LCM(lcm, arr[i]);
    }
    return lcm;
}

long GCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

long LCM(long a, long b)
{
    return (a * b) / GCD(a, b);
}