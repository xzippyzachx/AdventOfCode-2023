// Day 6
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

long time = long.Parse(lines[0].Split(":")[1].Replace(" ", ""));
long distance = long.Parse(lines[1].Split(":")[1].Replace(" ", ""));

long winAmount = 0;
for (long i = 0; i <= time; i++)
{
    long timeHold = i;
    long timeMove = time - i;
    long dist = timeMove * timeHold;
    if (dist > distance)
    {
        winAmount++;
    }
}

Console.WriteLine(winAmount);