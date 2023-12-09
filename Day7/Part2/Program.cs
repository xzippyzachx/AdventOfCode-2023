// Day 7
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

Dictionary<char, int> cardValues = new Dictionary<char, int>()
{
    { 'J', 1 },
    { '2', 2 },
    { '3', 3 },
    { '4', 4 },
    { '5', 5 },
    { '6', 6 },
    { '7', 7 },
    { '8', 8 },
    { '9', 9 },
    { 'T', 10 },
    { 'Q', 12 },
    { 'K', 13 },
    { 'A', 14 },
};
List<Tuple<string, int>> hands = new List<Tuple<string, int>>();

int CompareHands(string handOne, string handTwo)
{
    if (handOne == handTwo)
    {
        return 0;
    }

    HandType handTypeOne = GetHandType(handOne.ToCharArray());
    HandType handTypeTwo = GetHandType(handTwo.ToCharArray());

    if (handTypeOne > handTypeTwo)
    {
        return 1;
    }
    else if (handTypeOne < handTypeTwo)
    {
        return -1;
    }
    else
    {
        for (int i = 0; i < 5; i++)
        {
            if (cardValues[handOne[i]] > cardValues[handTwo[i]])
            {
                return 1;
            }
            else if (cardValues[handOne[i]] < cardValues[handTwo[i]])
            {
                return -1;
            }
        }
    }
    return 0;
}

HandType GetHandType (char[] hand)
{
    Dictionary<char, int> cardHandValues = new Dictionary<char, int>();
    foreach (char c in hand)
    {
        if (cardHandValues.ContainsKey(c))
        {
            cardHandValues[c]++;
        }
        else
        {
            cardHandValues.Add(c, 1);
        }
    }

    if (cardHandValues.ContainsKey('J') && cardHandValues.Count > 1)
    {
        int amount = cardHandValues['J'];
        cardHandValues.Remove('J');
        char highestCard = 'J';
        int highestValue = 0;
        foreach (char c in cardHandValues.Keys)
        {
            if (cardHandValues[c] > highestValue)
            {
                highestValue = cardHandValues[c];
                highestCard = c;
            }
        }
        cardHandValues[highestCard] += amount;
    }

    HandType type = HandType.H;
    foreach (int value in cardHandValues.Values)
    {
        if (value == 5)
        {
            type = HandType.FIVEOAK;
            break;
        }
        else if (value == 4)
        {
            type = HandType.FOUROAK;
            break;
        }
        else if (value == 3)
        {   
            if (type == HandType.OP)
            {
                type = HandType.FH;
            }
            else
            {
                type = HandType.TOAK;
            }
        }
        else if (value == 2)
        {
            if (type == HandType.OP)
            {
                type = HandType.TP;
            }
            else if (type == HandType.TOAK)
            {
                type = HandType.FH;
            }
            else
            {
                type = HandType.OP;
            }
        }
    }
    return type;
}

foreach (string line in lines)
{
    string[] splitLine = line.Split(" ");
    hands.Add(new Tuple<string, int>(splitLine[0], int.Parse(splitLine[1])));
}

hands.Sort((x, y) => CompareHands(x.Item1, y.Item1));

long total = 0;
for (int i = 0; i < hands.Count; i++)
{
    int rank = i + 1;
    total += hands[i].Item2 * rank;
}

Console.WriteLine(total);

enum HandType
{
    H,
    OP,
    TP,
    TOAK,
    FH,
    FOUROAK,
    FIVEOAK
}