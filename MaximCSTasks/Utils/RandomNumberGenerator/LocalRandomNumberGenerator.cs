namespace MaximCSTasks.Utils.RandomNumberGenerator;

public class LocalRandomNumberGenerator: RandomNumberGeneratorInterface
{
    public int GetRandomNumber(int min, int max)
    {
        return Random.Shared.Next(min, max);
    }
}