// Day 9
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

int total = 0;
foreach (string line in lines)
{
    int[] sequence = Array.ConvertAll(line.Split(" "), int.Parse);
    List<List<int>> stepDiffs = new List<List<int>> { sequence.ToList() };

    int diffTotal = 1;
    int endDiffTotal = 0;
    while (diffTotal != 0)
    {
        diffTotal = 0;
        List<int> newSeq = new List<int>();
        int last = 0;
        for (int i = 0; i < sequence.Length; i++)
        {
            if (i > 0)
            {
                int diff = sequence[i] - last;
                diffTotal += diff;
                newSeq.Add(diff);

                if (i == sequence.Length - 1)
                {
                    endDiffTotal += diff;
                    //Console.WriteLine(endDiffTotal);
                }
            }
            last = sequence[i];
        }
        stepDiffs.Add(newSeq);
        sequence = newSeq.ToArray();

        // foreach (int s in newSeq)
        // {
        //     Console.WriteLine(s);
        // }
        // Console.WriteLine("---------------");
    }
    total += stepDiffs[0].Last() + endDiffTotal;
}
Console.WriteLine(total);