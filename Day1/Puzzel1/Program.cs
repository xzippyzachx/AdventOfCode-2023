// Day 1
// Puzzel 1

string[] lines = File.ReadAllLines("Puzzel1/input.txt");

int total = 0;
foreach (string line in lines)
{
    char[] lineChars = line.ToCharArray();
    char frontNum = '\0';
    char endNum = '\0';
    for(int i = 0; i < lineChars.Length; i++)
    {
        //Console.WriteLine(i);
        //Console.WriteLine(lineChars.Length - i - 1);
        
        char frontPointer = lineChars[i];
        char endPointer = lineChars[lineChars.Length - i - 1];        
        if (Char.IsNumber(frontPointer) && frontNum == '\0')
        {
            frontNum = frontPointer;
            //Console.WriteLine("Found Front: " + frontNum);
        }
        if (Char.IsNumber(endPointer) && endNum == '\0')
        {
            endNum = endPointer;
            //Console.WriteLine("Found End: " + endNum);
        }
        if (i == (lineChars.Length / 2) - 1 && frontNum == '\0' && endNum == '\0') // Center
        {
            if (lineChars.Length % 2 == 1)
            {
                frontNum = lineChars[i + 1];
                endNum = frontNum;
            }
            else if (Char.IsNumber(frontPointer))
            {
                frontNum = frontPointer;
                endNum = frontPointer;
            }
            else
            {
                frontNum = endPointer;
                endNum = endPointer;
            }
            //Console.WriteLine("Found Front: " + frontNum);
            //Console.WriteLine("Found End: " + endNum);
        }
        if (frontNum != '\0' && endNum != '\0')
        {
            // Console.WriteLine(frontNum + "" + endNum);
            total += int.Parse(frontNum + "" + endNum);
            break;
        }
        // Console.WriteLine("-----");
    }
}

Console.WriteLine(total);