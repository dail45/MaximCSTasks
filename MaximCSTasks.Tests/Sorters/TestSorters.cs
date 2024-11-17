using MaximCSTasks.Sorters;

namespace MaximCSTasks.Tests.Sorters;

[TestFixtureSource(nameof(sorters))]
public class TestSorters
{
    static readonly StringSorterInterface[] sorters = new StringSorterInterface[]
    {
        new StringQuickSorter(),
        new StringTreeSorter()
    };

    private StringSorterInterface _sorter;

    public TestSorters(StringSorterInterface sorter)
    {
        _sorter = sorter;
    }

    [Test]
    [TestCase("abfdsfsd")]
    [TestCase("helloworld")]
    public void test_sorters(string line)
    {
        var result = _sorter.SortString(line);
        Assert.That(result, Is.EqualTo(string.Join("", line.OrderBy(x => x))));
    }

    [Test]
    [TestCase("abcdefg")]
    [TestCase("cdefg")]
    public void test_already_sorted_line(string line)
    {
        var result = _sorter.SortString(line);
        Assert.That(result, Is.EqualTo(line));
    }
}