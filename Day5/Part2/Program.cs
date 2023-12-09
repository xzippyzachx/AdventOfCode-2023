// Day 5
// Part 2

using System.Diagnostics;
Stopwatch stopwatch = Stopwatch.StartNew();

string[] lines = File.ReadAllLines("Part2/input.txt");

long[] rawSeeds = Array.ConvertAll(lines[0].Split(":")[1].Trim().Split(" "), s => long.Parse(s));
List<long[]>[] maps = new List<long[]>[7];

List<Tuple<long, long>> seedRanges = new List<Tuple<long, long>>();
bool flag = false;
long seedStart = 0;
foreach (long num in rawSeeds)
{
    if (!flag)
    {
        seedStart = num;
        flag = true;
    }
    else
    {
        seedRanges.Add(new Tuple<long, long>(seedStart, seedStart + num));
        flag = false;
    }
}
seedRanges = seedRanges.OrderBy(x => x.Item1).ToList();

int skip = 0;
int mapIndex = 0;
foreach (string line in lines)
{
    skip++;
    if (skip < 4 || line == "")
    {
        continue;
    }
    
    if (line.Contains(":"))
    {
        mapIndex++;
        continue;
    }

    long[] nums = Array.ConvertAll(line.Split(" "), s => long.Parse(s));
    if (maps[mapIndex] == null)
    {
        maps[mapIndex] = new List<long[]>();
    }
    maps[mapIndex].Add(nums);
}

long loc = 0;
while (true)
{
    long mapValue = loc;
    for (int i = 6; i >= 0; i--)
    {
        foreach (long[] nums in maps[i])
        {
            long destStart = nums[1];
            long sourceStart = nums[0];
            long Length = nums[2];
            
            if (mapValue >= sourceStart && mapValue < sourceStart + Length)
            {
                mapValue = destStart + (mapValue - sourceStart);
                break;
            }
        }
    }

    foreach (Tuple<long, long> r in seedRanges)
    {
        if (mapValue >= r.Item1 && mapValue < r.Item2)
        {
            Console.WriteLine(loc);
            Console.WriteLine(stopwatch.ElapsedMilliseconds / 1000.0 + "s");
            return;
        }
    }
    loc++;
}