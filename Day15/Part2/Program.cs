// Day 15
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

string[] values = lines[0].Split(",");

int Hash(string val)
{
    int curr = 0;
    foreach (char c in val)
    {
        curr += (int)c;
        curr *= 17;
        curr = curr % 256;
    }
    return curr;
}

Dictionary<int, List<Lens>> boxs = new Dictionary<int, List<Lens>>();
foreach (string val in values)
{
    char op = val.Contains("=") ? '=' : '-';
    string label = op == '=' ? val.Split("=")[0] : val.Split("-")[0];
    int focal = op == '=' ? int.Parse(val.Split("=")[1]) : 0;

    int hash = Hash(label);

    if (op == '-')
    {
        if (boxs.ContainsKey(hash))
        {
            int index = boxs[hash].FindIndex(l => l.label == label);
            if (index != -1)
            {
                boxs[hash].RemoveAt(index);
            }
        }
    }
    else if (op == '=')
    {
        Lens lens = new Lens(label, focal);
        if (boxs.ContainsKey(hash))
        {
            int index = boxs[hash].FindIndex(l => l.label == label);
            if (index != -1)
            {
                boxs[hash].RemoveAt(index);
                boxs[hash].Insert(index, lens);
            }
            else
            {
                boxs[hash].Add(lens);
            }
        }
        else
        {
            boxs.Add(hash, new List<Lens>{ lens });
        }
    }
}

int sum = 0;
foreach (int hash in boxs.Keys)
{
    for(int slot = 0; slot < boxs[hash].Count; slot++)
    {
        sum += (hash + 1) * (slot + 1) * boxs[hash][slot].focal;
    }
}
Console.WriteLine(sum);

record Lens(string label, int focal);