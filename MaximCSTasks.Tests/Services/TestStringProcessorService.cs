using MaximCSTasks.Models;
using MaximCSTasks.Services;
using MaximCSTasks.Sorters;
using Microsoft.Extensions.Configuration;
using Moq;

namespace MaximCSTasks.Tests.Services;

[TestFixture]
public class TestStringProcessorService
{
    private Mock<IRandomNumberGeneratorService> _randomNumberGeneratorServiceMock;
    private StringProcessorService _stringProcessorService;

    [SetUp]
    public void Setup()
    {
        _randomNumberGeneratorServiceMock = new Mock<IRandomNumberGeneratorService>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false).Build();
        _stringProcessorService = new StringProcessorService(_randomNumberGeneratorServiceMock.Object, configuration);
        _randomNumberGeneratorServiceMock.SetReturnsDefault(1);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void test_empty_line(string line)
    {
        var result = _stringProcessorService.ProcessLine(line, "q");
        Assert.That(result, Is.EqualTo(StringProcessorResult.Empty));
    }

    [Test]
    [TestCase("aaaaaa")]
    [TestCase("abc")]
    [TestCase("fuf")]
    public void test_is_blacklist(string line)
    {
        var result = _stringProcessorService.ProcessLine(line, "q");
        Assert.That(result, Is.EqualTo(StringProcessorResult.BlackList));
    }

    [Test]
    [TestCase("qt")]
    [TestCase("tq")]
    [TestCase("w")]
    [TestCase("e")]
    [TestCase("")]
    [TestCase(null)]
    public void test_invalid_sorter_type(string sorterType)
    {
        var result = _stringProcessorService.ProcessLine("abcdefg", sorterType);
        Assert.That(result, Is.EqualTo(StringProcessorResult.InvalidSorter));
    }

    [Test]
    [TestCase("123456")]
    [TestCase("fsdgiujdfgo2")]
    [TestCase("fgsduoghcс")]
    [TestCase("Hello, world!")]
    [TestCase("Привет, мир!")]
    public void test_unexpected_chars(string line)
    {
        var result = _stringProcessorService.ProcessLine(line, "q");
        Assert.That(result.Error, Does.StartWith("Your line contains unexpected characters: "));
    }

    [Test]
    [TestCase("abcdefghijk", "q")]
    [TestCase("abcdefghijk", "t")]
    public void test_normal(string line, string sorterType)
    {
        var result = _stringProcessorService.ProcessLine(line, sorterType);
        var resultLine = "kjihgfedcbaabcdefghijk";
        var charsCount = new Dictionary<char, int> {
            {'a', 2},
            {'b', 2},
            {'c', 2},
            {'d', 2},
            {'e', 2},
            {'f', 2},
            {'g', 2},
            {'h', 2},
            {'i', 2},
            {'j', 2},
            {'k', 2}
        };
        var vowelLargestSubString = "ihgfedcbaabcdefghi";
        var sortedResultLine = "aabbccddeeffgghhiijjkk";
        var resultLineWithoutRandomChar = "kihgfedcbaabcdefghijk";
        Assert.Multiple(() =>
        {
            Assert.That(result.Data.Result, Is.EqualTo(resultLine));
            Assert.That(result.Data.CharsCount, Is.EqualTo(charsCount));
            Assert.That(result.Data.VowelLargestSubString, Is.EqualTo(vowelLargestSubString));
            Assert.That(result.Data.SortedResultLine, Is.EqualTo(sortedResultLine));
            Assert.That(result.Data.ResultLineWithoutRandomChar, Is.EqualTo(resultLineWithoutRandomChar));
        });
    }
}