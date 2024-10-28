using MaximCSTasks;

Console.Write("Enter your line: ");
var line = Console.ReadLine();

if (string.IsNullOrEmpty(line)) {
    Console.WriteLine("Your line is null");
    Environment.Exit(0);
}

var result = Utils.EvenOrOddReverseTextFunc(line);
Console.WriteLine($"Result: \"{line}\" -> \"{result}\"");