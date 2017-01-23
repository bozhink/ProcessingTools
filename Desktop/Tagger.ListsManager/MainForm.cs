namespace ProcessingTools.ListsManager
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();

            this.blackListManager.ListGroupBoxLabel = "Black List";
            this.blackListManager.IsRankList = false;

            this.rankListManager.ListGroupBoxLabel = "Rank List";
            this.rankListManager.IsRankList = true;
        }

        private void QiutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
