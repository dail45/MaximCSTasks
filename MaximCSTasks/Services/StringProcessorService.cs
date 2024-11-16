using System.Collections.Immutable;
using System.Text.Json;
using MaximCSTasks.Models;
using MaximCSTasks.Sorters;
using Microsoft.VisualBasic;

namespace MaximCSTasks.Services;

public class StringProcessorService
{
    private static readonly StringProcessorService _instance;

    static StringProcessorService()
    {
        _instance = new StringProcessorService();
    }

    private StringProcessorService()
    {
    }

    public static StringProcessorService Instance => _instance;

    public StringProcessorResult ProcessLine(string line, string sorterType)
    {
        if (string.IsNullOrEmpty(line))
        {
            return StringProcessorResult.Empty;
        }

        if (string.IsNullOrEmpty(sorterType) || sorterType.Length > 1 || !new[] { "q", "t" }.Contains(sorterType))
        {
            return StringProcessorResult.InvalidSorter;
        }

        StringSorterInterface sorter = sorterType switch
        {
            "q" => new StringQuickSorter(),
            "t" => new StringTreeSorter()
        };
        var unexpectedChars = Utils.CheckOnlyEnglishChars(line);
        if (unexpectedChars.Count > 0)
        {
            return StringProcessorResult.UnexpectedChars(unexpectedChars);
        }

        var result = Utils.EvenOrOddReverseTextFunc(line);
        var charsCount = Utils.CalcCountChars(result);
        var vowelLargestSubString = Utils.FindMaxLengthSubStringOfVowelChars(result);
        var sortedResultLine = sorter.SortString(result);
        var resultLineWithoutRandomChar = Utils.RemoveRandomCharInString(result);

        var resultData = new StringProcessorData(
            result, 
            charsCount.ToImmutableDictionary(),
            vowelLargestSubString,
            sortedResultLine,
            resultLineWithoutRandomChar);
        return new StringProcessorResult(
            resultData,
            ""
        );
    }
}