// Day 4
// Part 2

using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("Part2/input.txt");
Dictionary<int, int> cardWins = new Dictionary<int, int>();

int GetWins(int cardId)
{
    int wins = cardWins[cardId];
    int totalWins = wins;
    for (int i = cardId + 1; i <= cardId + wins; i++)
    {
        totalWins += GetWins(i);
    }
    return totalWins;
}

int sum = 0;
foreach (string line in lines)
{
    string cleanLine = Regex.Replace(line, @"\s+", " ");

    int cardId = int.Parse(cleanLine.Split(":")[0].Split(" ")[1]);
    string[] card = cleanLine.Split(":")[1].Split("|");
    string[] nums = card[1].Trim().Split(" ");
    HashSet<string> winNums = new HashSet<string>(card[0].Trim().Split(" "));

    int times = 0;
    foreach (string num in winNums)
    {
        if (nums.Contains(num))
        {
            times++;
        }
    }
    cardWins.Add(cardId, times);
}

foreach (int cardId in cardWins.Keys)
{
    sum += GetWins(cardId) + 1;
}

Console.WriteLine(sum);