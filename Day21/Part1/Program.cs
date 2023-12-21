// Day 21
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

char[,] garden = new char[lines.Length,lines[0].Length];
Vector2 startPos = new Vector2(0,0);

Vector2[] dirs =
{
    new Vector2(-1,0),  // North
    new Vector2(1,0),   // South
    new Vector2(0,1),  // East
    new Vector2(0,-1),  // West
};

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        garden[y,x] = lines[y][x];

        if (lines[y][x] == 'S')
        {
            startPos = new Vector2(y,x);
        }
    }
}

int maxSteps = 64;
Queue<Vector2> nextSteps = new Queue<Vector2>();
nextSteps.Enqueue(startPos);
for (int s = 0; s < maxSteps; s++)
{
    Queue<Vector2> currSteps = new Queue<Vector2>(nextSteps);
    nextSteps.Clear();
    while (currSteps.TryDequeue(out Vector2? pos))
    {
        foreach (Vector2 dir in dirs)
        {
            Vector2 newPos = new Vector2(pos.y + dir.y, pos.x + dir.x);

            if (newPos.y < 0 || newPos.y >= lines.Length || newPos.x < 0 || newPos.x >= lines[0].Length)
            {
                continue;
            }

            if (garden[newPos.y,newPos.x] == '#')
            {
                continue;
            }

            if (!nextSteps.Contains(newPos))
            {
                //Console.WriteLine(newPos);
                nextSteps.Enqueue(newPos);
            }
        }
    }
    //Console.WriteLine(s + " ----------------");
}
Console.WriteLine(nextSteps.Count);

record Vector2(int y, int x);