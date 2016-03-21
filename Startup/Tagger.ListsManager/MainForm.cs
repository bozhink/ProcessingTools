namespace ProcessingTools.ListsManager
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Windows.Forms;

    using ProcessingTools.Configurator;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
            this.ConfigFileName = ConfigurationManager.AppSettings["ConfigJsonFilePath"];
            this.OpenConfigFile();

            this.blackListManager.ListGroupBoxLabel = "Black List";
            this.blackListManager.IsRankList = false;

            this.whiteListManager.ListGroupBoxLabel = "White List";
            this.whiteListManager.IsRankList = false;

            this.rankListManager.ListGroupBoxLabel = "Rank List";
            this.rankListManager.IsRankList = true;
        }

        private string ConfigFileName { get; set; }

        private Config Config { get; set; }

        private void CloseConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConfigFileName = string.Empty;
            this.rankListManager.ListFileName = string.Empty;
            this.whiteListManager.ListFileName = string.Empty;
            this.blackListManager.ListFileName = string.Empty;
            this.toolStripStatusLabelConfigOutput.Text = this.ConfigFileName;
        }

        private void OpenConfigFile()
        {
            this.Config = ConfigBuilder.Create(this.ConfigFileName);

            // Set BlackList file paths
            this.blackListManager.ListFileName = this.Config.BlackListXmlFilePath;
            this.blackListManager.CleanXslFileName = this.Config.BlackListCleanXslPath;

            // Set WhiteList file paths
            this.whiteListManager.ListFileName = this.Config.WhiteListXmlFilePath;
            this.whiteListManager.CleanXslFileName = this.Config.WhiteListCleanXslPath;

            // Set RankList file paths
            this.rankListManager.ListFileName = this.Config.RankListXmlFilePath;
            this.rankListManager.CleanXslFileName = this.Config.RankListCleanXslPath;

            this.toolStripStatusLabelConfigOutput.Text = this.ConfigFileName;
        }

        private void OpenConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.openConfigFileDialog.ShowDialog();
                if (!string.IsNullOrWhiteSpace(this.openConfigFileDialog.FileName) && File.Exists(this.openConfigFileDialog.FileName))
                {
                    this.ConfigFileName = this.openConfigFileDialog.FileName;
                    this.OpenConfigFile();
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
                this.ConfigFileName = ConfigurationManager.AppSettings["ConfigJsonFilePath"];
                this.OpenConfigFile();
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
