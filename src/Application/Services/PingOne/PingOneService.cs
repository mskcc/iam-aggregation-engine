using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Constants;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Exceptions;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingOne;

public class PingOneService : IPingOneService
{
    private readonly ILogger<PingOneService> _logger;
    private readonly PingOneOptions _pingOneOptions;
    private readonly HttpClient _pingOneHttpClient;

    /// <summary>
    /// Creates an instance of the <see cref="PingOneService"/> class.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pingOneOptionsMonitor"></param>
    public PingOneService(
        ILogger<PingOneService> logger,
        IOptionsMonitor<PingOneOptions> pingOneOptionsMonitor,
        IHttpClientFactory httpClientFactory
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(pingOneOptionsMonitor);
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        _logger = logger;
        _pingOneOptions = pingOneOptionsMonitor.CurrentValue;
        _pingOneHttpClient = httpClientFactory.CreateClient(HttpClientNames.PingOneClient);
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> CreateLinkedAccountForLdapGateway(string pingOneUserId, string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(pingOneUserId);
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var ldapGatewayLinkingUrl = $"{_pingOneOptions.ApiBaseUrl}/environments/{_pingOneOptions.EnvironmentId}/users/{pingOneUserId}/password";
        var requestBody = new
        {
            id = _pingOneOptions.LdapGatewayId,
            userType = new
            {
                id = _pingOneOptions.LdapGatewayUserTypeId,
            },
            correlationAttributes = new
            {
                sAMAccountName = samAccountName
            }
        };

        var serializedJson = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(serializedJson, Encoding.UTF8, "application/vnd.pingidentity.password.setGateway+json");
        var ldapGatewayLinkingResponse = await _pingOneHttpClient.PutAsync(ldapGatewayLinkingUrl, content);

        if (ldapGatewayLinkingResponse.IsSuccessStatusCode is false)
        {
            _logger.LogError("Failed to link Gateaway account for user {UserId} with status code {StatusCode}", pingOneUserId, ldapGatewayLinkingResponse.StatusCode);
            return new PingOneResponse
            {
                ContainsError = true,
                ErrorMessage = $"Failed to link Entra account for user {pingOneUserId} with status code {ldapGatewayLinkingResponse.StatusCode}",
                ErrorStatusCode = ldapGatewayLinkingResponse.StatusCode
            };
        }

        var deserializedLdapGatewayLinkingResponse = await ldapGatewayLinkingResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedLdapGatewayLinkingResponse);
        
        _logger.LogInformation("Successfully linked LDAP Gateway account for user {UserId} with status code {StatusCode}", pingOneUserId, ldapGatewayLinkingResponse.StatusCode);
        return deserializedLdapGatewayLinkingResponse;
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> CreateLinkedAccountForMicrosoftEntra(string pingOneUserId, string microsoftObjectId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(pingOneUserId);
        ArgumentNullException.ThrowIfNullOrEmpty(microsoftObjectId);

        var microsoftEamLinkingUrl = $"{_pingOneOptions.ApiBaseUrl}/environments/{_pingOneOptions.EnvironmentId}/users/{pingOneUserId}/linkedAccounts";
        var requestBody = new
        {
            identityProvider = new
            {
                id = _pingOneOptions.MicrosoftEntraIdentityProviderId,
            },
            externalId = microsoftObjectId
        };

        var serializedJson = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(serializedJson, Encoding.UTF8, "application/vnd.pingidentity.account.link+json");
        var microsoftEamLinkingResponse = await _pingOneHttpClient.PostAsync(microsoftEamLinkingUrl, content);
        
        if (microsoftEamLinkingResponse.IsSuccessStatusCode is false)
        {
            _logger.LogError("Failed to link Entra account for user {UserId} with status code {StatusCode}", pingOneUserId, microsoftEamLinkingResponse.StatusCode);

            return new PingOneResponse
            {
                ContainsError = true,
                ErrorMessage = $"Failed to link Entra account for user {pingOneUserId} with status code {microsoftEamLinkingResponse.StatusCode}",
                ErrorStatusCode = microsoftEamLinkingResponse.StatusCode
            };
        }

        var deserializedMicrosoftEamLinkingResponse = await microsoftEamLinkingResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedMicrosoftEamLinkingResponse);

        _logger.LogInformation("Successfully linked Microsft account for user {UserId} with status code {StatusCode}", pingOneUserId, microsoftEamLinkingResponse.StatusCode);
        return deserializedMicrosoftEamLinkingResponse;
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> CreateLinkedAccountForPingFederate(string pingOneUserId, string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(pingOneUserId);
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingFederateLinkingUrl = $"{_pingOneOptions.ApiBaseUrl}/environments/{_pingOneOptions.EnvironmentId}/users/{pingOneUserId}/linkedAccounts";
        var requestBody = new
        {
            identityProvider = new
            {
                id = _pingOneOptions.PingFederateIdentityProviderId,
            },
            externalId = samAccountName
        };

        var serializedJson = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(serializedJson, Encoding.UTF8, "application/vnd.pingidentity.account.link+json");
        var pingFederateLinkingResponse = await _pingOneHttpClient.PostAsync(pingFederateLinkingUrl, content);

        if (pingFederateLinkingResponse.IsSuccessStatusCode is false)
        {
            _logger.LogError("Failed to link PingFederate account for user {UserId} with status code {StatusCode}", pingOneUserId, pingFederateLinkingResponse.StatusCode);
            return new PingOneResponse
            {
                ContainsError = true,
                ErrorMessage = $"Failed to link PingFederate account for user {pingOneUserId} with status code {pingFederateLinkingResponse.StatusCode}",
                ErrorStatusCode = pingFederateLinkingResponse.StatusCode
            };
        }

        var deserializedPingFederateLinkingResponse = await pingFederateLinkingResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedPingFederateLinkingResponse);

        _logger.LogInformation("Successfully linked PingFederate account for user {UserId} with status code {StatusCode}", pingOneUserId, pingFederateLinkingResponse.StatusCode);
        return deserializedPingFederateLinkingResponse;
    }

