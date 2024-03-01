namespace CodingTracker.Exceptions;

public class CodingTrackerException : Exception
{
    // Constructor without parameters
    public CodingTrackerException()
    {
    }

    // Constructor with just the message parameter
    public CodingTrackerException(string message) : base(message)
    {
    }

    // Constructor with both message and inner exception parameters
    public CodingTrackerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    // Constructor with message, inner exception, and additional custom property (e.g., StatusCode)
    public CodingTrackerException(string message, Exception innerException, int statusCode) : base(message,
        innerException)
    {
        StatusCode = statusCode;
    }

    // Additional properties can be added as needed
    public int StatusCode { get; set; }

    // Additional constructors and methods can be added to meet the needs of your application
}