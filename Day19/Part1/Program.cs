// Day 19
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

List<Part> parts = new List<Part>();
Dictionary<string, List<Rule>> workflows = new Dictionary<string, List<Rule>>();

bool partFlag = false;
foreach (string line in lines)
{
    if (line == "")
    {
        partFlag = true;
        continue;
    }

    if (partFlag)
    {
        string[] split = line.Split(",");
        int x = int.Parse(split[0].Split("=")[1]);
        int m = int.Parse(split[1].Split("=")[1]);
        int a = int.Parse(split[2].Split("=")[1]);
        int s = int.Parse(split[3].Split("=")[1].Replace("}", ""));
        parts.Add(new Part(x,m,a,s));
        //Console.WriteLine(new Part(x,m,a,s));
    }
    else
    {
        string[] splitKey = line.Split("{");
        string[] splitComma = splitKey[1].Split(",");

        string key = splitKey[0];

        List<Rule> rules = new List<Rule>();
        for (int i = 0; i < splitComma.Length; i++)
        {
            if (splitComma[i].Contains(":"))
            {
                char rating = splitComma[i][0];
                char symbol = splitComma[i][1];
                int value = int.Parse(splitComma[i].Split(symbol)[1].Split(":")[0]);
                string result = splitComma[i].Split(symbol)[1].Split(":")[1].Replace("}", "");
                rules.Add(new Rule(rating,symbol,value,result));
                
            }
            else
            {
                rules.Add(new Rule(' ',' ',0,splitComma[i].Replace("}", "")));
            }
        }
        workflows.Add(key, rules);

        //Console.WriteLine(String.Join(",", workflows[key]));
    }
}

bool CheckRule(char rating, char symbol, int value, Part part)
{
    int left = 0;
    switch (rating)
    {
        case 'x':
            left = part.x;
            break;
        case 'm':
            left = part.m;
            break;
        case 'a':
            left = part.a;
            break;
        case 's':
            left = part.s;
            break;
    }

    if (symbol == '>' && left > value)
    {
        return true;
    }
    else if (symbol == '<' && left < value)
    {
        return true;
    }

    return false;
}

bool CheckRules(List<Rule> workflow, Part part)
{
    foreach (Rule rule in workflow)
    {
        if (rule.rating == ' ')
        {
            if (rule.result == "R")
            {
                return false;
            }
            else if (rule.result == "A")
            {
                return true;
            }
            else
            {
                return CheckRules(workflows[rule.result], part);
            }
        }
        else if (CheckRule(rule.rating, rule.symbol, rule.value, part))
        {
            if (rule.result == "R")
            {
                return false;
            }
            else if (rule.result == "A")
            {
                return true;
            }
            else
            {
                return CheckRules(workflows[rule.result], part);
            }
        }
    }

    return false;
}

int sum = 0;
foreach (Part part in parts)
{
    List<Rule> workflow = workflows["in"];
    
    if (CheckRules(workflow, part))
    {
        sum += part.x + part.m + part.a + part.s;
    }
}
Console.WriteLine(sum);

record Part(int x, int m, int a, int s);
record Rule(char rating, char symbol, int value, string result);