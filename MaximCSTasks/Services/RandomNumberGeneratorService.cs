using MaximCSTasks.RandomNumberGenerator;

namespace MaximCSTasks.Services;

public class RandomNumberGeneratorService
{
    private static readonly RandomNumberGeneratorService Instance;

    static RandomNumberGeneratorService()
    {
        Instance = new RandomNumberGeneratorService();
    }
    
    private RandomNumberGeneratorService() { }
    
    public static RandomNumberGeneratorService GetInstance => Instance;
    

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