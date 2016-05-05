namespace ProcessingTools.ListsManager
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Windows.Forms;

    using ProcessingTools.Configurator;

    public partial class MainForm : Form
    {
        private string configFileName;

        public MainForm()
        {
            this.InitializeComponent();
            this.OpenConfigFile(this.GetConfigFileNameFromApplicationConfigurations);

            this.blackListManager.ListGroupBoxLabel = "Black List";
            this.blackListManager.IsRankList = false;

            this.whiteListManager.ListGroupBoxLabel = "White List";
            this.whiteListManager.IsRankList = false;

            this.rankListManager.ListGroupBoxLabel = "Rank List";
            this.rankListManager.IsRankList = true;
        }

        internal static Config Config { get; set; }

        private string GetDefaultConfigFileName => ConfigurationManager.AppSettings["ConfigJsonFilePath"];

        private string GetConfigFileNameFromApplicationConfigurations
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ApplicationSettings.Default.ConfigurationFileName))
                {
                    ApplicationSettings.Default.ConfigurationFileName = this.GetDefaultConfigFileName;
                }

                return ApplicationSettings.Default.ConfigurationFileName;
            }
        }

        private string GetConfigFileNameDirectory => Path.GetDirectoryName(this.GetConfigFileNameFromApplicationConfigurations);

        private string ConfigFileName
        {
            get
            {
                return this.configFileName;
            }

            set
            {
                this.configFileName = value;
                ApplicationSettings.Default.ConfigurationFileName = this.configFileName;
            }
        }

        private void CloseConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConfigFileName = string.Empty;
            this.toolStripStatusLabelConfigOutput.Text = this.ConfigFileName;
        }

        private void OpenConfigFile()
        {
            this.OpenConfigFile(null);
        }

        private void OpenConfigFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
            {
                this.ConfigFileName = this.GetDefaultConfigFileName;
            }
            else
            {
                this.ConfigFileName = fileName;
            }

            try
            {
                Config = ConfigBuilder.Create(this.ConfigFileName);

                this.toolStripStatusLabelConfigOutput.Text = this.ConfigFileName;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, $"Unable to open config file '{this.ConfigFileName}.'");
                this.ConfigFileName = this.GetDefaultConfigFileName;
            }
        }

        private void OpenConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.openConfigFileDialog.InitialDirectory = this.GetConfigFileNameDirectory;
                this.openConfigFileDialog.AddExtension = true;
                this.openConfigFileDialog.CheckFileExists = true;
                this.openConfigFileDialog.CheckPathExists = true;
                this.openConfigFileDialog.Multiselect = false;
                this.openConfigFileDialog.FileName = string.Empty;
                this.openConfigFileDialog.Filter = $"{ApplicationSettings.Default.DefaultExtension} | *.{ApplicationSettings.Default.DefaultExtension}";
                this.openConfigFileDialog.DefaultExt = ApplicationSettings.Default.DefaultExtension;

                var dialogResult = this.openConfigFileDialog.ShowDialog();
                switch (dialogResult)
                {
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        this.OpenConfigFile(this.openConfigFileDialog.FileName);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void OpenDefaultConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpenConfigFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.Message);
            }
        }

        private void QiutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
