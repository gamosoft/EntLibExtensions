namespace EntLibExtensionsTest
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGetFromCache = new System.Windows.Forms.Button();
            this.btnAddLogEntry = new System.Windows.Forms.Button();
            this.grpCache = new System.Windows.Forms.GroupBox();
            this.btnFlush = new System.Windows.Forms.Button();
            this.lblCacheValue = new System.Windows.Forms.Label();
            this.chkCacheHit = new System.Windows.Forms.CheckBox();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.cboSeverity = new System.Windows.Forms.ComboBox();
            this.lblCacheName = new System.Windows.Forms.Label();
            this.grpCache.SuspendLayout();
            this.grpLogging.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetFromCache
            // 
            this.btnGetFromCache.Location = new System.Drawing.Point(17, 53);
            this.btnGetFromCache.Name = "btnGetFromCache";
            this.btnGetFromCache.Size = new System.Drawing.Size(109, 23);
            this.btnGetFromCache.TabIndex = 0;
            this.btnGetFromCache.Text = "Get from Cache";
            this.btnGetFromCache.UseVisualStyleBackColor = true;
            this.btnGetFromCache.Click += new System.EventHandler(this.btnGetFromCache_Click);
            // 
            // btnAddLogEntry
            // 
            this.btnAddLogEntry.Location = new System.Drawing.Point(67, 66);
            this.btnAddLogEntry.Name = "btnAddLogEntry";
            this.btnAddLogEntry.Size = new System.Drawing.Size(109, 23);
            this.btnAddLogEntry.TabIndex = 1;
            this.btnAddLogEntry.Text = "Insert Log Entry";
            this.btnAddLogEntry.UseVisualStyleBackColor = true;
            this.btnAddLogEntry.Click += new System.EventHandler(this.btnSPLogEntry_Click);
            // 
            // grpCache
            // 
            this.grpCache.Controls.Add(this.lblCacheName);
            this.grpCache.Controls.Add(this.btnFlush);
            this.grpCache.Controls.Add(this.lblCacheValue);
            this.grpCache.Controls.Add(this.chkCacheHit);
            this.grpCache.Controls.Add(this.btnGetFromCache);
            this.grpCache.Location = new System.Drawing.Point(13, 13);
            this.grpCache.Name = "grpCache";
            this.grpCache.Size = new System.Drawing.Size(254, 130);
            this.grpCache.TabIndex = 2;
            this.grpCache.TabStop = false;
            this.grpCache.Text = "Cache";
            // 
            // btnFlush
            // 
            this.btnFlush.Location = new System.Drawing.Point(203, 19);
            this.btnFlush.Name = "btnFlush";
            this.btnFlush.Size = new System.Drawing.Size(45, 23);
            this.btnFlush.TabIndex = 4;
            this.btnFlush.Text = "Flush";
            this.btnFlush.UseVisualStyleBackColor = true;
            this.btnFlush.Click += new System.EventHandler(this.btnFlush_Click);
            // 
            // lblCacheValue
            // 
            this.lblCacheValue.AutoSize = true;
            this.lblCacheValue.Location = new System.Drawing.Point(17, 93);
            this.lblCacheValue.Name = "lblCacheValue";
            this.lblCacheValue.Size = new System.Drawing.Size(58, 13);
            this.lblCacheValue.TabIndex = 2;
            this.lblCacheValue.Text = "-----------------";
            // 
            // chkCacheHit
            // 
            this.chkCacheHit.AutoSize = true;
            this.chkCacheHit.Enabled = false;
            this.chkCacheHit.Location = new System.Drawing.Point(175, 89);
            this.chkCacheHit.Name = "chkCacheHit";
            this.chkCacheHit.Size = new System.Drawing.Size(73, 17);
            this.chkCacheHit.TabIndex = 1;
            this.chkCacheHit.Text = "Cache Hit";
            this.chkCacheHit.UseVisualStyleBackColor = true;
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.lblSeverity);
            this.grpLogging.Controls.Add(this.cboSeverity);
            this.grpLogging.Controls.Add(this.btnAddLogEntry);
            this.grpLogging.Location = new System.Drawing.Point(14, 158);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(254, 103);
            this.grpLogging.TabIndex = 3;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "Logging";
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(13, 32);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(48, 13);
            this.lblSeverity.TabIndex = 3;
            this.lblSeverity.Text = "Severity:";
            // 
            // cboSeverity
            // 
            this.cboSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeverity.FormattingEnabled = true;
            this.cboSeverity.Location = new System.Drawing.Point(67, 29);
            this.cboSeverity.Name = "cboSeverity";
            this.cboSeverity.Size = new System.Drawing.Size(154, 21);
            this.cboSeverity.TabIndex = 2;
            // 
            // lblCacheName
            // 
            this.lblCacheName.AutoSize = true;
            this.lblCacheName.Location = new System.Drawing.Point(17, 25);
            this.lblCacheName.Name = "lblCacheName";
            this.lblCacheName.Size = new System.Drawing.Size(58, 13);
            this.lblCacheName.TabIndex = 5;
            this.lblCacheName.Text = "-----------------";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 273);
            this.Controls.Add(this.grpLogging);
            this.Controls.Add(this.grpCache);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EntLib Extensions";
            this.Load += new System.EventHandler(this.Main_Load);
            this.grpCache.ResumeLayout(false);
            this.grpCache.PerformLayout();
            this.grpLogging.ResumeLayout(false);
            this.grpLogging.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetFromCache;
        private System.Windows.Forms.Button btnAddLogEntry;
        private System.Windows.Forms.GroupBox grpCache;
        private System.Windows.Forms.CheckBox chkCacheHit;
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.Label lblCacheValue;
        private System.Windows.Forms.ComboBox cboSeverity;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.Button btnFlush;
        private System.Windows.Forms.Label lblCacheName;
    }
}

