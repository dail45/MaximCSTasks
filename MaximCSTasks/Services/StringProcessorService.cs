﻿using System.Collections.Immutable;
using System.Text.Json;
using MaximCSTasks.Utils;
using MaximCSTasks.Models;
using MaximCSTasks.Sorters;
using Microsoft.VisualBasic;

namespace MaximCSTasks.Services;

public class StringProcessorService : IStringProcessorService
{
    private IRandomNumberGeneratorService _randomNumberGeneratorService;
    private readonly IConfiguration _configuration;
    public StringProcessorService(IRandomNumberGeneratorService randomNumberGeneratorService, IConfiguration configuration)
    {
        _randomNumberGeneratorService = randomNumberGeneratorService;
        _configuration = configuration;
    }

    public StringProcessorResult ProcessLine(string line, string sorterType)
    {
        if (string.IsNullOrEmpty(line))
        {
            return StringProcessorResult.Empty;
        }

        if (_configuration.GetSection("AppSettings:Settings:BlackList").Get<List<string>>().Contains(line))
        {
            return StringProcessorResult.BlackList;
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
        var unexpectedChars = Utilites.CheckOnlyEnglishChars(line);
        if (unexpectedChars.Count > 0)
        {
            return StringProcessorResult.UnexpectedChars(unexpectedChars);
        }

        var result = Utilites.EvenOrOddReverseTextFunc(line);
        var charsCount = Utilites.CalcCountChars(result);
        var vowelLargestSubString = Utilites.FindMaxLengthSubStringOfVowelChars(result);
        var sortedResultLine = sorter.SortString(result);
        var resultLineWithoutRandomChar = Utilites.RemoveRandomCharInString(result, _randomNumberGeneratorService);

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