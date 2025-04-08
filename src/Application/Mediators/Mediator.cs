using Microsoft.Extensions.DependencyInjection;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Mediators;

/// <summary>
/// Concrete implementation of <see cref="IMediator"/>
/// </summary>
public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Creates an instance of <see cref="Mediator"/>
    /// </summary>
    public Mediator(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Notify Mediator
    /// </summary>
    public async Task<object> Notify(object sender, string serviceKey, object? payloadData = null)
    {
        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(serviceKey);
        
        using (var scope = _serviceProvider.CreateScope())
        {
            var colleagues = scope.ServiceProvider.GetServices<IColleague>().ToList();

            foreach (string key in ServiceKeys.GetKeys())
            {
                var keyedService = scope.ServiceProvider.GetKeyedService<IColleague>(key);
                
                if (keyedService is null || serviceKey != key)
                {
                    continue;
                }

                return await keyedService.Receive(serviceKey, payloadData);
            }

            foreach (var colleague in colleagues)
            {
                if (colleague == sender)
                {
                    continue;
                }

                return await colleague.Receive(serviceKey, payloadData);
            }
        }

        return string.Empty;
    }
}