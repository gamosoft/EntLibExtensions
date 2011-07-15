using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.ApplicationServer.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;

namespace EntLibExtensions
{
    // To use the DLL in the application folder it has to be "CustomCacheManagerData"
    //[ConfigurationElementType(typeof(CustomCacheManagerData))]
    // To use the DLL in the EntLib console folder (enhanced design support) use "AppFabricCacheManagerDataBase"
    // C:\Program Files (x86)\Microsoft Enterprise Library 5.0\Bin
    [ConfigurationElementType(typeof(AppFabricCacheManagerDataBase))]
    public class AppFabricCacheManager : ICacheManager
    {
        #region "Variables"

        private const string _defaultHostName = "localhost";
        private int _defaultPort = 22233;
        private const string _defaultCacheName = "default";
        private const string _defaultRegionName = "default";

        private DataCache _dc;

        #endregion "Variables"

        #region "Properties"

        /// <summary>
        /// Name of the AppFabric host/cluster
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Port for AppFabric
        /// </summary>
        public int CachePort { get; set; }

        /// <summary>
        /// Name of the Cache
        /// </summary>
        public string CacheName { get; set; }

        /// <summary>
        /// Whether to use local cache or not
        /// </summary>
        public bool LocalCache { get; set; }

        /// <summary>
        /// Security mode
        /// </summary>
        public DataCacheSecurityMode SecurityMode { get; set; }

        /// <summary>
        /// Protection level
        /// </summary>
        public DataCacheProtectionLevel ProtectionLevel { get; set; }
        
        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Default constructor to test again localhost:22233 on "default" cache,
        /// no local cache, Transport security mode and EncryptAndSign protection level
        /// </summary>
        public AppFabricCacheManager()
        {
            this.SetDefaultValues();
            this.Initialize();
        }

