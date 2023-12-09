// Day 2
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

int sum = 0;
foreach (string line in lines)
{
    int minRed = 0;
    int minGreen = 0;
    int minBlue = 0;

    string[] cubesPulls = line.Split(":")[1].Split(";");
    foreach (string pull in cubesPulls)
    {
        foreach (string cubes in pull.Split(","))
        {
            int amount = int.Parse(cubes.Trim().Split(" ")[0]);
            string color = cubes.Trim().Split(" ")[1];
            
            minRed = color == "red" && amount > minRed ? amount : minRed;
            minGreen = color == "green" && amount > minGreen ? amount : minGreen;
            minBlue = color == "blue" && amount > minBlue ? amount : minBlue;
        }
    }
    
    sum += minRed * minGreen * minBlue;
}

Console.WriteLine(sum);