    /// <inheritdoc/>
    public async Task<string> GetPingOneUserIdFromSamAccountName(string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var pingOneUserIdUrl = $"{_pingOneOptions.ApiBaseUrl}/environments/{_pingOneOptions.EnvironmentId}/users?filter=username eq \"{samAccountName}\"";
        var pingOneUserIdResponse = await _pingOneHttpClient.GetAsync(pingOneUserIdUrl);
        pingOneUserIdResponse.EnsureSuccessStatusCode();

        var deserializedPingOneUserIdResponse = await pingOneUserIdResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        var userId = deserializedPingOneUserIdResponse?.Embedded?.Users?.SingleOrDefault()?.Id;
        ArgumentNullException.ThrowIfNullOrEmpty(userId);

        return userId;
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> GetExternalIdps()
    {
        var apiBaseUrl = _pingOneOptions.ApiBaseUrl?.TrimEnd('/');
        var environmentId = _pingOneOptions.EnvironmentId;
        var externalIdpsEndpoint = $"{apiBaseUrl}/environments/{environmentId}/identityProviders";

        var externalIdpsResponse = await _pingOneHttpClient.GetAsync(externalIdpsEndpoint);
        var deserializedExternalIdpResponse = await externalIdpsResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedExternalIdpResponse);

        return deserializedExternalIdpResponse;
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> GetPingOneIdentitiesForProcessing()
    {
        var apiBaseUrl = _pingOneOptions.ApiBaseUrl?.TrimEnd('/');
        var environmentId = _pingOneOptions.EnvironmentId;
        var usersEndpoint = $"{apiBaseUrl}/environments/{environmentId}/users";

        var usersResponse = await _pingOneHttpClient.GetAsync(usersEndpoint);
        var deserializedUsersResponse = await usersResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedUsersResponse);

        return deserializedUsersResponse;
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> GetLinkedIdentityProviderAccounts(string pingOneUserId)
    {
        var apiBaseUrl = _pingOneOptions.ApiBaseUrl?.TrimEnd('/');
        var environmentId = _pingOneOptions.EnvironmentId;
        var linkedAccountsEndpoint = $"{apiBaseUrl}/environments/{environmentId}/users/{pingOneUserId}/linkedAccounts";

        var linkedAccountsResponse = await _pingOneHttpClient.GetAsync(linkedAccountsEndpoint);
        var deserializedLinkedAccountsResponse = await linkedAccountsResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedLinkedAccountsResponse);

        return deserializedLinkedAccountsResponse;
    }

