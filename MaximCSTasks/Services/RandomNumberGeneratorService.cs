using MaximCSTasks.Utils.RandomNumberGenerator;

namespace MaximCSTasks.Services;

public class RandomNumberGeneratorService : IRandomNumberGeneratorService
{
    public RandomNumberGeneratorService(IConfiguration configuration)
    {
        _remoteRandomNumberGenerator = new RemoteRandomNumberGenerator(configuration);
    }

    private readonly RemoteRandomNumberGenerator _remoteRandomNumberGenerator;
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