// Day 14
// Part 1
string[] lines = File.ReadAllLines("Part1/input.txt");

List<string> columns = new List<string>();
for (int i = 0; i < lines[0].Length; i++)
{
    string column = "";
    foreach (string row in lines)
    {
        column += row[i];
    }
    columns.Add(column);
    //Console.WriteLine(column);
}

int sum = 0;
foreach (string column in columns)
{
    int dotCount = 0;
    for (int i = 0; i < column.Length; i++)
    {
        if (column[i] == 'O')
        {
            //Console.WriteLine(column.Length - (i - dotCount));
            sum += column.Length - (i - dotCount);
        }
        if (column[i] == '#')
        {
            dotCount = 0;
        }
        if (column[i] == '.')
        {
            dotCount++;
        }
    }
    //Console.WriteLine("-------------");
}
Console.WriteLine(sum);