using Microsoft.Extensions.Logging;

namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Services.ResourceState;

/// <inheritdoc/>
public class ResourceStateService : IResourceStateService
{
    private readonly ReaderWriterLockSlim _lock;
    private readonly ILogger<ResourceStateService> _logger;

    private bool _oidcAggregationRunning;
    private bool _oidcPurgeRunning;
    private bool _samlAggregationRunning;
    private bool _samlPurgeRunning;
    private bool _legacyAggregationRunning;
    private bool _legacyPurgeRunning;
    private bool _ServiceNowApplicationsAggregationRunning;
    private bool _ServiceNowApplicationsPurgeRunning;
    private bool _ServiceNowUsersAggregationRunning;
    private bool _ServiceNowUsersPurgeRunning;

    /// <summary>
    /// Create an instance of <see cref="ResourceStateService"/>
    /// </summary>
    /// <param name="logger">Logger</param>
    public ResourceStateService(ILogger<ResourceStateService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);

        _logger = logger;
        _lock = new ReaderWriterLockSlim();
    }


    /// <inheritdoc />
    public bool IsOidcAggregationRunning 
    { 
        get 
        {
            _logger.LogDebug("Entering read lock for IsOidcAggregationRunning");
            _lock.EnterReadLock();
            try
            {
                return _oidcAggregationRunning;
            }
            finally
            {
                _logger.LogDebug("Exiting read lock for IsOidcAggregationRunning");
                _lock.ExitReadLock();
            }
        }
        set
        {
            _logger.LogDebug("Entering write lock for IsOidcAggregationRunning");
            _lock.EnterWriteLock();
            try
            {
                _oidcAggregationRunning = value;
            }
            finally
            {
                _logger.LogDebug("Exiting write lock for IsOidcAggregationRunning");
                _lock.ExitWriteLock();
            }
        } 
    }
        
    /// <inheritdoc />
    public bool IsOidcPurgeRunning
    {
        get
        {
            _logger.LogDebug("Entering read lock for IsOidcPurgeRunning");
            _lock.EnterReadLock();
            try
            {
                return _oidcPurgeRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsOidcPurgeRunning");
            }
        }
        set
        {
            _logger.LogDebug("Entering write lock for IsOidcPurgeRunning");
            _lock.EnterWriteLock();
            try
            {
                _oidcPurgeRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsOidcPurgeRunning");
            }
        }
    }
    
    /// <inheritdoc />
    public bool IsSamlAggregationRunning
    { 
        get
        {
            _logger.LogDebug("Entering read lock for IsSamlAggregationRunning");
            _lock.EnterReadLock();
            try
            {
                return _samlAggregationRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsSamlAggregationRunning");
            }
        }
        set
        {
            _logger.LogDebug("Entering write lock for IsSamlAggregationRunning");
            _lock.EnterWriteLock();
            try
            {
                _samlAggregationRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsSamlAggregationRunning");
            }
        }
    }
    
    /// <inheritdoc />
    public bool IsSamlPurgeRunning
    {
        get
        {
            _logger.LogDebug("Entering read lock for IsSamlPurgeRunning");
            _lock.EnterReadLock();
            try
            {
                return _samlPurgeRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsSamlPurgeRunning");
            }
        }
        set
        {
            _logger.LogDebug("Entering write lock for IsSamlPurgeRunning");
            _lock.EnterWriteLock();
            try
            {
                _samlPurgeRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsSamlPurgeRunning");
            }
        }
    }

    /// <inheritdoc />
    public bool IsLegacyAggregationRunning 
    {
        get
        {
            _logger.LogDebug("Entering read lock for IsLegacyAggregationRunning");
            _lock.EnterReadLock();
            try
            {
                return _legacyAggregationRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsLegacyAggregationRunning");
            }
        }

        set
        {
            _logger.LogDebug("Entering write lock for IsLegacyAggregationRunning");
            _lock.EnterWriteLock();
            try
            {
                _legacyAggregationRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsLegacyAggregationRunning");
            }
        }
    }

    /// <inheritdoc />
    public bool IsLegacyPurgeRunning
    {
        get
        {
            _logger.LogDebug("Entering read lock for IsLegacyPurgeRunning");
            _lock.EnterReadLock();
            try
            {
                return _legacyPurgeRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsLegacyPurgeRunning");
            }
        }

        set
        {
            _logger.LogDebug("Entering write lock for IsLegacyPurgeRunning");
            _lock.EnterWriteLock();
            try
            {
                _legacyPurgeRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsLegacyPurgeRunning");
            }
        }
    }

    /// <inheritdoc />
    public bool IsServiceNowApplicationsAggregationRunning
    {
        get
        {
            _logger.LogDebug("Entering read lock for IsServiceNowApplicationsAggregationRunning");
            _lock.EnterReadLock();
            try
            {
                return _ServiceNowApplicationsAggregationRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsServiceNowApplicationsAggregationRunning");
            }
        }

        set
        {
            _logger.LogDebug("Entering write lock for IsServiceNowApplicationsAggregationRunning");
            _lock.EnterWriteLock();
            try
            {
                _ServiceNowApplicationsAggregationRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsServiceNowApplicationsAggregationRunning");
            }
        }
    }

    /// <inheritdoc/>
    public bool IsServiceNowApplicationsPurgingRunning
    {
        get
        {
            _logger.LogDebug("Entering read lock for IsServiceNowApplicationsPurgingRunning");
            _lock.EnterReadLock();
            try
            {
                return _ServiceNowApplicationsPurgeRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for IsServiceNowApplicationsPurgingRunning");
            }
        }

        set
        {
            _logger.LogDebug("Entering write lock for IsServiceNowApplicationsPurgingRunning");
            _lock.EnterWriteLock();
            try
            {
                _ServiceNowApplicationsPurgeRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for IsServiceNowApplicationsPurgingRunning");
            }
        }
    }

    /// <inheritdoc/>
    public bool IsServiceNowUsersAggregationRunning 
    { 
        get
        {
            _logger.LogDebug("Entering read lock for _ServiceNowUsersAggregationRunning");
            _lock.EnterReadLock();
            try
            {
                return _ServiceNowUsersAggregationRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for _ServiceNowUsersAggregationRunning");
            }
        }

        set
        {
            _logger.LogDebug("Entering write lock for _ServiceNowUsersAggregationRunning");
            _lock.EnterWriteLock();
            try
            {
                _ServiceNowUsersAggregationRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for _ServiceNowUsersAggregationRunning");
            }
        }
    }

    /// <inheritdoc/>
    public bool IsServiceNowUsersPurgingRunning 
    { 
        get
        {
            _logger.LogDebug("Entering read lock for _ServiceNowUsersPurgeRunning");
            _lock.EnterReadLock();
            try
            {
                return _ServiceNowUsersPurgeRunning;
            }
            finally
            {
                _lock.ExitReadLock();
                _logger.LogDebug("Exiting read lock for _ServiceNowUsersPurgeRunning");
            }
        }

        set
        {
            _logger.LogDebug("Entering write lock for _ServiceNowUsersPurgeRunning");
            _lock.EnterWriteLock();
            try
            {
                _ServiceNowUsersPurgeRunning = value;
            }
            finally
            {
                _lock.ExitWriteLock();
                _logger.LogDebug("Exiting write lock for _ServiceNowUsersPurgeRunning");
            }
        }
    }
}
