
namespace ApiSwaggerAuth.WinForm
{
    partial class ApiScanForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpAPI = new System.Windows.Forms.GroupBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.txtApiUrl = new System.Windows.Forms.TextBox();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.grpAPI.SuspendLayout();
            this.grpResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAPI
            // 
            this.grpAPI.Controls.Add(this.btnScan);
            this.grpAPI.Controls.Add(this.txtApiUrl);
            this.grpAPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAPI.Location = new System.Drawing.Point(0, 0);
            this.grpAPI.Name = "grpAPI";
            this.grpAPI.Size = new System.Drawing.Size(1447, 168);
            this.grpAPI.TabIndex = 0;
            this.grpAPI.TabStop = false;
            this.grpAPI.Text = "API URL";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(12, 101);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(150, 46);
            this.btnScan.TabIndex = 1;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // txtApiUrl
            // 
            this.txtApiUrl.Location = new System.Drawing.Point(10, 44);
            this.txtApiUrl.Name = "txtApiUrl";
            this.txtApiUrl.Size = new System.Drawing.Size(1431, 39);
            this.txtApiUrl.TabIndex = 0;
            // 
            // grpResults
            // 
            this.grpResults.Controls.Add(this.txtResults);
            this.grpResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResults.Location = new System.Drawing.Point(0, 168);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(1447, 714);
            this.grpResults.TabIndex = 1;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Results";
            // 
            // txtResults
            // 
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Location = new System.Drawing.Point(3, 35);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(1441, 676);
            this.txtResults.TabIndex = 1;
            // 
            // ApiScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1447, 882);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.grpAPI);
            this.Name = "ApiScanForm";
            this.Text = "API Scanner";
            this.grpAPI.ResumeLayout(false);
            this.grpAPI.PerformLayout();
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAPI;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.TextBox txtApiUrl;
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.TextBox txtResults;
    }
}

