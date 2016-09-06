namespace ProcessingTools.ListsManager
{
    public partial class ListManagerControl
    {
        private System.Windows.Forms.Button addToListViewButton;

        private System.Windows.Forms.Button clearListViewButton;

        private System.Windows.Forms.Button clearTextBoxButton;

        private System.Windows.Forms.ColumnHeader columnHeader;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

        private System.Windows.Forms.TextBox listEntriesTextBox;

        private System.Windows.Forms.GroupBox listGroupBox;

        private System.Windows.Forms.Button listImportButton;

        private System.Windows.Forms.GroupBox listManagerGroupBox;

        private System.Windows.Forms.Button listParseButton;

        private System.Windows.Forms.Button listSearchButton;

        private System.Windows.Forms.TextBox listSearchTextBox;

        private System.Windows.Forms.ListView listView;

        private System.Windows.Forms.ContextMenuStrip listViewContextMenuStrip;

        private System.Windows.Forms.GroupBox parseTextGroupBox;

        private System.Windows.Forms.ColumnHeader rankColumnHeader;

        private System.Windows.Forms.GroupBox searchGroupBox;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listManagerGroupBox = new System.Windows.Forms.GroupBox();
            this.listGroupBox = new System.Windows.Forms.GroupBox();
            this.clearListViewButton = new System.Windows.Forms.Button();
            this.listImportButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rankColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parseTextGroupBox = new System.Windows.Forms.GroupBox();
            this.clearTextBoxButton = new System.Windows.Forms.Button();
            this.addToListViewButton = new System.Windows.Forms.Button();
            this.listParseButton = new System.Windows.Forms.Button();
            this.listEntriesTextBox = new System.Windows.Forms.TextBox();
            this.searchGroupBox = new System.Windows.Forms.GroupBox();
            this.listSearchButton = new System.Windows.Forms.Button();
            this.listSearchTextBox = new System.Windows.Forms.TextBox();
            this.listManagerGroupBox.SuspendLayout();
            this.listGroupBox.SuspendLayout();
            this.listViewContextMenuStrip.SuspendLayout();
            this.parseTextGroupBox.SuspendLayout();
            this.searchGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // listManagerGroupBox
            // 
            this.listManagerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listManagerGroupBox.Controls.Add(this.listGroupBox);
            this.listManagerGroupBox.Controls.Add(this.parseTextGroupBox);
            this.listManagerGroupBox.Controls.Add(this.searchGroupBox);
            this.listManagerGroupBox.Location = new System.Drawing.Point(0, 0);
            this.listManagerGroupBox.Name = "listManagerGroupBox";
            this.listManagerGroupBox.Size = new System.Drawing.Size(590, 500);
            this.listManagerGroupBox.TabIndex = 0;
            this.listManagerGroupBox.TabStop = false;
            this.listManagerGroupBox.Text = "listManagerGroupBox";
            // 
            // listGroupBox
            // 
            this.listGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listGroupBox.Controls.Add(this.clearListViewButton);
            this.listGroupBox.Controls.Add(this.listImportButton);
            this.listGroupBox.Controls.Add(this.listView);
            this.listGroupBox.Location = new System.Drawing.Point(6, 230);
            this.listGroupBox.Name = "listGroupBox";
            this.listGroupBox.Size = new System.Drawing.Size(578, 264);
            this.listGroupBox.TabIndex = 17;
            this.listGroupBox.TabStop = false;
            this.listGroupBox.Text = "List View";
            // 
            // clearListViewButton
            // 
            this.clearListViewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearListViewButton.Location = new System.Drawing.Point(138, 234);
            this.clearListViewButton.Name = "clearListViewButton";
            this.clearListViewButton.Size = new System.Drawing.Size(88, 23);
            this.clearListViewButton.TabIndex = 17;
            this.clearListViewButton.Text = "Clear list view";
            this.clearListViewButton.UseVisualStyleBackColor = true;
            this.clearListViewButton.Click += new System.EventHandler(this.ClearListViewButton_Click);
            // 
            // listImportButton
            // 
            this.listImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listImportButton.BackColor = System.Drawing.Color.Red;
            this.listImportButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.listImportButton.Location = new System.Drawing.Point(6, 234);
            this.listImportButton.Name = "listImportButton";
            this.listImportButton.Size = new System.Drawing.Size(126, 23);
            this.listImportButton.TabIndex = 16;
            this.listImportButton.Text = "Import to List";
            this.listImportButton.UseVisualStyleBackColor = false;
            this.listImportButton.Click += new System.EventHandler(this.ListImportButton_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader,
            this.rankColumnHeader});
            this.listView.ContextMenuStrip = this.listViewContextMenuStrip;
            this.listView.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(6, 19);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(566, 209);
            this.listView.TabIndex = 15;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "List item";
            this.columnHeader.Width = 318;
            // 
            // rankColumnHeader
            // 
            this.rankColumnHeader.Text = "Rank";
            this.rankColumnHeader.Width = 133;
            // 
            // listViewContextMenuStrip
            // 
            this.listViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.listViewContextMenuStrip.Name = "listViewContextMenuStrip";
            this.listViewContextMenuStrip.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // parseTextGroupBox
            // 
            this.parseTextGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parseTextGroupBox.Controls.Add(this.clearTextBoxButton);
            this.parseTextGroupBox.Controls.Add(this.addToListViewButton);
            this.parseTextGroupBox.Controls.Add(this.listParseButton);
            this.parseTextGroupBox.Controls.Add(this.listEntriesTextBox);
            this.parseTextGroupBox.Location = new System.Drawing.Point(6, 19);
            this.parseTextGroupBox.Name = "parseTextGroupBox";
            this.parseTextGroupBox.Size = new System.Drawing.Size(383, 205);
            this.parseTextGroupBox.TabIndex = 16;
            this.parseTextGroupBox.TabStop = false;
            this.parseTextGroupBox.Text = "Enter list of words to be added in the list:";
            // 
            // clearTextBoxButton
            // 
            this.clearTextBoxButton.Location = new System.Drawing.Point(193, 177);
            this.clearTextBoxButton.Name = "clearTextBoxButton";
            this.clearTextBoxButton.Size = new System.Drawing.Size(75, 23);
            this.clearTextBoxButton.TabIndex = 15;
            this.clearTextBoxButton.Text = "Clear";
            this.clearTextBoxButton.UseVisualStyleBackColor = true;
            this.clearTextBoxButton.Click += new System.EventHandler(this.ClearTextBoxButton_Click);
            // 
            // addToListViewButton
            // 
            this.addToListViewButton.Location = new System.Drawing.Point(87, 177);
            this.addToListViewButton.Name = "addToListViewButton";
            this.addToListViewButton.Size = new System.Drawing.Size(100, 23);
            this.addToListViewButton.TabIndex = 14;
            this.addToListViewButton.Text = "Add to List View";
            this.addToListViewButton.UseVisualStyleBackColor = true;
            this.addToListViewButton.Click += new System.EventHandler(this.AddToListViewButton_Click);
            // 
            // listParseButton
            // 
            this.listParseButton.Location = new System.Drawing.Point(6, 177);
            this.listParseButton.Name = "listParseButton";
            this.listParseButton.Size = new System.Drawing.Size(75, 23);
            this.listParseButton.TabIndex = 13;
            this.listParseButton.Text = "Parse";
            this.listParseButton.UseVisualStyleBackColor = true;
            this.listParseButton.Click += new System.EventHandler(this.ListParseButton_Click);
            // 
            // listEntriesTextBox
            // 
            this.listEntriesTextBox.AcceptsReturn = true;
            this.listEntriesTextBox.AcceptsTab = true;
            this.listEntriesTextBox.AllowDrop = true;
            this.listEntriesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEntriesTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listEntriesTextBox.Location = new System.Drawing.Point(6, 19);
            this.listEntriesTextBox.Multiline = true;
            this.listEntriesTextBox.Name = "listEntriesTextBox";
            this.listEntriesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.listEntriesTextBox.Size = new System.Drawing.Size(371, 152);
            this.listEntriesTextBox.TabIndex = 12;
            // 
            // searchGroupBox
            // 
            this.searchGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchGroupBox.Controls.Add(this.listSearchButton);
            this.searchGroupBox.Controls.Add(this.listSearchTextBox);
            this.searchGroupBox.Location = new System.Drawing.Point(395, 19);
            this.searchGroupBox.Name = "searchGroupBox";
            this.searchGroupBox.Size = new System.Drawing.Size(189, 80);
            this.searchGroupBox.TabIndex = 15;
            this.searchGroupBox.TabStop = false;
            this.searchGroupBox.Text = "Search in list";
            // 
            // listSearchButton
            // 
            this.listSearchButton.BackColor = System.Drawing.Color.Green;
            this.listSearchButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.listSearchButton.Location = new System.Drawing.Point(6, 45);
            this.listSearchButton.Name = "listSearchButton";
            this.listSearchButton.Size = new System.Drawing.Size(75, 23);
            this.listSearchButton.TabIndex = 15;
            this.listSearchButton.Text = "Search";
            this.listSearchButton.UseVisualStyleBackColor = false;
            this.listSearchButton.Click += new System.EventHandler(this.ListSearchButton_Click);
            // 
            // listSearchTextBox
            // 
            this.listSearchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSearchTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listSearchTextBox.Location = new System.Drawing.Point(6, 19);
            this.listSearchTextBox.Name = "listSearchTextBox";
            this.listSearchTextBox.Size = new System.Drawing.Size(177, 23);
            this.listSearchTextBox.TabIndex = 14;
            this.listSearchTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListSearchTextBox_KeyPress);
            // 
            // ListManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listManagerGroupBox);
            this.Name = "ListManagerControl";
            this.Size = new System.Drawing.Size(590, 500);
            this.listManagerGroupBox.ResumeLayout(false);
            this.listGroupBox.ResumeLayout(false);
            this.listViewContextMenuStrip.ResumeLayout(false);
            this.parseTextGroupBox.ResumeLayout(false);
            this.parseTextGroupBox.PerformLayout();
            this.searchGroupBox.ResumeLayout(false);
            this.searchGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