    /// <inheritdoc/>
    public async Task<bool> UnlinkIdentityProivder(string pingOneUserId, string linkedAccountId)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(pingOneUserId);
        ArgumentNullException.ThrowIfNullOrEmpty(linkedAccountId);

        var unlinkIdentityProviderUrl = $"{_pingOneOptions.ApiBaseUrl}/environments/{_pingOneOptions.EnvironmentId}/users/{pingOneUserId}/linkedAccounts/{linkedAccountId}";
        _pingOneHttpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        var unlinkIdentityProviderResponse = await _pingOneHttpClient.DeleteAsync(unlinkIdentityProviderUrl);

        if (unlinkIdentityProviderResponse.StatusCode is not System.Net.HttpStatusCode.NoContent)
        {
            _logger.LogError("Failed to ulink Identity Provider account for user {UserId} with status code {StatusCode}", pingOneUserId, unlinkIdentityProviderResponse.StatusCode);
            throw new IdentityLinkingException($"Failed to unlink Identity Provider accounts for user {pingOneUserId} with status code {unlinkIdentityProviderResponse.StatusCode}");
        }

        _logger.LogInformation("Successfully unlinked Identity Provider account for user {UserId} with status code {StatusCode}", pingOneUserId, unlinkIdentityProviderResponse.StatusCode);

        return true;
    }

    /// <inheritdoc/>
    public async Task<PingOneResponse> UnlinkLdapGatewayIdentity(string pingOneUserId, string samAccountName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(pingOneUserId);
        ArgumentNullException.ThrowIfNullOrEmpty(samAccountName);

        var ldapGatewayLinkingUrl = $"{_pingOneOptions.ApiBaseUrl}/environments/{_pingOneOptions.EnvironmentId}/users/{pingOneUserId}/password";
        var requestBody = new
        {
            id = string.Empty,
            userType = new
            {
                id = string.Empty,
            },
            correlationAttributes = new
            {
                sAMAccountName = samAccountName
            }
        };

        var serializedJson = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(serializedJson, Encoding.UTF8, "application/vnd.pingidentity.password.setGateway+json");
        var ldapGatewayLinkingResponse = await _pingOneHttpClient.PutAsync(ldapGatewayLinkingUrl, content);
        
        if (ldapGatewayLinkingResponse.IsSuccessStatusCode is false)
        {
            _logger.LogError("Failed to link PingFederate account for user {UserId} with status code {StatusCode}", pingOneUserId, ldapGatewayLinkingResponse.StatusCode);
            throw new IdentityLinkingException($"Failed to link PingFederate account for user {pingOneUserId} with status code {ldapGatewayLinkingResponse.StatusCode}");
        }

        var deserializedLdapGatewayLinkingResponse = await ldapGatewayLinkingResponse.Content.ReadFromJsonAsync<PingOneResponse>();
        ArgumentNullException.ThrowIfNull(deserializedLdapGatewayLinkingResponse);
        
        _logger.LogInformation("Successfully linked LDAP Gateway account for user {UserId} with status code {StatusCode}", pingOneUserId, ldapGatewayLinkingResponse.StatusCode);
        return deserializedLdapGatewayLinkingResponse;
    }
}