// Day 2
// Puzzel 1

string[] lines = File.ReadAllLines("Puzzel1/input.txt");

int maxRed = 12;
int maxGreen = 13;
int maxBlue = 14;

int sum = 0;
foreach (string line in lines)
{
    int gameId = int.Parse(line.Split(":")[0].Split(" ")[1]);
    
    bool impossible = false;
    string[] cubesPulls = line.Split(":")[1].Split(";");
    foreach (string pull in cubesPulls)
    {
        foreach (string cubes in pull.Split(","))
        {
            int amount = int.Parse(cubes.Trim().Split(" ")[0]);
            string color = cubes.Trim().Split(" ")[1];
            if (color == "red" && amount > maxRed || color == "green" && amount > maxGreen || color == "blue" && amount > maxBlue)
            {
                impossible = true;
            }
        }
    }

    sum += impossible == false ? gameId : 0;
}

Console.WriteLine(sum);