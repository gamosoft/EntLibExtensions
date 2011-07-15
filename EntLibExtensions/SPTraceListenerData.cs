using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace EntLibExtensions
{
    /// <summary>
    /// Class definition for Enterprise Library console integration
    /// </summary>
    [Description("SharePoint Trace Listener")]
    [DisplayName("SharePoint Trace Listener")]
    public class SPTraceListenerData : TraceListenerData
    {
        #region "Variables"

        private const string _siteURLString = "SiteURL";
        private const string _listNameString = "ListName";

        #endregion "Variables"

        #region "Properties"

        /// <summary>
        /// Gets/sets the site URL
        /// </summary>
        [ConfigurationProperty(_siteURLString, IsRequired = true, DefaultValue = "http://localhost/sites/entlibdemo")]
        [Description("Site URL")]
        public string SiteURL
        {
            get
            {
                return (string)base[_siteURLString];
            }
            set
            {
                base[_siteURLString] = value;
            }
        }

        /// <summary>
        /// Gets/sets the list name
        /// </summary>
        [ConfigurationProperty(_listNameString, IsRequired = true, DefaultValue = "EntLib SharePoint Logging List")]
        [Description("List Name")]
        public string ListName
        {
            get
            {
                return (string)base[_listNameString];
            }
            set
            {
                base[_listNameString] = value;
            }
        }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        public SPTraceListenerData() : base(typeof(SPTraceListener))
        {
        }

        /// <summary>
        /// Get trace listener instance
        /// </summary>
        /// <returns>Trace listener instance</returns>
        protected override System.Linq.Expressions.Expression<Func<System.Diagnostics.TraceListener>> GetCreationExpression()
        {
            return () => new SPTraceListener(SiteURL, ListName, this.TraceOutputOptions);
        }

        #endregion "Methods"
    }
}
