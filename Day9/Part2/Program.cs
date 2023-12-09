// Day 9
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

int total = 0;
foreach (string line in lines)
{
    int[] sequence = Array.ConvertAll(line.Split(" "), int.Parse);
    List<List<int>> stepDiffs = new List<List<int>> { sequence.ToList() };

    int diffTotal = 1;
    while (diffTotal != 0)
    {
        diffTotal = 0;
        List<int> newSeq = new List<int>();
        int last = 0;
        for (int i = sequence.Length - 1; i >= 0 ; i--)
        {
            if (i < sequence.Length - 1)
            {
                int diff = last - sequence[i];
                diffTotal += diff;
                newSeq.Insert(0, diff);
            }
            last = sequence[i];
        }
        stepDiffs.Add(newSeq);
        sequence = newSeq.ToArray();
    }

    stepDiffs[stepDiffs.Count - 1].Insert(0, 0);
    for (int i = stepDiffs.Count - 2; i >=0; i--)
    {
        stepDiffs[i].Insert(0, stepDiffs[i][0] - stepDiffs[i+1][0]);
    }

    total += stepDiffs[0][0];
}
Console.WriteLine(total);