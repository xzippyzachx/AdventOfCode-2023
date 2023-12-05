// Day 5
// Puzzel 1

string[] lines = File.ReadAllLines("Puzzel1/input.txt");

long[] seeds = Array.ConvertAll(lines[0].Split(":")[1].Trim().Split(" "), s => long.Parse(s));
List<long[]>[] maps = new List<long[]>[7];

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
        //Console.WriteLine("----------------------");
        continue;
    }

    long[] nums = Array.ConvertAll(line.Split(" "), s => long.Parse(s));
    if (maps[mapIndex] == null)
    {
        maps[mapIndex] = new List<long[]>();
    }
    maps[mapIndex].Add(nums);

    //Console.WriteLine(nums.ToString());
}

for (int i = 0; i < 7; i++)
{
    for (int s = 0; s < seeds.Length; s++)
    {
        foreach (long[] nums in maps[i])
        {
            long destStart = nums[0];
            long sourceStart = nums[1];
            long Length = nums[2];
            
            if (seeds[s] >= sourceStart && seeds[s] < sourceStart + Length)
            {
                seeds[s] = destStart + (seeds[s] - sourceStart);
                break;
            }
        }
    }
}

Console.WriteLine(seeds.Min());