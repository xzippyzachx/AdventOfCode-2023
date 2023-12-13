// Day 12
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

long sum = 0;
foreach (string line in lines)
{
    string rawSprings =  line.Split(" ")[0];
    int[] rawSizes =  Array.ConvertAll(line.Split(" ")[1].Split(","), int.Parse);

    string[] springsArr = new string[5];
    int[] sizes = new int[rawSizes.Length * 5 + 1];
    for (int i = 0; i < 5; i++)
    {
        springsArr[i] = rawSprings;
        for (int j = 0; j < rawSizes.Length; j++)
        {
            sizes[(i*rawSizes.Length)+j] = rawSizes[j];
        }
    }
    string springs = string.Join("?", springsArr) + ".";

    int n = springs.Length;
    int k = sizes.Length - 1;

    sizes[sizes.Length - 1] = n + 1;
    // Console.WriteLine("[{0}]", string.Join(", ", sizes));
    // Console.WriteLine(springs);

    long[,,] f = new long[n+1,k+2,n+2];
    f[0,0,0] = 1;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < k+1; j++)
        {
            for (int p = 0; p < n+1; p++)
            {
                long curr = f[i,j,p];
                if (curr == 0)
                {
                    continue;
                }
                if (springs[i] == '.' || springs[i] == '?')
                {
                    if (p == 0 || p == sizes[j-1])
                    {
                        f[i+1,j,0] += curr;
                    }
                }
                if (springs[i] == '#' || springs[i] == '?')
                {
                    int tmp = p == 0 ? 1 : 0; 
                    f[i+1,j+tmp,p+1] += curr;
                }
            }
        }
    }

    sum += f[n,k,0];
}
Console.WriteLine(sum);