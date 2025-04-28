using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.PingFederate;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Data;
using Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;
using Mskcc.Tools.Idp.ConnectionsAggregator.Domain.Entities;
using Mskcc.Tools.Idp.ConnectionsAggregator.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Tests;

public class PingFederateServiceTests
{
    private readonly Mock<ILogger<PingFederateService>> _mockLogger;
    private readonly Mock<ILogger<ApplicationDbContext>> _mockDbContextLogger;
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly Mock<IResourceStateService> _mockResourceStateService;
    private readonly Mock<IOptionsMonitor<PingFederateOptions>> _mockPingFederateOptions;
    private readonly Mock<IOptionsMonitor<ApiOptions>> _mockApiOptions;
    private readonly ApplicationDbContext _dbContext;
    private readonly PingFederateService _service;

    public PingFederateServiceTests()
    {

        // Setup Mock Logger
        _mockLogger = new Mock<ILogger<PingFederateService>>();

        // Setup Mock IHttpClientFactory
        _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        // Setup Mock logger for db context
        _mockDbContextLogger = new Mock<ILogger<ApplicationDbContext>>();

        // Setup mock ping federate options
        _mockPingFederateOptions = new Mock<IOptionsMonitor<PingFederateOptions>>();

        // Setup mock options 
        _mockApiOptions = new Mock<IOptionsMonitor<ApiOptions>>();

        // In-memory DB context setup
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        _dbContext = new ApplicationDbContext(options, _mockApiOptions.Object, _mockDbContextLogger.Object);

        // Setup Mock ResourceStateService
        _mockResourceStateService = new Mock<IResourceStateService>();

        // Create PingFederateService instance
        _service = new PingFederateService(
            _mockLogger.Object,
            _mockHttpClientFactory.Object,
            _dbContext,
            _mockResourceStateService.Object,
            _mockPingFederateOptions.Object);
    }

    [Fact]
    public async Task GetSpConnectionsAsync_ReturnsSpConnections_WhenAggregationIsNotRunning()
    {
        // Arrange
        _mockResourceStateService.Setup(s => s.IsSamlAggregationRunning).Returns(false);
        _dbContext.Set<SpConnection>().Add(new SpConnection { Name = "TestConnection" });
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _service.GetSpConnectionsAsync(new PaginationFilter(1, 10));

        // Assert
        Assert.Single(result);
        Assert.Equal("TestConnection", result.First().Name);
    }

    [Fact]
    public async Task PurgeSpConnectionsAsync_RemovesAllConnections()
    {
        // Arrange
        _dbContext.Set<SpConnection>().Add(new SpConnection { Name = "TestConnection" });
        await _dbContext.SaveChangesAsync();

        // Act
        await _service.PurgeSpConnectionsAsync();

        // Assert
        var connectionsCount = await _dbContext.Set<SpConnection>().CountAsync();
        Assert.Equal(0, connectionsCount);
    }

    [Fact]
    public void GetSpConnectionsCount_ReturnsCorrectCount()
    {
        // Arrange
        _dbContext.Set<SpConnection>().Add(new SpConnection { Name = "TestConnection" });
        _dbContext.SaveChanges();

        // Act
        var result = _service.GetSpConnectionsCount();

        // Assert
        Assert.Equal(1, result);
    }
}

// Fake HttpMessageHandler to simulate HTTP responses
public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _response;

    public FakeHttpMessageHandler(HttpResponseMessage response)
    {
        _response = response;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_response);
    }
}