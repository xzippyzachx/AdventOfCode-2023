// Day 22
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

PriorityQueue<Brick, int> bricks = new PriorityQueue<Brick, int>();
List<Brick> stackedBricks = new List<Brick>();

foreach (string line in lines)
{
    string[] split = line.Split("~");
    string[] startSplit = split[0].Split(",");
    string[] endSplit = split[1].Split(",");

    Vector3 start = new Vector3(int.Parse(startSplit[0]),int.Parse(startSplit[1]),int.Parse(startSplit[2]));
    Vector3 end = new Vector3(int.Parse(endSplit[0]),int.Parse(endSplit[1]),int.Parse(endSplit[2]));
    Brick newBrick = new Brick(start,end);

    bricks.Enqueue(newBrick, start.z);
}

// Fall to create stack
while(bricks.TryDequeue(out Brick? brick, out int priority))
{
    // Fall until hit brick
    int newZ = brick.Fall(stackedBricks);

    // Update height
    int oldZ = brick.zBottom;
    int fallDist = oldZ - newZ;
    brick.start.z -= fallDist;
    brick.end.z -= fallDist;
    
    // Add to stacked bricks
    stackedBricks.Add(brick);
}

int sum = 0;
int index = 0;
foreach (Brick brick in stackedBricks)
{
    bool singleSupport = false;
    foreach (Brick sBrick in brick.supports)
    {
        if (sBrick.supported.Count <= 1)
        {
            singleSupport = true;
        }
    }

    if (!singleSupport)
    {
        //Console.WriteLine(index);
        sum++;
    }
    index++;
}
Console.WriteLine(sum);

enum BrickOrientation
{
    X,
    Y,
    Z
}

class Brick
{
    public Vector3 start;
    public Vector3 end;
    public Brick(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
    }

    public int zTop => start.z > end.z ? start.z : end.z;
    public int zBottom => start.z < end.z ? start.z : end.z;

    public List<Brick> supported = new List<Brick>();
    public List<Brick> supports = new List<Brick>();

    public BrickOrientation Orientation()
    {
        int xLength = end.x - start.x;
        int yLength = end.y - start.y;

        if (xLength > 0)
        {
            return BrickOrientation.X;
        }
        else if (yLength > 0)
        {
            return BrickOrientation.Y;
        }
        return BrickOrientation.Z;
    }

    public int Fall(List<Brick> stackedBricks)
    {
        for (int newZ = zBottom - 1; newZ > 0; newZ--)
        {
            bool land = false;
            foreach (Brick sBrick in stackedBricks)
            {
                if (newZ == sBrick.zTop)
                {
                    if (Overlap(sBrick, newZ))
                    {
                        land = true;
                        supported.Add(sBrick);
                        sBrick.supports.Add(this);
                    }
                }
            }

            if (land)
            {
                return newZ + 1;
            }
        }
        return 1;
    }

    public List<Vector3> GetSurface(int z)
    {
        List<Vector3> points = new List<Vector3>();
        BrickOrientation thisOrientation = Orientation();
        if (thisOrientation == BrickOrientation.X)
        {   
            for (int x = start.x; x <= end.x; x++)
            {
                points.Add(new Vector3(x, start.y, z));
            }
        }
        else if (thisOrientation == BrickOrientation.Y)
        {
            for (int y = start.y; y <= end.y; y++)
            {
                points.Add(new Vector3(start.x, y, z));
            }
        }
        else
        {
            points.Add(new Vector3(start.x, start.y, z));
        }
        return points;
    }

    public bool Overlap(Brick brick, int z)
    {
        foreach (Vector3 p1 in GetSurface(z))
        {
            foreach (Vector3 p2 in brick.GetSurface(z))
            {
                if (p1.x == p2.x && p1.y == p2.y)
                {
                    return true;
                }
            }
        }        
        return false;
    }
}
class Vector3
{
    public int x;
    public int y;
    public int z;
    public Vector3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public override string ToString()
    {
        return "(" + x + "," + y + "," + z + ")";
    }
}