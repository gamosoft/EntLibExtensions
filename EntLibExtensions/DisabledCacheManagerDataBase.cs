using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;

namespace EntLibExtensions
{
    /// <summary>
    /// Class definition for Enterprise Library console integration
    /// </summary>
    [Description("Disabled Cache Manager Provider")]
    [DisplayName("Disabled Cache Manager")]
    public class DisabledCacheManagerDataBase : CacheManagerDataBase
    {
        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        public DisabledCacheManagerDataBase() : base(typeof(DisabledCacheManager))
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
                () => new DisabledCacheManager())
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
            return () => new DisabledCacheManager();
        }

        #endregion "Methods"
    }
}