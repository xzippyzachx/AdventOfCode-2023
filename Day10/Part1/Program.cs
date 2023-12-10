// Day 10
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");
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

Console.WriteLine(startPos);

// north: y -1
// south: y 1
// east: x 1
// west: x -1
//              pipe         last direction      next direction
Dictionary<Tuple<char,Tuple<short,short>>,Tuple<short,short>> pipeMap = new Dictionary<Tuple<char,Tuple<short,short>>,Tuple<short,short>>()
{
    { new Tuple<char, Tuple<short, short>>('|', new Tuple<short, short>(1,0)), new Tuple<short,short>(1,0) },
    { new Tuple<char, Tuple<short, short>>('|', new Tuple<short, short>(-1,0)), new Tuple<short,short>(-1,0) },
    { new Tuple<char, Tuple<short, short>>('-', new Tuple<short, short>(0,1)), new Tuple<short,short>(0,1) },
    { new Tuple<char, Tuple<short, short>>('-', new Tuple<short, short>(0,-1)), new Tuple<short,short>(0,-1) },
    { new Tuple<char, Tuple<short, short>>('L', new Tuple<short, short>(1,0)), new Tuple<short,short>(0,1) },
    { new Tuple<char, Tuple<short, short>>('L', new Tuple<short, short>(0,-1)), new Tuple<short,short>(-1,0) },
    { new Tuple<char, Tuple<short, short>>('J', new Tuple<short, short>(1,0)), new Tuple<short,short>(0,-1) },
    { new Tuple<char, Tuple<short, short>>('J', new Tuple<short, short>(0,1)), new Tuple<short,short>(-1,0) },
    { new Tuple<char, Tuple<short, short>>('7', new Tuple<short, short>(-1,0)), new Tuple<short,short>(0,-1) },
    { new Tuple<char, Tuple<short, short>>('7', new Tuple<short, short>(0,1)), new Tuple<short,short>(1,0) },
    { new Tuple<char, Tuple<short, short>>('F', new Tuple<short, short>(-1,0)), new Tuple<short,short>(0,1) },
    { new Tuple<char, Tuple<short, short>>('F', new Tuple<short, short>(0,-1)), new Tuple<short,short>(1,0) },
};

int length = 0;
Tuple<short,short> dir = new Tuple<short, short>(1,0);
Tuple<int,int> pos = startPos;
char pipe = '7';
while (pipe != 'S')
{
    pos = new Tuple<int,int>(pos.Item1 + dir.Item1, pos.Item2 + dir.Item2);
    pipe = map[pos.Item1][pos.Item2];
    Tuple<char, Tuple<short, short>> key = new Tuple<char, Tuple<short, short>>(pipe, dir);
    if (pipeMap.ContainsKey(key))
    {
        dir = pipeMap[key];
    }
    length++;
}
Console.WriteLine(length/2);