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
    Console.WriteLine($"Result: \"{line}\" -> \"{result}\"");
}