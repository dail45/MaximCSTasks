using MaximCSTasks.RandomNumberGenerator;

namespace MaximCSTasks.Services;

public class RandomNumberGeneratorService
{
    private static readonly RandomNumberGeneratorService _instance;

    static RandomNumberGeneratorService()
    {
        _instance = new RandomNumberGeneratorService();
    }
    
    private RandomNumberGeneratorService() { }
    
    public static RandomNumberGeneratorService Instance => _instance;
    

    private readonly RemoteRandomNumberGenerator _remoteRandomNumberGenerator = new RemoteRandomNumberGenerator();
    private readonly LocalRandomNumberGenerator _localRandomNumberGenerator = new LocalRandomNumberGenerator();
    
    public int GetRandomNumber(int min, int max)
    {
        try
        {
            return _remoteRandomNumberGenerator.GetRandomNumber(min, max);
        }
        catch
        {
            return _localRandomNumberGenerator.GetRandomNumber(min, max);
        }
    }
}