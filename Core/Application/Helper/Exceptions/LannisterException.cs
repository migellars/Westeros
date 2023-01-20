using Serilog;

namespace Application.Helper.Exceptions;

public class LannisterException: Exception
{
    private string? ErrorCode { get; set; }

    public LannisterException(string message) : base(message)
    { }

    public  LannisterException(string message, string? errorCode) : base(message)
    {
        Log.Fatal("Error code {@errorcode} ::: Message {@message} ",errorCode, message);
        ErrorCode = errorCode;
        //TODO: Save error to DB
    }
    
}