namespace ProcessingTools.ListsManager
{
    using System;
    using System.Windows.Forms;
    using System.Xml;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
            this.ConfigFileName = string.Empty;
            this.BlackListFileName = string.Empty;
            this.WhiteListFileName = string.Empty;
            this.RankListFileName = string.Empty;
            this.CleanRankListFileName = string.Empty;
            this.CleanWhiteListFileName = string.Empty;
            this.CleanBlackListFileName = string.Empty;
            this.TempDir = string.Empty;

            this.blackListManager.ListGroupBoxLabel = "Black List";
            this.blackListManager.IsRankList = false;

            this.whiteListManager.ListGroupBoxLabel = "White List";
            this.whiteListManager.IsRankList = false;

            this.rankListManager.ListGroupBoxLabel = "Rank List";
            this.rankListManager.IsRankList = true;
        }

        public string BlackListFileName
        {
            get;
            set;
        }

        public string CleanBlackListFileName
        {
            get;
            set;
        }

        public string CleanRankListFileName
        {
            get;
            set;
        }

        public string CleanWhiteListFileName
        {
            get;
            set;
        }

        public string ConfigFileName
        {
            get;
            set;
        }

        public string RankListFileName
        {
            get;
            set;
        }

        public string TempDir
        {
            get;
            set;
        }

        public string WhiteListFileName
        {
            get;
            set;
        }

        private void CloseConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConfigFileName = string.Empty;
            this.rankListManager.ListFileName = string.Empty;
            this.whiteListManager.ListFileName = string.Empty;
            this.blackListManager.ListFileName = string.Empty;
            this.toolStripStatusLabelConfigOutput.Text = this.ConfigFileName;
        }

        private void OpenConfigFile(string fileName)
        {
            this.ConfigFileName = fileName;
            this.toolStripStatusLabelConfigOutput.Text = this.ConfigFileName;

            XmlDocument configXml = new XmlDocument();
            configXml.Load(this.ConfigFileName);

            // Parse config file
            this.BlackListFileName = configXml.ChildNodes.Item(1)["black-list-xml-file-path"].InnerText;
            this.WhiteListFileName = configXml.ChildNodes.Item(1)["white-list-xml-file-path"].InnerText;
            this.RankListFileName = configXml.ChildNodes.Item(1)["rank-list-xml-file-path"].InnerText;
            this.CleanRankListFileName = configXml.ChildNodes.Item(1)["rank-list-clean-xsl-path"].InnerText;
            this.CleanWhiteListFileName = configXml.ChildNodes.Item(1)["white-list-clean-xsl-path"].InnerText;
            this.CleanBlackListFileName = configXml.ChildNodes.Item(1)["black-list-clean-xsl-path"].InnerText;
            this.TempDir = configXml.ChildNodes.Item(1)["temp"].InnerText;

            // Set BlackList file paths
            this.blackListManager.ListFileName = this.BlackListFileName;
            this.blackListManager.CleanXslFileName = this.CleanBlackListFileName;
            this.blackListManager.TempDirectory = this.TempDir;

            // Set WhiteList file paths
            this.whiteListManager.ListFileName = this.WhiteListFileName;
            this.whiteListManager.CleanXslFileName = this.CleanWhiteListFileName;
            this.whiteListManager.TempDirectory = this.TempDir;

            // Set RankList file paths
            this.rankListManager.ListFileName = this.RankListFileName;
            this.rankListManager.CleanXslFileName = this.CleanRankListFileName;
            this.rankListManager.TempDirectory = this.TempDir;
        }

        private void OpenConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to open the config file as xml
            try
            {
                this.openConfigFileDialog.ShowDialog();
                if (this.openConfigFileDialog.FileName.Length > 0)
                {
                    this.OpenConfigFile(this.openConfigFileDialog.FileName);
                }
            }
            catch (Exception configException)
            {
                MessageBox.Show(configException.Message, $"Error during opening the config file '{this.ConfigFileName}.'");
            }
        }

        private void OpenDefaultConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpenConfigFile(@"C:\bin\config.xml");
            }
            catch (Exception configException)
            {
                MessageBox.Show(configException.Message, $"Error during opening the config file '{this.ConfigFileName}.'");
            }
        }

        private void QiutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}