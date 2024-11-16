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
    [JsonIgnore]
    public static StringProcessorResult Empty => new(
        Data: StringProcessorData.Empty,
        Error: "Your line is null or empty."
    );
    
    [JsonIgnore]
    public static StringProcessorResult InvalidSorter => new(
        Data: StringProcessorData.Empty,
        Error: "Sorter type is invalid."
    );

    public static StringProcessorResult UnexpectedChars(List<char> unexpectedChars)
    {
        return new StringProcessorResult(
            Data: StringProcessorData.Empty,
            Error: "Your line contains unexpected characters: " +
                   string.Join(", ", unexpectedChars.Select(ch => $"\"{ch}\""))
        );
    }
}