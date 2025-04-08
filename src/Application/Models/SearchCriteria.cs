namespace Mskcc.Tools.Idp.ConnectionsAggregator.Application.Models;

/// <summary>
/// Class for storing the search criteria.
/// </summary>
public class SearchCriteria
{
    private readonly ReaderWriterLockSlim _lock;
    private string _searchCriteria = string.Empty;

    /// <summary>
    /// Creates an instance of <see cref="SearchCriteriaService"/>
    /// </summary>
    /// <param name="logger"></param>
    public SearchCriteria()
    {
        _lock = new ReaderWriterLockSlim();
    }

    /// <summary>
    /// Criteria for which to search with.
    /// </summary>
    /// <remarks>
    /// This property is thread-safe.
    /// </remarks>
    public string Criteria
    {
        get 
        {
            _lock.EnterReadLock();
            try
            {
                return _searchCriteria;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        set
        {
            _lock.EnterWriteLock();
            try
            {
                _searchCriteria = value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        } 
    }
}