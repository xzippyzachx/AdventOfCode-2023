// Day 16
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

int h = lines.Length;
int w = lines[0].Length;

Dictionary<Vector2, List<Vector2>> energized = new Dictionary<Vector2, List<Vector2>>();

void Energize(Vector2 pos, Vector2 dir)
{
    if (energized.ContainsKey(pos))
    {
        energized[pos].Add(dir);
    }
    else
    {
        energized.Add(pos, new List<Vector2>() { dir });
    }
}

void Move(Vector2 currPos, Vector2 dir)
{
    Vector2 newPos = new Vector2(currPos.y + dir.y, currPos.x + dir.x);
    if (newPos.y < 0 || newPos.y >= h || newPos.x < 0 || newPos.x >= w)
    {
        return;
    }

    if (energized.ContainsKey(newPos))
    {
        foreach (Vector2 d in energized[newPos])
        {
            if (d == dir)
            {
                return;
            }
        } 
    }

    Energize(newPos, dir);

    char tile = lines[newPos.y][newPos.x];
    if (tile == '.')
    {
        Move(newPos, dir);
    }
    else if (tile == '\\')
    {
        if (dir == new Vector2(-1, 0)) // North
        {
            Move(newPos, new Vector2(0, -1));
        }
        else if (dir == new Vector2(1, 0)) // South
        {
            Move(newPos, new Vector2(0, 1));
        }
        else if (dir == new Vector2(0, 1)) // East
        {
            Move(newPos, new Vector2(1, 0));
        }
        else if (dir == new Vector2(0, -1)) // West
        {
            Move(newPos, new Vector2(-1, 0));
        }
    }
    else if (tile == '/')
    {
        if (dir == new Vector2(-1, 0)) // North
        {
            Move(newPos, new Vector2(0, 1));
        }
        else if (dir == new Vector2(1, 0)) // South
        {
            Move(newPos, new Vector2(0, -1));
        }
        else if (dir == new Vector2(0, 1)) // East
        {
            Move(newPos, new Vector2(-1, 0));
        }
        else if (dir == new Vector2(0, -1)) // West
        {
            Move(newPos, new Vector2(1, 0));
        }
    }
    else if (tile == '|')
    {
        if (dir == new Vector2(-1, 0) || dir == new Vector2(1, 0)) // North or South
        {
            Move(newPos, dir);
        }
        else if (dir == new Vector2(0, 1) || dir == new Vector2(0, -1)) // East or West
        {
            Move(newPos, new Vector2(-1, 0));
            Move(newPos, new Vector2(1, 0));
        }
    }
    else if (tile == '-')
    {
        if (dir == new Vector2(0, 1) || dir == new Vector2(0, -1)) // East or West
        {
            Move(newPos, dir);
        }
        else if (dir == new Vector2(-1, 0) || dir == new Vector2(1, 0)) // North or South
        {
            Move(newPos, new Vector2(0, -1));
            Move(newPos, new Vector2(0, 1));
        }
    }
}

Thread thread = new Thread(new ThreadStart(Solve), 10000000);
thread.Start();

void Solve()
{
    int mostEnergized = 0;
    void CheckMost()
    {
        if (energized.Count > mostEnergized)
        {
            mostEnergized = energized.Count;
        }
        energized.Clear();
    }

    for (int i = 0; i < w; i++)
    {
        Move(new Vector2(-1,i), new Vector2(1,0));
        CheckMost();

        Move(new Vector2(h,i), new Vector2(-1,0));
        CheckMost();
    }

    for (int i = 0; i < h; i++)
    {
        Move(new Vector2(i,-1), new Vector2(0,1));
        CheckMost();

        Move(new Vector2(i,w), new Vector2(0,-1));
        CheckMost();
    }
    Console.WriteLine(mostEnergized);
}

record Vector2(int y, int x);