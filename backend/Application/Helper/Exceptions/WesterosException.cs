using Serilog;

namespace Application.Helper.Exceptions;

public class WesterosException: Exception
{
    private string? ErrorCode { get; set; }

    public WesterosException(string message) : base(message)
    { }

    public  WesterosException(string message, string? errorCode) : base(message)
    {
        Log.Fatal("Error code {@errorcode} ::: Message {@message} ",errorCode, message);
        ErrorCode = errorCode;
        //TODO: Save error to DB
    }
    
}