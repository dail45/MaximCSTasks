using MaximCSTasks.Apis;
namespace MaximCSTasks.RandomNumberGenerator;

public class RemoteRandomNumberGenerator: RandomNumberGeneratorInterface
{
    private readonly RandomAPI _randomApi;

    public RemoteRandomNumberGenerator(IConfiguration configuration)
    {
        _randomApi = new RandomAPI(configuration);
    }
    
    public int GetRandomNumber(int min, int max)
    {
        var task = _randomApi.GetAsyncRandomNumber(min, max);
        var result = task.GetAwaiter().GetResult();
        return result;
    }
}