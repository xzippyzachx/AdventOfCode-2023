// Day 23
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

char[,] map = new char[lines.Length,lines[0].Length];
char[,] printMap = new char[lines.Length,lines[0].Length];
Vector2 startPos = new Vector2(0,1);
Vector2 endPos = new Vector2(lines.Length-1,lines[0].Length-2);

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
        map[y,x] = lines[y][x];
        printMap[y,x] = lines[y][x];
    }
}

// How to optimize:
// Create a new graph just including start, end and intersections
// BFS on the new optimized graph

Queue<Step> nextSteps = new Queue<Step>();
nextSteps.Enqueue(new Step(startPos, 0, new List<Vector2>() { startPos }));
int longest = 0;
while(nextSteps.TryDequeue(out Step? step))
{
    foreach (Vector2 dir in dirs)
    {
        Vector2 newPos = new Vector2(step.pos.y + dir.y, step.pos.x + dir.x);
        
        int newDist = step.dist + 1;

        if (newPos.y < 0 || newPos.y >= lines.Length || newPos.x < 0 || newPos.x >= lines[0].Length)
        {
            continue;
        }

        if (step.visited.Contains(newPos))
        {
            continue;
        }

        if (newPos == endPos)
        {
            if (newDist > longest)
            {
                longest = newDist;
            }
            //Console.WriteLine("End: " + newDist);
            continue;
        }

        if (map[newPos.y,newPos.x] == '#')
        {
            continue;
        }
        printMap[newPos.y,newPos.x] = 'O';
        
        List<Vector2> newVisited = new List<Vector2>(step.visited) { newPos };

        nextSteps.Enqueue(new Step(newPos, newDist, newVisited));
    }
}
Console.WriteLine(longest);

// for (int y = 0; y < lines.Length; y++)
// {
//     string line = "";
//     for (int x = 0; x < lines[y].Length; x++)
//     {
//         line += printMap[y,x];
//     }
//     Console.WriteLine(line);
// }

record Step(Vector2 pos, int dist, List<Vector2> visited);
record Vector2(int y, int x);