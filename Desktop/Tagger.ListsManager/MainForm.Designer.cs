namespace ProcessingTools.ListsManager
{
    public partial class MainForm
    {
        private ListManagerControl blackListManager;

        private System.Windows.Forms.TabPage blackListTabPage;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip;

        private System.Windows.Forms.ToolStripMenuItem qiutToolStripMenuItem;

        private ListManagerControl rankListManager;

        private System.Windows.Forms.TabPage rankListTabPage;

        private System.Windows.Forms.TabControl tabControl;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qiutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.blackListTabPage = new System.Windows.Forms.TabPage();
            this.blackListManager = new ProcessingTools.ListsManager.ListManagerControl();
            this.rankListTabPage = new System.Windows.Forms.TabPage();
            this.rankListManager = new ProcessingTools.ListsManager.ListManagerControl();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.blackListTabPage.SuspendLayout();
            this.rankListTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(639, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qiutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // qiutToolStripMenuItem
            // 
            this.qiutToolStripMenuItem.Name = "qiutToolStripMenuItem";
            this.qiutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.qiutToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.qiutToolStripMenuItem.Text = "&Quit";
            this.qiutToolStripMenuItem.Click += new System.EventHandler(this.QiutToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.blackListTabPage);
            this.tabControl.Controls.Add(this.rankListTabPage);
            this.tabControl.Location = new System.Drawing.Point(13, 28);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(614, 563);
            this.tabControl.TabIndex = 0;
            // 
            // blackListTabPage
            // 
            this.blackListTabPage.Controls.Add(this.blackListManager);
            this.blackListTabPage.Location = new System.Drawing.Point(4, 22);
            this.blackListTabPage.Name = "blackListTabPage";
            this.blackListTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.blackListTabPage.Size = new System.Drawing.Size(606, 537);
            this.blackListTabPage.TabIndex = 2;
            this.blackListTabPage.Text = "BlackList";
            this.blackListTabPage.ToolTipText = "Edit rank list content";
            this.blackListTabPage.UseVisualStyleBackColor = true;
            // 
            // blackListManager
            // 
            this.blackListManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blackListManager.IsRankList = false;
            this.blackListManager.ListGroupBoxLabel = "listManagerGroupBox";
            this.blackListManager.Location = new System.Drawing.Point(6, 6);
            this.blackListManager.Name = "blackListManager";
            this.blackListManager.Size = new System.Drawing.Size(594, 525);
            this.blackListManager.TabIndex = 0;
            // 
            // rankListTabPage
            // 
            this.rankListTabPage.Controls.Add(this.rankListManager);
            this.rankListTabPage.Location = new System.Drawing.Point(4, 22);
            this.rankListTabPage.Name = "rankListTabPage";
            this.rankListTabPage.Size = new System.Drawing.Size(606, 537);
            this.rankListTabPage.TabIndex = 3;
            this.rankListTabPage.Text = "RankList";
            this.rankListTabPage.UseVisualStyleBackColor = true;
            // 
            // rankListManager
            // 
            this.rankListManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rankListManager.IsRankList = false;
            this.rankListManager.ListGroupBoxLabel = "listManagerGroupBox";
            this.rankListManager.Location = new System.Drawing.Point(3, 3);
            this.rankListManager.Name = "rankListManager";
            this.rankListManager.Size = new System.Drawing.Size(600, 531);
            this.rankListManager.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 616);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Tagger Lists Manager";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.blackListTabPage.ResumeLayout(false);
            this.rankListTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
