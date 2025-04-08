namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;

/// <summary>
/// Colleague definition
/// </summary>
public interface IColleague
{
    /// <summary>
    /// Receive notification from <see cref="IMediator"/>
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <param name="payloadData"></param>
    Task<object> Receive(string serviceKey, object? payloadData);

    /// <summary>
    /// Send notification to <see cref="IMediator"/>
    /// </summary>
    /// <param name="serviceKey"></param>
    /// <param name="payloadData"></param>
     Task<object> Execute(string serviceKey, object? payloadData);
}
