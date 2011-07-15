using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace EntLibExtensionsTest
{
    /// <summary>
    /// Main test code
    /// </summary>
    public partial class Main : Form
    {
        #region "Methods"

        /// <summary>
        /// Default constructor
        /// </summary>
        public Main()
        {
            // Some notes:
            // To debug a SP2010 console application, change build to AnyCPU
            // http://rhythmiccoding.blogspot.com/2010/06/retrieving-com-class-factory-for.html
            // Right click on the ProjectName >> Properties >> Build tab >> Platform target >> Select "Any CPU". (By Default, it's x86)
            //
            // Sometimes (if running inside VM) you can get an incorrect cache miss. Create a new cache with NO eviction: New-Cache cacheName -Eviction None
            InitializeComponent();
        }

        /// <summary>
        /// Show some values upon screen loading
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void Main_Load(object sender, EventArgs e)
        {
            this.cboSeverity.DataSource = Enum.GetValues(typeof(TraceEventType));
            this.cboSeverity.SelectedItem = TraceEventType.Information;

            ICacheManager mgr = CacheFactory.GetCacheManager();
            string cacheName = (mgr is EntLibExtensions.AppFabricCacheManager)
                ? ((EntLibExtensions.AppFabricCacheManager)mgr).CacheName
                : "[Not AppFabric]";
            this.lblCacheName.Text = String.Format("Cache Name: {0}", cacheName);
        }

        /// <summary>
        /// Gets an item from cache
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetFromCache_Click(object sender, EventArgs e)
        {
            this.EnableControls(false);
            string uniqueKey = "unique_key";

            ICacheManager mgr = CacheFactory.GetCacheManager();
            object myobject = mgr[uniqueKey];
            this.chkCacheHit.Checked = (myobject != null);
            if (myobject == null)
            {
                mgr.Add(uniqueKey, "Value retrieved from cache");
                myobject = "Value retrieved from method";
            }
            this.lblCacheValue.Text = (string)myobject;
            this.EnableControls(true);
        }

        /// <summary>
        /// SharePoint 2010 log sample
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void btnSPLogEntry_Click(object sender, EventArgs e)
        {
            this.EnableControls(false);
            // The following entry is a workaround to use Data Access Block in the GAC
            // http://entlib.codeplex.com/workitem/26903
            try
            {
                Dictionary<string, object> props = new Dictionary<string, object>();
                props.Add("property1", DateTime.Now);
                props.Add("property2", true);
                props.Add("property3", "String sample");

                Random r = new Random();
                LogEntry le = new LogEntry();
                le.ActivityId = Guid.NewGuid();
                le.Title = "New log entry!";
                // le.Categories.Add("General");
                le.Message = String.Format("This is a new log entry to test SPTraceListener... Random:{0}", r.Next(1000).ToString("000"));
                le.Severity = (TraceEventType)this.cboSeverity.SelectedItem;
                le.ExtendedProperties = props;
                Logger.Write(le);
                // Logger.Write("Title copied from the message");
                // Logger.Write("Message", "General", 0, 0, TraceEventType.Error, "New title", props);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            this.EnableControls(true);
        }

        /// <summary>
        /// Attempt to flush the cache from the current Cache Manager
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void btnFlush_Click(object sender, EventArgs e)
        {
            this.EnableControls(false);
            ICacheManager mgr = CacheFactory.GetCacheManager();
            // MessageBox.Show(String.Format("Items to be deleted from the cache : {0}", mgr.Count.ToString()));
            mgr.Flush();
            this.chkCacheHit.Checked = false;
            this.lblCacheValue.Text = "";
            this.EnableControls(true);
        }

        /// <summary>
        /// Enable/disable UI controls and set cursor to wait
        /// </summary>
        /// <param name="controlsEnabled">Whether to enable/disable controls</param>
        private void EnableControls(bool controlsEnabled)
        {
            this.Enabled = controlsEnabled;
            this.Cursor = controlsEnabled ? Cursors.Default : Cursors.WaitCursor;
        }

        #endregion "Methods"
    }
}