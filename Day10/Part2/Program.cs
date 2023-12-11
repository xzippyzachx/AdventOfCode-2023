// Day 10
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");
List<char[]> map = new List<char[]>();
Tuple<int,int> startPos = new Tuple<int, int>(0,0);

for (int l = 0; l < lines.Length; l++)
{
    string line = lines[l];
    map.Add(line.ToCharArray());

    if (line.Contains('S'))
    {
        startPos = new Tuple<int, int>(l, line.IndexOf('S'));
    }
}

// north: y -1
// south: y 1
// east: x 1
// west: x -1
//              pipe         last direction      next direction
Dictionary<Tuple<char,Tuple<short,short>>,Tuple<short,short,List<Tuple<short,short>>>> pipeMap = new Dictionary<Tuple<char,Tuple<short,short>>,Tuple<short,short,List<Tuple<short,short>>>>()
{
    { new Tuple<char, Tuple<short, short>>('|', new Tuple<short, short>(1,0)), new Tuple<short,short,List<Tuple<short,short>>>(1,0,new List<Tuple<short,short>>() { new Tuple<short,short>(0,1) }) },
    { new Tuple<char, Tuple<short, short>>('|', new Tuple<short, short>(-1,0)), new Tuple<short,short,List<Tuple<short,short>>>(-1,0,new List<Tuple<short,short>>() { new Tuple<short,short>(0,-1) }) },
    { new Tuple<char, Tuple<short, short>>('-', new Tuple<short, short>(0,1)), new Tuple<short,short,List<Tuple<short,short>>>(0,1,new List<Tuple<short,short>>() { new Tuple<short,short>(-1,0) }) },
    { new Tuple<char, Tuple<short, short>>('-', new Tuple<short, short>(0,-1)), new Tuple<short,short,List<Tuple<short,short>>>(0,-1,new List<Tuple<short,short>>() { new Tuple<short,short>(1,0) }) },
    { new Tuple<char, Tuple<short, short>>('L', new Tuple<short, short>(1,0)), new Tuple<short,short,List<Tuple<short,short>>>(0,1,new List<Tuple<short,short>>()) },
    { new Tuple<char, Tuple<short, short>>('L', new Tuple<short, short>(0,-1)), new Tuple<short,short,List<Tuple<short,short>>>(-1,0,new List<Tuple<short,short>>() { new Tuple<short,short>(0,-1),new Tuple<short,short>(1,0) }) },
    { new Tuple<char, Tuple<short, short>>('J', new Tuple<short, short>(1,0)), new Tuple<short,short,List<Tuple<short,short>>>(0,-1,new List<Tuple<short,short>>() { new Tuple<short,short>(0,1),new Tuple<short,short>(1,0) }) },
    { new Tuple<char, Tuple<short, short>>('J', new Tuple<short, short>(0,1)), new Tuple<short,short,List<Tuple<short,short>>>(-1,0,new List<Tuple<short,short>>()) },
    { new Tuple<char, Tuple<short, short>>('7', new Tuple<short, short>(-1,0)), new Tuple<short,short,List<Tuple<short,short>>>(0,-1,new List<Tuple<short,short>>()) },
    { new Tuple<char, Tuple<short, short>>('7', new Tuple<short, short>(0,1)), new Tuple<short,short,List<Tuple<short,short>>>(1,0,new List<Tuple<short,short>>() { new Tuple<short,short>(0,1),new Tuple<short,short>(-1,0) }) },
    { new Tuple<char, Tuple<short, short>>('F', new Tuple<short, short>(-1,0)), new Tuple<short,short,List<Tuple<short,short>>>(0,1,new List<Tuple<short,short>>() { new Tuple<short,short>(0,-1),new Tuple<short,short>(-1,0) }) },
    { new Tuple<char, Tuple<short, short>>('F', new Tuple<short, short>(0,-1)), new Tuple<short,short,List<Tuple<short,short>>>(1,0,new List<Tuple<short,short>>()) },
};

List<Tuple<int,int>> loopPipes = new List<Tuple<int,int>>();
List<Tuple<int,int>> checkInsideTiles = new List<Tuple<int,int>>();

int length = 0;
Tuple<short,short> dir = new Tuple<short, short>(1,0);
Tuple<int,int> pos = startPos;
char pipe = '7';
while (pipe != 'S')
{
    pos = new Tuple<int,int>(pos.Item1 + dir.Item1, pos.Item2 + dir.Item2);
    pipe = map[pos.Item1][pos.Item2];
    loopPipes.Add(pos);
    // Console.WriteLine(pos);
    Tuple<char, Tuple<short, short>> key = new Tuple<char, Tuple<short, short>>(pipe, dir);
    if (pipeMap.ContainsKey(key))
    {
        dir = new Tuple<short,short>(pipeMap[key].Item1, pipeMap[key].Item2);

        List<Tuple<short,short>> insideDirs = pipeMap[key].Item3;
        foreach (Tuple<short,short> insideDir in insideDirs)
        {
            Tuple<int,int> insidePos = new Tuple<int,int>(pos.Item1 + insideDir.Item1, pos.Item2 + insideDir.Item2);
            if (!checkInsideTiles.Contains(insidePos))
            {
                checkInsideTiles.Add(insidePos);
            }
        }
    }
    length++;
}

List<Tuple<int,int>> checkedTiles = new List<Tuple<int,int>>();
List<Tuple<int,int>> insideTiles = new List<Tuple<int,int>>();
foreach (Tuple<int,int> checkTile in checkInsideTiles)
{
    if (!FindMapEdge(checkTile))
    {
        insideTiles.AddRange(checkedTiles);
    }
    checkedTiles.Clear();
}

Console.WriteLine(insideTiles.Count);

bool FindMapEdge(Tuple<int,int> tilePos)
{
    if (tilePos.Item1 >= map.Count || tilePos.Item1 < 0 || tilePos.Item2 >= map[0].Length || tilePos.Item2 < 0)
    {
        return true;
    }

    if (loopPipes.Contains(tilePos) || checkedTiles.Contains(tilePos) || insideTiles.Contains(tilePos))
    {
        return false;
    }

    checkedTiles.Add(tilePos);

    if (FindMapEdge(new Tuple<int,int>(tilePos.Item1 + 1, tilePos.Item2)) ||
        FindMapEdge(new Tuple<int,int>(tilePos.Item1 - 1, tilePos.Item2)) ||
        FindMapEdge(new Tuple<int,int>(tilePos.Item1, tilePos.Item2 + 1)) ||
        FindMapEdge(new Tuple<int,int>(tilePos.Item1, tilePos.Item2 - 1)))
    {
        return true;
    }
    return false;
}