namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;

/// <summary>
/// CQRS Mediator definition
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Notify Mediator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="message"></param>
    /// <param name="payloadData"</param>
    Task<object> Notify(object sender, string message, object payloadData);
}