using Serilog;

namespace SharedKernel.Resources.Logging;


public record LannisterLogger
{
    public static LannisterLogger CreateInstance()
    {
        return new LannisterLogger();
    }

    private static string? ErrorCode { get; set; }

    /// <summary>
    /// Log Any Error encountered
    /// </summary>
    /// <param name="errorCode">Accept string type of error code</param>
    /// <param name="message">Accept string type of error message</param>
    public static void LogError(string errorCode, string message)
    {
        Log.Fatal("Error code {@errorcode} ::: Message {@message} ",errorCode, message);
    }

    /// <summary>
    /// Log Anything you want with this simple method
    /// </summary>
    /// <param name="message">Accept a string message</param>
    public static void LogInfo(string message)
    {
        Log.Information("Message :::  {@message} :::", message);
    }
}