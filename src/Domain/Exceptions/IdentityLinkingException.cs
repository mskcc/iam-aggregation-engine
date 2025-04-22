namespace Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;

/// <summary>
/// Represents an exception that indicates a conflict has occurred.
/// </summary>
[Serializable]
public class IdentityLinkingException : Exception
{
    /// <summary>
    /// Gets or sets the error code or identifier.
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public new string Message { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityLinkingException"/> class with a specified error code and message.
    /// </summary>
    /// <param name="error">The error code or identifier.</param>
    /// <param name="message">The error message.</param>
    public IdentityLinkingException(string error, string message) : base(message)
    {
        Error = error;
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityLinkingException"/> class with a specified error code and message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public IdentityLinkingException(string message) : base(message)
    {
        Error = string.Empty;
        Message = message;
    }
}