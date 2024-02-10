namespace CodingTracker.Exceptions;

using System;

public class CodingTrackerException : Exception
{
    // Additional properties can be added as needed
    public int StatusCode { get; set; }

    // Constructor without parameters
    public CodingTrackerException() : base()
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
    public CodingTrackerException(string message, Exception innerException, int statusCode) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    // Additional constructors and methods can be added to meet the needs of your application
}
