// Day 18
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

Dictionary<Vector2, string> trench = new Dictionary<Vector2, string>();

Vector2 currPos = new Vector2(0,0); // x,y

Vector2 GetDir(char dir)
{
    if (dir == '3')
    {
        return new Vector2(0,-1);
    }
    else if (dir == '1')
    {
        return new Vector2(0,1);
    }
    else if (dir == '0')
    {
        return new Vector2(1,0);
    }
    else // L
    {
        return new Vector2(-1,0);
    }
}

foreach (string line in lines)
{
    string color = line.Split(" ")[2].Replace("#","").Replace("(","").Replace(")","");

    char dir = color[5];
    int dist = Int32.Parse(color.Substring(0,5), System.Globalization.NumberStyles.HexNumber);

    //Console.WriteLine(dir + " " + dist + " " + color);

    for (int i = 0; i < dist; i++)
    {
        Vector2 dirVect = GetDir(dir);
        currPos = new Vector2(currPos.x + dirVect.x, currPos.y + dirVect.y);
        trench.Add(currPos, color);
    }
}

// Shoelace theorem https://rosettacode.org/wiki/Shoelace_formula_for_polygonal_area#C#
long ShoelaceArea(List<Vector2> v)
{
    int n = v.Count;
    long a = 0;
    for (int i = 0; i < n - 1; i++) {
        a += v[i].x * v[i + 1].y - v[i + 1].x * v[i].y;
    }
    return Math.Abs(a + v[n - 1].x * v[0].y - v[0].x * v[n - 1].y) / 2;
}

// Pick's theorem  https://en.wikipedia.org/wiki/Pick%27s_theorem
long area = ShoelaceArea(trench.Keys.ToList());
long interior = area - trench.Count / 2 + 1;

Console.WriteLine(trench.Count + interior);

record Vector2(int x, int y);