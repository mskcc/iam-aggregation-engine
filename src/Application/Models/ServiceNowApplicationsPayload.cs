namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

public class ServiceNowApplicationsPayload
{   
    /// <summary>
    /// The name of the application to filter by.
    /// </summary>
    public string ApplicationNameFilter { get; set; } = string.Empty;
}