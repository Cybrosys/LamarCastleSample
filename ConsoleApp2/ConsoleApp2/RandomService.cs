namespace ConsoleApp2;

public class RandomService : IRandomService
{
    private static Random _random = new Random();

    public void OutputRandom()
    {
        Console.WriteLine(_random.Next());
    }
}
