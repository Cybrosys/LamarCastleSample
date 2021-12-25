using ConsoleApp2;

public class Program
{
    public static void Main(string[] args)
    {
        var container = new Lamar.Container(x => x.IncludeRegistry<Registry>());

        var randomService = container.GetInstance<IRandomService>();
        randomService.OutputRandom();
    }
}