// Day 17
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

int h = lines.Length;
int w = lines[0].Length;

int[,] grid = new int[h,w];
for (int y = 0; y < h; y++)
{
    for (int x = 0; x < w; x++)
    {
        grid[y,x] = int.Parse(lines[y][x].ToString());
    }
}

Vector2 start = new Vector2(0, 0);
Vector2 end = new Vector2(h - 1, w - 1);
int maxDis = 3;

var queue = new PriorityQueue<State, int>();
queue.Enqueue(new State(start, 0, new Vector2(0, 1)), 0); // Right
queue.Enqueue(new State(start, 0, new Vector2(1, 0)), 0); // Down

var cost = new Dictionary<State, int>
{
    [new State(start, 0, new Vector2(0, 1))] = 0,
    [new State(start, 0, new Vector2(1, 0))] = 0
};

Vector2 Turn(Vector2 currDir, char turn)
{
    if (turn == 'L')
    {
        if (currDir == new Vector2(-1,0)) // Up
        {
            return new Vector2(0,-1);
        }
        else if (currDir == new Vector2(1,0)) // Down
        {
            return new Vector2(0,1);
        }
        else if (currDir == new Vector2(0,1)) // Right
        {
            return new Vector2(-1,0);
        }
        else if (currDir == new Vector2(0,-1)) // Left
        {
            return new Vector2(1,0);
        }
    }
    else if (turn == 'R')
    {
        if (currDir == new Vector2(-1,0)) // Up
        {
            return new Vector2(0,1);
        }
        else if (currDir == new Vector2(1,0)) // Down
        {
            return new Vector2(0,-1);
        }
        else if (currDir == new Vector2(0,1)) // Right
        {
            return new Vector2(1,0);
        }
        else if (currDir == new Vector2(0,-1)) // Left
        {
            return new Vector2(-1,0);
        }
    }
    return currDir;
}

while (queue.TryDequeue(out var state, out int heatLoss))
{
    if (state.pos == end)
    {
        Console.WriteLine(heatLoss);
        return;
    }

    foreach (char turn in new char[]{ 'L', 'R', 'S' }) // Left, right, straight
    {
        Vector2 nextDir = Turn(state.dir, turn);
        Vector2 next = new Vector2(state.pos.y + nextDir.y, state.pos.x + nextDir.x);

        int nextDis = 0;
        if (turn == 'S') // Try go straight ahead
        {
            if (state.dis + 1 > maxDis)
                continue;
            nextDis = state.dis + 1;
        }
        else // Try turn left/right
        {
            nextDis = 1;
        }

        if (next.x >= 0 && next.x < w && next.y >= 0 && next.y < h)
        {
            int nextCost = cost[state] + grid[next.y,next.x];
            State nextState = new State(next, nextDis, nextDir);
            if (!cost.TryGetValue(nextState, out int value) || nextCost < value)
            {
                cost[nextState] = nextCost;
                queue.Enqueue(nextState, nextCost);
            }
        }
    }
}

record Vector2(int y, int x);
record State(Vector2 pos, int dis, Vector2 dir);