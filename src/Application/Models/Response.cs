namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Represents an HTTP response.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Response<T>
{
    /// <summary>
    /// Creates an instance of <see cref="Response"/>
    /// </summary>
    public Response()
    {
        Succeeded = true;
        Message = string.Empty;
        Errors = null;
    }

    /// <summary>
    /// Creates an instance of <see cref="Response"/>
    /// </summary>
    /// <param name="data"></param>
    public Response(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        Errors = null;
        Data = data;
    }

    /// <summary>
    /// Represents the payload data for the response.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Represents the status of retrieving payload data.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Represents any errors that occured during retrival of payload data.
    /// </summary>
    public string[]? Errors { get; set; }
    
    /// <summary>
    /// Represents a message for the response.
    /// </summary>
    public string Message { get; set; }
}