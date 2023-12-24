// Day 24
// Part 1

using System.Numerics;

string[] lines = File.ReadAllLines("Part1/input.txt");

long minXY = 200000000000000;
long maxXY = 400000000000000;

List<Path> paths = new List<Path>();
foreach (string line in lines)
{
    string[] split = line.Split(" @ ");
    string[] posSplit = split[0].Split(", ");
    string[] velSplit = split[1].Split(", ");

    long posX = long.Parse(posSplit[0]);
    long posY = long.Parse(posSplit[1]);
    long posZ = long.Parse(posSplit[2]);

    long velX = long.Parse(velSplit[0]);
    long velY = long.Parse(velSplit[1]);
    long velZ = long.Parse(velSplit[2]);

    paths.Add(new Path(new Vector2(posX,posY), new Vector2(velX,velY)));
}

float CrossProduct(Vector2 v1, Vector2 v2)
{
    return v1.X * v2.Y - v1.Y * v2.X;
}

Vector2? FindIntersection(Vector2 point1, Vector2 direction1, Vector2 point2, Vector2 direction2)
{
    // Check for parallel lines
    if (CrossProduct(direction1, direction2) == 0)
    {
        return null; // Lines are parallel or coincident
    }

    // Calculate the intersection point using Cramer's rule
    float det = Vector2.Dot(direction1, new Vector2(-direction2.Y, direction2.X));
    float s = Vector2.Dot(new Vector2(-direction2.Y, direction2.X), point2 - point1) / det;

    Vector2 intersectionPoint = point1 + s * direction1;
    return intersectionPoint;
}

int cross = 0;
for (int a = 0; a < paths.Count; a++)
{
    for (int b = a + 1; b < paths.Count; b++)
    { 
        Path pA = paths[a];
        Path pB = paths[b];

        Vector2? intersect = FindIntersection(pA.position, pA.velocity, pB.position, pB.velocity);
        if (intersect.HasValue)
        {
            // Check if intersect is inside the test area
            if (intersect.Value.X >= minXY && intersect.Value.X <= maxXY && intersect.Value.Y >= minXY && intersect.Value.Y <= maxXY)
            {
                // Check if intersect is in the future
                if ((intersect.Value.X > pA.position.X) == (pA.velocity.X > 0) && (intersect.Value.X > pB.position.X) == (pB.velocity.X > 0))
                {
                    cross++;
                    // Console.WriteLine("A:" + pA);
                    // Console.WriteLine("B:" + pB);
                    // Console.WriteLine(intersect.Value);
                }
            }
        }
    }
}
Console.WriteLine(cross);

record Path(Vector2 position, Vector2 velocity);