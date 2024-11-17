using MaximCSTasks.Models;

namespace MaximCSTasks.Services;

public interface IStringProcessorService
{
    public StringProcessorResult ProcessLine(string line, string sorterType);
}