        /// <summary>
        /// Constructor with no local cache, Transport security mode and EncryptAndSign protection level
        /// </summary>
        /// <param name="hostName">Host name</param>
        /// <param name="cachePort">Cache port</param>
        /// <param name="cacheName">Cache name</param>
        public AppFabricCacheManager(string hostName, int cachePort, string cacheName)
        {
            this.SetDefaultValues();
            // Custom values
            this.HostName = hostName;
            this.CachePort = cachePort;
            this.CacheName = cacheName;
            this.Initialize();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hostName">Host name</param>
        /// <param name="cachePort">Cache port</param>
        /// <param name="cacheName">Cache name</param>
        /// <param name="localCache">Whether to use local cache or not</param>
        /// <param name="securityMode">Security mode</param>
        /// <param name="protectionLevel">Protection level</param>
        public AppFabricCacheManager(string hostName, int cachePort, string cacheName, bool localCache, DataCacheSecurityMode securityMode, DataCacheProtectionLevel protectionLevel)
        {
            this.HostName = hostName;
            this.CachePort = cachePort;
            this.CacheName = cacheName;
            this.LocalCache = localCache;
            this.SecurityMode = securityMode;
            this.ProtectionLevel = protectionLevel;
            this.Initialize();
        }

        /// <summary>
        /// Constructor to be used when the DLL is not copied in the EntLib configuration folder
        /// </summary>
        /// <param name="collection">Collection of parameters</param>
        public AppFabricCacheManager(NameValueCollection collection)
        {
            this.SetDefaultValues();
            foreach (string key in collection.AllKeys)
            {
                if (!String.IsNullOrEmpty(collection[key]))
                {
                    switch (key.ToLower())
                    {
                        case "hostname":
                            this.HostName = collection[key];
                            break;
                        case "cacheport":
                            this.CachePort = int.Parse(collection[key]);
                            break;
                        case "cachename":
                            this.CacheName = collection[key];
                            break;
                        case "localcache":
                            this.LocalCache = bool.Parse(collection[key]);
                            break;
                        case "securitymode":
                            this.SecurityMode = (DataCacheSecurityMode)Enum.Parse(typeof(DataCacheSecurityMode), collection[key]);
                            break;
                        case "protectionlevel":
                            this.ProtectionLevel = (DataCacheProtectionLevel)Enum.Parse(typeof(DataCacheProtectionLevel), collection[key]);
                            break;
                    }
                }
            }
            this.Initialize();
        }

        /// <summary>
        /// Set default values to avoid errors
        /// </summary>
        private void SetDefaultValues()
        {
            this.HostName = _defaultHostName;
            this.CachePort = _defaultPort;
            this.CacheName = _defaultCacheName;
            this.LocalCache = false;
            this.SecurityMode = DataCacheSecurityMode.Transport;
            this.ProtectionLevel = DataCacheProtectionLevel.EncryptAndSign;
        }

        /// <summary>
        /// Initializes the cache provider with the specified settings
        /// </summary>
        private void Initialize()
        {
            DataCacheFactoryConfiguration cfg = new DataCacheFactoryConfiguration()
            {
                Servers = CreateCacheEndpoints(this.HostName, this.CachePort),
                SecurityProperties = new DataCacheSecurity(this.SecurityMode, this.ProtectionLevel)
            };

            if (this.LocalCache)
            {
                // Use these default values, make it read from config file
                DataCacheLocalCacheProperties localCacheConfig;
                TimeSpan localTimeout = new TimeSpan(0, 0, 30);
                localCacheConfig = new DataCacheLocalCacheProperties(10000, localTimeout, DataCacheLocalCacheInvalidationPolicy.TimeoutBased);
                cfg.LocalCacheProperties = localCacheConfig;
            }

            var factory = new DataCacheFactory(cfg);
            _dc = factory.GetCache(this.CacheName);
        }

        /// <summary>
        /// Creates cache endpoints
        /// </summary>
        /// <param name="hostName">Host name</param>
        /// <param name="cachePort">Cache port</param>
        /// <returns>List of endpoints</returns>
        private static List<DataCacheServerEndpoint> CreateCacheEndpoints(string hostName, int cachePort)
        {
            List<DataCacheServerEndpoint> servers = new List<DataCacheServerEndpoint>();
            servers.Add(new DataCacheServerEndpoint(hostName, cachePort));
            return servers;
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
            this.Add(key, value);
        }

        /// <summary>
        /// Adds an item to the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <param name="value">Value</param>
        public void Add(string key, object value)
        {
            _dc.Add(key, value);
            // if (_dc.Add(key, value) == null) throw (new Exception("error"));
            // _dc.Add(key, value, TimeSpan.FromMinutes(30));
            //catch (DataCacheException cacheError)
        }

        /// <summary>
        /// Whether the cache contains the provided key
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>True or false</returns>
        public bool Contains(string key)
        {
            return (_dc.Get(key) != null);
        }

        /// <summary>
        /// Number of items in the cache
        /// </summary>
        public int Count
        {
            get
            {
                int counter = 0;
                foreach (string regionName in _dc.GetSystemRegions())
                {
                    foreach (KeyValuePair<string, object> cached in _dc.GetObjectsInRegion(regionName))
                    {
                        counter++;
                    }
                }
                return counter;
            }
        }

        /// <summary>
        /// Flush the cache (clearing all regions)
        /// </summary>
        public void Flush()
        {
            foreach (string regionName in _dc.GetSystemRegions()) 
            {
                _dc.ClearRegion(regionName);
            } 
        }

        /// <summary>
        /// Gets a cached object
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Cached object</returns>
        public object GetData(string key)
        {
            return _dc.Get(key);
        }

        /// <summary>
        /// Removes an object from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        public void Remove(string key)
        {
            _dc.Remove(key);
        }

        /// <summary>
        /// Indexer to get objects from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Cached object</returns>
        public object this[string key]
        {
            get { return _dc.Get(key); }
        }

        #endregion "Methods"
    }
}