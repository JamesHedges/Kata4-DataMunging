namespace Kata04.Part3
{
    public interface IKata04Config
    {
        string KataDataBaseAddress { get; }
        string WeatherFilePath { get; }
        string FootballFilePath { get; }
    }
}