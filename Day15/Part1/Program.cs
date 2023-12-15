// Day 15
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

string[] values = lines[0].Split(",");
int sum = 0;
foreach (string val in values)
{
    int curr = 0;
    foreach (char c in val)
    {
        curr += (int)c;
        curr *= 17;
        curr = curr % 256;
    }
    sum += curr;
}
Console.WriteLine(sum);