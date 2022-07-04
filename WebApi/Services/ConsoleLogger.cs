namespace WebApi.Services;

public class ConsoleLogger : ILoggerService
{
    void ILoggerService.Write(string message)
    {
        Console.WriteLine("[ConsoleLogger] - " + message);
    }
}