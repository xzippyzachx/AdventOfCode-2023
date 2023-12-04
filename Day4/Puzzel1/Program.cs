// Day 4
// Puzzel 1

string[] lines = File.ReadAllLines("Puzzel1/input.txt");

int DoublePoints(int times)
{
    if (times == 0)
    {
        return 0;
    }

    return (int) MathF.Pow(2, times - 1);
}

int sum = 0;
foreach (string line in lines)
{
    string[] card = line.Split(":")[1].Split("|");
    string[] nums = card[1].Trim().Replace("  ", " ").Split(" ");
    HashSet<string> winNums = new HashSet<string>(card[0].Trim().Replace("  ", " ").Split(" "));

    int times = 0;
    foreach (string num in winNums)
    {
        if (nums.Contains(num))
        {
            times++;
        }
    }
    //Console.WriteLine(">>>> " + times + " >>>> " + DoublePoints(times));
    sum += DoublePoints(times);
}
Console.WriteLine(sum);