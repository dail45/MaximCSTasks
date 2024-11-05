using MaximCSTasks;

Console.Write("Enter your line: ");
var line = Console.ReadLine();

if (string.IsNullOrEmpty(line))
{
    Console.WriteLine("Your line is null");
    Environment.Exit(0);
}

var unexpectedChars = Utils.CheckOnlyEnglishChars(line);
if (unexpectedChars.Count > 0)
{
    Console.WriteLine("Your line contains unexpected characters: " + string.Join(", ", unexpectedChars.Select(ch => $"\"{ch}\"")));
}
else
{
    var result = Utils.EvenOrOddReverseTextFunc(line);
    var charsCount = Utils.CalcCountChars(result);
    var vowelLargestSubString = Utils.FindMaxLengthSubStringOfVowelChars(result);
    Console.WriteLine($"Result: \"{line}\" -> \"{result}\"");
    Console.WriteLine("Counts of chars in result line:");
    Console.WriteLine(string.Join("\n", charsCount.Select(pair => $"\"{pair.Key}\": {pair.Value}")));
    Console.WriteLine($"Largest vowel substring: \"{vowelLargestSubString}\"");
}