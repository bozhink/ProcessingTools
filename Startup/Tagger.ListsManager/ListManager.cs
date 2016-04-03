namespace ProcessingTools.ListsManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Ninject;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;

    using Settings;

    public partial class ListManagerControl : UserControl
    {
        private readonly IKernel kernel;

        public ListManagerControl()
        {
            this.InitializeComponent();
            this.IsRankList = false;
            this.kernel = NinjectConfig.CreateKernel();
        }

        /// <summary>
        /// Get or set the boolean value which designates whether current views are rank-related or not
        /// </summary>
        public bool IsRankList { get; set; }

        /// <summary>
        /// Get or set the name of the main group box of this control
        /// </summary>
        public string ListGroupBoxLabel
        {
            get
            {
                return this.listManagerGroupBox.Text;
            }

            set
            {
                this.listManagerGroupBox.Text = value;
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.kernel != null)
                {
                    this.kernel.Dispose();
                }

                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void AddToListViewButton_Click(object sender, EventArgs e)
        {
            if (this.IsRankList)
            {
                for (Match entriesMatch = Regex.Match(this.listEntriesTextBox.Text, @"\S+\s+\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
                {
                    try
                    {
                        string[] taxonRankPair =
                            {
                                Regex.Match(entriesMatch.Value, @"\S+").Value,
                                Regex.Match(entriesMatch.Value, @"\S+").NextMatch().Value.ToLower()
                            };

                        var item = new ListViewItem(taxonRankPair);
                        this.listView.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                for (Match entriesMatch = Regex.Match(this.listEntriesTextBox.Text, @"\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
                {
                    this.listView.Items.Add(new ListViewItem(entriesMatch.Value));
                }
            }
        }

        private void ClearListViewButton_Click(object sender, EventArgs e)
        {
            this.listView.Items.Clear();
        }

        private void ClearTextBoxButton_Click(object sender, EventArgs e)
        {
            this.listEntriesTextBox.Text = string.Empty;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView.SelectedItems)
            {
                this.listView.Items.Remove(item);
            }
        }

        private void ListImportButton_Click(object sender, EventArgs e)
        {
            var awaiter = this.ImportData().GetAwaiter();
        }

        private async Task ImportData()
        {
            this.Enabled = false;
            try
            {
                if (this.IsRankList)
                {
                    var service = this.kernel.Get<ITaxonRankDataService>();

                    var taxa = new HashSet<TaxonRankServiceModel>(this.listView.Items.Cast<ListViewItem>()
                        .Select(i => new TaxonRankServiceModel
                        {
                            ScientificName = i.SubItems[0].Text,
                            Rank = i.SubItems[1].Text,
                            IsWhiteListed = i.Checked
                        }));

                    foreach (var taxon in taxa)
                    {
                        await service.Add(taxon);
                    }
                }
                else
                {
                    var service = this.kernel.Get<ITaxonomicBlackListDataService>();

                    var items = new HashSet<string>(this.listView.Items
                        .Cast<ListViewItem>()
                        .Select(i => i.Text));

                    foreach (var item in items)
                    {
                        await service.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in import.");
            }

            this.Enabled = true;
        }

        private void ListParseButton_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                this.listEntriesTextBox.Text = Regex.Replace(this.listEntriesTextBox.Text, @"[^\w-]+", " ");
                if (this.IsRankList)
                {
                    for (Match entriesMatch = Regex.Match(this.listEntriesTextBox.Text, @"\S+\s+\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
                    {
                        result.Append(entriesMatch.Value);
                        result.Append("\r\n");
                    }
                }
                else
                {
                    for (Match entriesMatch = Regex.Match(this.listEntriesTextBox.Text, @"\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
                    {
                        result.Append(entriesMatch.Value);
                        result.Append("\r\n");
                    }
                }

                this.listEntriesTextBox.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in parse.");
            }
        }

        private void LoadWholeListButton_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show(
                "Are you sure?",
                "Load whole list",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            var awaiter = this.LoadAllData().GetAwaiter();
        }

        private async Task LoadAllData()
        {
            this.Enabled = false;
            try
            {
                if (this.IsRankList)
                {
                    var service = this.kernel.Get<ITaxonRankDataService>();

                    (await service.All())
                        .ToList()
                        .ForEach(taxon =>
                        {
                            string[] taxonRankPair = { taxon.ScientificName, taxon.Rank };
                            var listItem = new ListViewItem(taxonRankPair);
                            listItem.Checked = taxon.IsWhiteListed;
                            this.listView.Items.Add(listItem);
                        });
                }
                else
                {
                    var service = this.kernel.Get<ITaxonomicBlackListDataService>();

                    (await service.All())
                        .ToList()
                        .ForEach(item =>
                        {
                            this.listView.Items.Add(item);
                        });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in load.");
            }

            this.Enabled = true;
        }

        private void ListSearchButton_Click(object sender, EventArgs e)
        {
            var awaiter = this.Search().GetAwaiter();
        }

        private void ListSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                var awaiter = this.Search().GetAwaiter();
            }
        }

        private async Task Search()
        {
            string textToSearch = this.listSearchTextBox.Text?.Trim() ?? string.Empty;
            if (textToSearch.Length < 1)
            {
                return;
            }

            this.Enabled = false;
            try
            {
                if (this.IsRankList)
                {
                    var service = this.kernel.Get<ITaxonRankDataService>();

                    (await service.All())
                        .Where(t => t.ScientificName.Contains(textToSearch))
                        .ToList()
                        .ForEach(taxon =>
                        {
                            string[] taxonRankPair = { taxon.ScientificName, taxon.Rank };
                            var listItem = new ListViewItem(taxonRankPair);
                            listItem.Checked = taxon.IsWhiteListed;
                            this.listView.Items.Add(listItem);
                        });
                }
                else
                {
                    var service = this.kernel.Get<ITaxonomicBlackListDataService>();

                    (await service.All())
                        .Where(i => i.Contains(textToSearch))
                        .ToList()
                        .ForEach(item =>
                        {
                            this.listView.Items.Add(item);
                        });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in search.");
            }

            this.Enabled = true;
        }
    }
}