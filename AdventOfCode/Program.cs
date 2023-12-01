namespace com.github.horizon.adventofcode;

using System;
using System.Net.Http.Headers;
using System.Net.Http;
using HttpClient = HttpClient;

public class Program
{
    public static void Main(string[] args)
    {
        //Read file
        var input = File.ReadAllText(@"C:\Users\danielf\RiderProjects\AdventOfCode\AdventOfCode\AdventOfCode01.txt");
        var inputArray = input.Split("\n");

        var sum = 0L;
        var lineNum = 0;

        foreach (var line in inputArray)
        {
            lineNum++;
            var firstNumber = -1;
            var secondNumber = firstNumber;

            var charArray = line.ToCharArray();
            var values = new List<int>();

            var currentString = "";
            for (var i = 0; i < charArray.Length; i++)
            {
                if (char.IsNumber(charArray[i]))
                {
                    var occurrences = TryParse(currentString);
                    if (occurrences.Length != 0) values.AddRange(occurrences);

                    currentString = "";

                    values.Add(
                        int.Parse(
                            charArray[i]
                                .ToString()));
                    continue;
                }

                currentString += char.ToLower(charArray[i]);
                if (i == charArray.Length - 1)
                {
                    var occurrences = TryParse(currentString);
                    if (occurrences.Length == 0) continue;

                    values.AddRange(occurrences);
                }
            }

            firstNumber = values[0];
            secondNumber = values[^1];

            if (firstNumber != -1 && secondNumber != -1)
            {
                sum += int.Parse(firstNumber.ToString() + secondNumber.ToString());
                Console.WriteLine("Line " + lineNum + ": " + firstNumber + " + " + secondNumber + " = " + firstNumber + secondNumber + " | Sum: " + sum);
            }
            else
                Console.WriteLine("Error on line " + lineNum);
        }

        Console.WriteLine("Final sum: " + sum);
    }

    private static int[] TryParse(string s)
    {
        s = s.Trim();
        var values = new List<int>();
        var charArray = s.ToCharArray();
        for (var i = 0; i < charArray.Length; i++)
        {
            for (var j = i; j < charArray.Length; j++)
            {
                var remainingString = s.Substring(i, j - i + 1);
                var number = TryParseNumber(remainingString);
                if (number == -1) continue;
                values.Add(number);
                i = j;
                break;
            }
        }

        return values.ToArray();
    }

    private static int TryParseNumber(string s)
    {
        return s switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => -1
        };
    }
}