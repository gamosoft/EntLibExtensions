using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.SharePoint;

namespace EntLibExtensions
{
    /// <summary>
    /// SharePoint trace listener for Enterprise Library
    /// </summary>
    [ConfigurationElementType(typeof(SPTraceListenerData))]
    // [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class SPTraceListener : CustomTraceListener
    {
        #region "Properties"

        /// <summary>
        /// Gets/sets the Site URL
        /// </summary>
        public string SiteURL { get; set; }

        /// <summary>
        /// Gets/sets the list name
        /// </summary>
        public string ListName { get; set; }

        #endregion "Properties"

        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="siteURL">Site URL</param>
        /// <param name="listName">List name</param>
        /// <param name="traceOptions">Trace options</param>
        public SPTraceListener(string siteURL, string listName, TraceOptions traceOptions)
        {
            this.SiteURL = siteURL;
            this.ListName = listName;
            this.TraceOutputOptions = traceOptions;
        }

        /// <summary>
        /// Constructor to be used when the DLL is not copied in the EntLib configuration folder
        /// </summary>
        /// <param name="collection">Collection of parameters</param>
        public SPTraceListener(NameValueCollection collection)
        {
            foreach (string key in collection.AllKeys)
            {
                if (!String.IsNullOrEmpty(collection[key]))
                {
                    switch (key.ToLower())
                    {
                        case "listurl":
                            this.SiteURL = collection[key];
                            break;
                        case "listname":
                            this.ListName = collection[key];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Method invoked when data needs to be traced. Will filter based on current trace options
        /// </summary>
        /// <param name="eventCache">TraceEventCache</param>
        /// <param name="source">Source</param>
        /// <param name="eventType">TraceEventType</param>
        /// <param name="id">ID</param>
        /// <param name="data">Data</param>
        public override void TraceData(System.Diagnostics.TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, object data)
        {
            // If there's no filter means we should log everything
            if (this.Filter == null || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                this.AddNewEntry(data as LogEntry);
            }
        }

        /// <summary>
        /// Adds a new log entry
        /// </summary>
        /// <param name="data">LogEntry</param>
        private void AddNewEntry(LogEntry data)
        {
            SPSecurity.RunWithElevatedPrivileges(
                delegate()
                {
                    using (SPSite s = new SPSite(this.SiteURL))
                    {
                        using (SPWeb w = s.OpenWeb())
                        {
                            SPListItem item = w.Lists[this.ListName].Items.Add();
                            item["Title"] = String.IsNullOrEmpty(data.Title) ? data.Message : data.Title;
                            item["Message"] = data.Message;
                            item["Severity"] = data.Severity;
                            item["TimeStamp"] = data.TimeStamp;
                            item["ActivityId"] = data.ActivityIdString;
                            StringBuilder sb = new StringBuilder();
                            string propertySeparator = "<br />";
                            foreach (string property in data.ExtendedProperties.Keys)
                            {
                                sb.AppendFormat("{0}={1}{2}", property, data.ExtendedProperties[property].ToString(), propertySeparator);
                            }
                            item["ExtendedProperties"] = sb.ToString();
                            item.Update();
                        }
                    }
                });
        }

        /// <summary>
        /// Writes a message
        /// </summary>
        /// <param name="message">Message</param>
        public override void Write(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes a message
        /// </summary>
        /// <param name="message">Message</param>
        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }

        #endregion "Methods"
    }
}