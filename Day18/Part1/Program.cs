// Day 18
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

Dictionary<Vector2, string> trench = new Dictionary<Vector2, string>();
Dictionary<Vector2, string> trenchInterior  = new Dictionary<Vector2, string>();

Vector2 currPos = new Vector2(0,0); // x,y
int minH = 0;
int maxH = 0;
int minW = 0;
int maxW = 0;

Vector2 GetDir(char dir)
{
    if (dir == 'U')
    {
        return new Vector2(0,-1);
    }
    else if (dir == 'D')
    {
        return new Vector2(0,1);
    }
    else if (dir == 'R')
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
    char dir = line.Split(" ")[0][0];
    int dist = int.Parse(line.Split(" ")[1]);
    string color = line.Split(" ")[2].Replace("#","").Replace("(","").Replace(")","");

    //Console.WriteLine(dir + " " + dist + " " + color);

    for (int i = 0; i < dist; i++)
    {
        Vector2 dirVect = GetDir(dir);
        currPos = new Vector2(currPos.x + dirVect.x, currPos.y + dirVect.y);

        trench.Add(currPos, color);

        if (currPos.y + 1 > maxH)
        {
            maxH = currPos.y + 1;
        }
        if (currPos.y < minH)
        {
            minH = currPos.y;
        }
        if (currPos.x + 1 > maxW)
        {
            maxW = currPos.x + 1;
        }
        if (currPos.x < minW)
        {
            minW = currPos.x;
        }
    }
}

void Dig(Vector2 pos)
{
    if (trench.ContainsKey(pos) || trenchInterior.ContainsKey(pos))
    {
        return;
    }
    trenchInterior.Add(pos, "");

    foreach (char dir in new char[] {'U', 'D', 'R', 'L'})
    {
        Vector2 dirVect = GetDir(dir);
        Vector2 newPos = new Vector2(pos.x + dirVect.x, pos.y + dirVect.y);
        if (!trench.ContainsKey(newPos) && !trenchInterior.ContainsKey(newPos))
        {
            Dig(newPos);
        }
    }
}

Thread thread = new Thread(new ThreadStart(Solve), 10000000);
thread.Start();

void Solve()
{
    Vector2 startPos = new Vector2(1,1); // x,y
    Dig(startPos);

    for (int y = minH; y < maxH; y++)
    {
        string slice = "";
        for (int x = minW; x < maxW; x++)
        {
            if (x == 0 && y ==0)
            {
                slice += "S";
            }
            if (trench.ContainsKey(new Vector2(x,y)) || trenchInterior.ContainsKey(new Vector2(x,y)))
            {
                slice += "#";
            }
            else
            {
                slice += ".";
            }
        }
        //Console.WriteLine(slice);
    }
    Console.WriteLine(trench.Count + trenchInterior.Count);
}

record Vector2(int x, int y);