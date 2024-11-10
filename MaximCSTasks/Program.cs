using System.Diagnostics;
using MaximCSTasks;
using MaximCSTasks.Sorters;

Console.Write("Enter your line: ");
var inputLine = Console.ReadLine();

if (string.IsNullOrEmpty(inputLine))
{
    Console.WriteLine("Your line is null");
    Environment.Exit(0);
}

Console.Write("Choose sorter for your line (q - QuickSorter, t - TreeSorter): ");
var sorterLine = Console.ReadLine();
if (string.IsNullOrEmpty(sorterLine) || sorterLine.Length > 1 || !"qt".Contains(sorterLine[0]))
{
    Console.WriteLine("Your choose is invalid");
    Environment.Exit(0);
}

StringSorterInterface sorter = sorterLine switch
{
    "q" => new StringQuickSorter(),
    "t" => new StringTreeSorter()
};

var unexpectedChars = Utils.CheckOnlyEnglishChars(inputLine);
if (unexpectedChars.Count > 0)
{
    Console.WriteLine("Your line contains unexpected characters: " + string.Join(", ", unexpectedChars.Select(ch => $"\"{ch}\"")));
}
else
{
    var result = Utils.EvenOrOddReverseTextFunc(inputLine);
    var charsCount = Utils.CalcCountChars(result);
    var vowelLargestSubString = Utils.FindMaxLengthSubStringOfVowelChars(result);
    var sortedResultLine = sorter.SortString(result);
    var resultLineWithoutRandomChar = Utils.RemoveRandomCharInString(result);
    Console.WriteLine($"Result: \"{inputLine}\" -> \"{result}\"");
    Console.WriteLine("Counts of chars in result line:");
    Console.WriteLine(string.Join("\n", charsCount.Select(pair => $"\"{pair.Key}\": {pair.Value}")));
    Console.WriteLine($"Largest vowel substring: \"{vowelLargestSubString}\"");
    Console.WriteLine($"Sorted result line: \"{sortedResultLine}\"");
    Console.WriteLine($"Result line without random char: \"{resultLineWithoutRandomChar}\"");
}