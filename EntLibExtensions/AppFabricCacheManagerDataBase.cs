using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq.Expressions;
using Microsoft.ApplicationServer.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;

namespace EntLibExtensions
{
    /// <summary>
    /// Class definition for Enterprise Library console integration
    /// </summary>
    [Description("AppFabric Cache Manager Provider")]
    [DisplayName("AppFabric Cache Manager")]
    public class AppFabricCacheManagerDataBase : CacheManagerDataBase
    {
        #region "Variables"

        private const string _hostNameString = "HostName";
        private const string _cachePortString = "CachePort";
        private const string _cacheNameString = "CacheName";
        private const string _localCacheString = "LocalCache";
        private const string _securityModeString = "SecurityMode";
        private const string _protectionLevelString = "ProtectionMode";

        #endregion "Variables"

        #region "Properties"

        /// <summary>
        /// Gets/sets the host name
        /// </summary>
        [ConfigurationProperty(_hostNameString, IsRequired = true, DefaultValue = "localhost")]
        [Description("Host Name")]
        public string HostName
        {
            get
            {
                return (string)base[_hostNameString];
            }
            set
            {
                base[_hostNameString] = value;
            }
        }

        /// <summary>
        /// Gets/sets the cache port
        /// </summary>
        [ConfigurationProperty(_cachePortString, IsRequired = true, DefaultValue = 22233)]
        [Description("Cache Port")]
        public int CachePort
        {
            get
            {
                return (int)base[_cachePortString];
            }
            set
            {
                base[_cachePortString] = value;
            }
        }

        /// <summary>
        /// Gets/sets the cache name
        /// </summary>
        [ConfigurationProperty(_cacheNameString, IsRequired = true, DefaultValue = "default")]
        [Description("Cache Name")]
        public string CacheName
        {
            get
            {
                return (string)base[_cacheNameString];
            }
            set
            {
                base[_cacheNameString] = value;
            }
        }

        /// <summary>
        /// Gets/sets whether to use local cache
        /// </summary>
        [ConfigurationProperty(_localCacheString, IsRequired = true, DefaultValue = false)]
        [Description("Local Cache")]
        public bool LocalCache
        {
            get
            {
                return (bool)base[_localCacheString];
            }
            set
            {
                base[_localCacheString] = value;
            }
        }

        /// <summary>
        /// Gets/sets the security mode
        /// </summary>
        [ConfigurationProperty(_securityModeString, IsRequired = true, DefaultValue = DataCacheSecurityMode.Transport)]
        [Description("Security Mode")]
        public DataCacheSecurityMode SecurityMode
        {
            get
            {
                return (DataCacheSecurityMode)base[_securityModeString];
            }
            set
            {
                base[_securityModeString] = value;
            }
        }

        /// <summary>
        /// Gets/sets the protection level
        /// </summary>
        [ConfigurationProperty(_protectionLevelString, IsRequired = true, DefaultValue = DataCacheProtectionLevel.EncryptAndSign)]
        [Description("Protection Level")]
        public DataCacheProtectionLevel ProtectionLevel
        {
            get
            {
                return (DataCacheProtectionLevel)base[_protectionLevelString];
            }
            set
            {
                base[_protectionLevelString] = value;
            }
        }        

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppFabricCacheManagerDataBase() : base(typeof(AppFabricCacheManager))
        {
        }

        /// <summary>
        /// Get registrations
        /// </summary>
        /// <param name="configurationSource">Configuration source</param>
        /// <returns>Registrations</returns>
        public override IEnumerable<TypeRegistration> GetRegistrations(IConfigurationSource configurationSource)
        {
            yield return
              new TypeRegistration<ICacheManager>(
                () => new AppFabricCacheManager(HostName, CachePort, CacheName, LocalCache, SecurityMode, ProtectionLevel))
              {
                  Name = this.Name,
                  Lifetime = TypeRegistrationLifetime.Transient
              };
        }

        /// <summary>
        /// Get cache manager instance
        /// </summary>
        /// <returns>Cache manager instance</returns>
        protected override Expression<Func<ICacheManager>> GetCacheManagerCreationExpression()
        {
            return () => new AppFabricCacheManager(HostName, CachePort, CacheName, LocalCache, SecurityMode, ProtectionLevel);
        }

        #endregion "Methods"
    }
}