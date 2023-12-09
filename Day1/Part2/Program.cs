// Day 1
// Part 2

string[] lines = File.ReadAllLines("Part2/input.txt");

Dictionary<string, char> numbers = new Dictionary<string, char>()
{
    {"one", '1'},
    {"two", '2'},
    {"three", '3'},
    {"four", '4'},
    {"five", '5'},
    {"six", '6'},
    {"seven", '7'},
    {"eight", '8'},
    {"nine", '9'}
};

char ContainNum(string input)
{
    if (input.Length < 3)
    {
        return '\0';
    }

    foreach(KeyValuePair<string, char> num in numbers)
    {
        if (input.Contains(num.Key))
        {
            return num.Value;
        }
    }
    return '\0';
}

int total = 0;
foreach (string line in lines)
{
    char[] lineChars = line.ToCharArray();
    char frontNum = '\0';
    char endNum = '\0';

    string frontString = "";
    string endString = "";

    for(int i = 0; i < lineChars.Length; i++)
    {
        char frontPointer = lineChars[i];
        char endPointer = lineChars[lineChars.Length - i - 1];
        if (frontNum == '\0')
        {
            frontString += frontPointer;
            char wordNum = ContainNum(frontString);
            if (wordNum != '\0')
            {
                frontNum = wordNum;
            }
            else if (Char.IsNumber(frontPointer))
            {
                frontNum = frontPointer;
            }
        }
        if (endNum == '\0')
        {
            endString = endPointer + endString;
            char wordNum = ContainNum(endString);
            if (wordNum != '\0')
            {
                endNum = wordNum;
            }
            else if (Char.IsNumber(endPointer))
            {
                endNum = endPointer;
            }
        }
        if (i == (lineChars.Length / 2) - 1 && frontNum == '\0' && endNum == '\0') // Center
        {
            if (lineChars.Length % 2 == 1 && Char.IsNumber(lineChars[i + 1]))
            {
                frontNum = lineChars[i + 1];
                endNum = frontNum;
            }
            else if (Char.IsNumber(frontPointer))
            {
                frontNum = frontPointer;
                endNum = frontPointer;
            }
            else if (Char.IsNumber(endPointer))
            {
                frontNum = endPointer;
                endNum = endPointer;
            }
            else
            {
                char wordNum = ContainNum(endString);
                frontNum = wordNum;
                endNum = wordNum;
            }
        }
        if (frontNum != '\0' && endNum != '\0')
        {
            total += int.Parse(frontNum + "" + endNum);
            // Console.WriteLine("Num: " + frontNum + "" + endNum);
            // Console.WriteLine("-----");
            break;
        }
        // Console.WriteLine("-----");
    }
}

Console.WriteLine(total);