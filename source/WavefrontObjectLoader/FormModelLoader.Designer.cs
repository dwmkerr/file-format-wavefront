namespace WavefrontObjectLoader
{
    partial class FormModelLoader
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelBusyStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarBusy = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainerMainAndOutput = new System.Windows.Forms.SplitContainer();
            this.splitContainerTreeAndDetails = new System.Windows.Forms.SplitContainer();
            this.treeViewModel = new System.Windows.Forms.TreeView();
            this.richTextBoxOutput = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainAndOutput)).BeginInit();
            this.splitContainerMainAndOutput.Panel1.SuspendLayout();
            this.splitContainerMainAndOutput.Panel2.SuspendLayout();
            this.splitContainerMainAndOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTreeAndDetails)).BeginInit();
            this.splitContainerTreeAndDetails.Panel1.SuspendLayout();
            this.splitContainerTreeAndDetails.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainerMainAndOutput);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(784, 516);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(784, 562);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelBusyStatus,
            this.toolStripProgressBarBusy});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelBusyStatus
            // 
            this.toolStripStatusLabelBusyStatus.Name = "toolStripStatusLabelBusyStatus";
            this.toolStripStatusLabelBusyStatus.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabelBusyStatus.Text = "Busy";
            this.toolStripStatusLabelBusyStatus.Visible = false;
            // 
            // toolStripProgressBarBusy
            // 
            this.toolStripProgressBarBusy.Name = "toolStripProgressBarBusy";
            this.toolStripProgressBarBusy.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBarBusy.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBarBusy.Visible = false;
            // 
            // splitContainerMainAndOutput
            // 
            this.splitContainerMainAndOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainAndOutput.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMainAndOutput.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMainAndOutput.Name = "splitContainerMainAndOutput";
            this.splitContainerMainAndOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMainAndOutput.Panel1
            // 
            this.splitContainerMainAndOutput.Panel1.Controls.Add(this.splitContainerTreeAndDetails);
            // 
            // splitContainerMainAndOutput.Panel2
            // 
            this.splitContainerMainAndOutput.Panel2.Controls.Add(this.richTextBoxOutput);
            this.splitContainerMainAndOutput.Size = new System.Drawing.Size(784, 516);
            this.splitContainerMainAndOutput.SplitterDistance = 372;
            this.splitContainerMainAndOutput.TabIndex = 0;
            // 
            // splitContainerTreeAndDetails
            // 
            this.splitContainerTreeAndDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTreeAndDetails.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTreeAndDetails.Name = "splitContainerTreeAndDetails";
            // 
            // splitContainerTreeAndDetails.Panel1
            // 
            this.splitContainerTreeAndDetails.Panel1.Controls.Add(this.treeViewModel);
            this.splitContainerTreeAndDetails.Size = new System.Drawing.Size(784, 372);
            this.splitContainerTreeAndDetails.SplitterDistance = 261;
            this.splitContainerTreeAndDetails.TabIndex = 0;
            // 
            // treeViewModel
            // 
            this.treeViewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewModel.Location = new System.Drawing.Point(0, 0);
            this.treeViewModel.Name = "treeViewModel";
            this.treeViewModel.Size = new System.Drawing.Size(261, 372);
            this.treeViewModel.TabIndex = 0;
            this.treeViewModel.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewModel_AfterSelect);
            // 
            // richTextBoxOutput
            // 
            this.richTextBoxOutput.BackColor = System.Drawing.Color.White;
            this.richTextBoxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxOutput.Name = "richTextBoxOutput";
            this.richTextBoxOutput.ReadOnly = true;
            this.richTextBoxOutput.Size = new System.Drawing.Size(784, 140);
            this.richTextBoxOutput.TabIndex = 0;
            this.richTextBoxOutput.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.loadToolStripMenuItem.Text = "&Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // FormModelLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormModelLoader";
            this.Text = "Wavefront Model Loader";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainerMainAndOutput.Panel1.ResumeLayout(false);
            this.splitContainerMainAndOutput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainAndOutput)).EndInit();
            this.splitContainerMainAndOutput.ResumeLayout(false);
            this.splitContainerTreeAndDetails.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTreeAndDetails)).EndInit();
            this.splitContainerTreeAndDetails.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerMainAndOutput;
        private System.Windows.Forms.RichTextBox richTextBoxOutput;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBusyStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarBusy;
        private System.Windows.Forms.SplitContainer splitContainerTreeAndDetails;
        private System.Windows.Forms.TreeView treeViewModel;
    }
}

