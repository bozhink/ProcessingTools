namespace ProcessingTools.ListsManager
{
    using System;
    using System.Windows.Forms;
    using System.Xml;

    public partial class MainForm : Form
    {
        public string configFileName
        {
            get;
            set;
        }

        public string blackListFileName
        {
            get;
            set;
        }

        public string whiteListFileName
        {
            get;
            set;
        }

        public string rankListFileName
        {
            get;
            set;
        }

        public string cleanRankListFileName
        {
            get;
            set;
        }

        public string cleanWhiteListFileName
        {
            get;
            set;
        }

        public string cleanBlackListFileName
        {
            get;
            set;
        }

        public string tempDir
        {
            get;
            set;
        }

        public MainForm()
        {
            InitializeComponent();
            configFileName = string.Empty;
            blackListFileName = string.Empty;
            whiteListFileName = string.Empty;
            rankListFileName = string.Empty;
            cleanRankListFileName = string.Empty;
            cleanWhiteListFileName = string.Empty;
            cleanBlackListFileName = string.Empty;
            tempDir = string.Empty;

            blackListManager.listGroupBoxLabel = "Black List";
            blackListManager.isRankList = false;

            whiteListManager.listGroupBoxLabel = "White List";
            whiteListManager.isRankList = false;

            rankListManager.listGroupBoxLabel = "Rank List";
            rankListManager.isRankList = true;
        }

        private void openConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to open the config file as xml
            try
            {
                openConfigFileDialog.ShowDialog();
                if (openConfigFileDialog.FileName.Length > 0)
                {
                    OpenConfigFile(openConfigFileDialog.FileName);
                }
            }
            catch (Exception configException)
            {
                MessageBox.Show(configException.Message, "Error during opening the config file \'" + configFileName + "\'");
            }
        }

        private void openDefaultConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenConfigFile(@"C:\bin\config.xml");
            }
            catch (Exception configException)
            {
                MessageBox.Show(configException.Message, "Error during opening the config file \'" + configFileName + "\'");
            }
        }

        private void OpenConfigFile(string fileName)
        {
            configFileName = fileName;
            toolStripStatusLabelConfigOutput.Text = configFileName;

            XmlDocument configXml = new XmlDocument();
            configXml.Load(configFileName);

            // Parse config file
            blackListFileName = configXml.ChildNodes.Item(1)["black-list-xml-file-path"].InnerText;
            whiteListFileName = configXml.ChildNodes.Item(1)["white-list-xml-file-path"].InnerText;
            rankListFileName = configXml.ChildNodes.Item(1)["rank-list-xml-file-path"].InnerText;
            cleanRankListFileName = configXml.ChildNodes.Item(1)["rank-list-clean-xsl-path"].InnerText;
            cleanWhiteListFileName = configXml.ChildNodes.Item(1)["white-list-clean-xsl-path"].InnerText;
            cleanBlackListFileName = configXml.ChildNodes.Item(1)["black-list-clean-xsl-path"].InnerText;
            tempDir = configXml.ChildNodes.Item(1)["temp"].InnerText;

            // Set BlackList file paths
            blackListManager.listFileName = blackListFileName;
            blackListManager.cleanXslFileName = cleanBlackListFileName;
            blackListManager.tempDirectory = tempDir;

            // Set WhiteList file paths
            whiteListManager.listFileName = whiteListFileName;
            whiteListManager.cleanXslFileName = cleanWhiteListFileName;
            whiteListManager.tempDirectory = tempDir;

            // Set RankList file paths
            rankListManager.listFileName = rankListFileName;
            rankListManager.cleanXslFileName = cleanRankListFileName;
            rankListManager.tempDirectory = tempDir;
        }

        private void closeConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configFileName = string.Empty;
            rankListManager.listFileName = string.Empty;
            whiteListManager.listFileName = string.Empty;
            blackListManager.listFileName = string.Empty;
            toolStripStatusLabelConfigOutput.Text = configFileName;
        }

        private void qiutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
