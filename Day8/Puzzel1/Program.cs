// Day 8
// Puzzel 1

string[] lines = File.ReadAllLines("Puzzel1/input.txt");
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

string element = "AAA";
int index = 0;
int steps = 0;
// Follow instructions until ZZZ reached > Count steps needed
while (element != "ZZZ")
{
    char c = instructions[index];

    if (c == 'L')
    {
        element = elementLookup[element].Item1;
    }
    else
    {
        element = elementLookup[element].Item2;
    }
    steps++;

    if (index == instructions.Length - 1)
    {
        index = 0;
    }
    else
    {
        index++;
    }
}
Console.WriteLine(steps);