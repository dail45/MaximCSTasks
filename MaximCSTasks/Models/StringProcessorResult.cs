using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace MaximCSTasks.Models;

public record StringProcessorData(
    string Result,
    ImmutableDictionary<char, int> CharsCount,
    string VowelLargestSubString,
    string SortedResultLine,
    string ResultLineWithoutRandomChar
)
{
    [JsonIgnore]
    public static StringProcessorData Empty => new(
        Result: "",
        CharsCount: ImmutableDictionary<char, int>.Empty,
        VowelLargestSubString: "",
        SortedResultLine: "",
        ResultLineWithoutRandomChar: "");
}

public record StringProcessorResult(
    StringProcessorData Data,
    string Error
)
{
    private static StringProcessorResult ErrorMsg(string error)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));
        return new StringProcessorResult(Data: StringProcessorData.Empty, Error: error);
    }
    
    [JsonIgnore]
    public static StringProcessorResult Empty => ErrorMsg("Your line is null or empty.");
    
    [JsonIgnore]
    public static StringProcessorResult InvalidSorter => ErrorMsg("Sorter type is invalid.");
    
    [JsonIgnore]
    public static StringProcessorResult BlackList => ErrorMsg("Your line is in a blacklist.");

    public static StringProcessorResult UnexpectedChars(List<char> unexpectedChars)
    {
        return ErrorMsg("Your line contains unexpected characters: " +
                        string.Join(", ", unexpectedChars.Select(ch => $"\"{ch}\"")));
    }
}