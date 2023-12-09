// Day 6
// Part 1

using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("Part1/input.txt");

int[] times = Array.ConvertAll(Regex.Replace(lines[0], @"\s+", " ").Split(":")[1].Trim().Split(" "), s => int.Parse(s));
int[] distances = Array.ConvertAll(Regex.Replace(lines[1], @"\s+", " ").Split(":")[1].Trim().Split(" "), s => int.Parse(s));

int totalMulti = 1;
for (int t = 0; t < times.Length; t++)
{
    int winAmount = 0;
    for (int i = 0; i <= times[t]; i++)
    {
        int timeHold = i;
        int timeMove = times[t] - i;
        int distance = timeMove * timeHold;
        if (distance > distances[t])
        {
            winAmount++;
        }
    }
    totalMulti *= winAmount;
}

Console.WriteLine(totalMulti);