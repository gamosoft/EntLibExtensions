using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace EntLibExtensions
{
    // To use the DLL in the application folder it has to be "CustomCacheManagerData"
    //[ConfigurationElementType(typeof(CustomCacheManagerData))]
    // To use the DLL in the EntLib console folder (enhanced design support) use "DisabledCacheManagerDataBase"
    // C:\Program Files (x86)\Microsoft Enterprise Library 5.0\Bin
    [ConfigurationElementType(typeof(DisabledCacheManagerDataBase))]
    public class DisabledCacheManager : ICacheManager
    {
        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        public DisabledCacheManager()
        {
        }

        /// <summary>
        /// Constructor to be used when the DLL is not copied in the EntLib configuration folder
        /// </summary>
        /// <param name="collection">Collection of parameters</param>
        public DisabledCacheManager(NameValueCollection collection)
        {
        }

        /// <summary>
        /// Adds an item to the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="value">Value</param>
        /// <param name="scavengingPriority">CacheItemPriority</param>
        /// <param name="refreshAction">ICacheItemRefreshAction</param>
        /// <param name="expirations">ICacheItemExpirations</param>
        public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations)
        {
            // Do nothing
        }

        /// <summary>
        /// Adds an item to the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="value">Value</param>
        public void Add(string key, object value)
        {
            // Do nothing
        }

        /// <summary>
        /// Whether the cache contains the provided key
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>True or false</returns>
        public bool Contains(string key)
        {
            return false;
        }

        /// <summary>
        /// Number of items in the cache
        /// </summary>
        public int Count
        {
            get { return 0; }
        }

        /// <summary>
        /// Flush the cache
        /// </summary>
        public void Flush()
        {
            // Do nothing
        }

        /// <summary>
        /// Gets a cached object
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Cached object</returns>
        public object GetData(string key)
        {
            return null;
        }

        /// <summary>
        /// Removes an object from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        public void Remove(string key)
        {
            // Do nothing
        }

        /// <summary>
        /// Indexer to get objects from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Cached object</returns>
        public object this[string key]
        {
            get { return null; }
        }

        #endregion "Methods"
    }
}