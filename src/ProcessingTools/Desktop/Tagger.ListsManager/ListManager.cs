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
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;
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
        /// Gets or sets a value indicating whether get or set the boolean value which designates whether current views are rank-related or not
        /// </summary>
        public bool IsRankList { get; set; }

        /// <summary>
        /// Gets or sets get or set the name of the main group box of this control
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
                var items = new HashSet<KeyValuePair<string, string>>();

                for (Match entriesMatch = Regex.Match(this.listEntriesTextBox.Text, @"\S+\s+\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
                {
                    try
                    {
                        var taxonRankPair = new KeyValuePair<string, string>(
                            Regex.Match(entriesMatch.Value, @"\S+").Value,
                            Regex.Match(entriesMatch.Value, @"\S+").NextMatch().Value.ToLowerInvariant());

                        items.Add(taxonRankPair);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                items.OrderBy(i => i.Key)
                    .ToList()
                    .ForEach(i =>
                    {
                        var item = new ListViewItem(new[] { i.Key, i.Value });
                        this.listView.Items.Add(item);
                    });
            }
            else
            {
                var items = new HashSet<string>();

                for (Match entriesMatch = Regex.Match(this.listEntriesTextBox.Text, @"\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
                {
                    items.Add(entriesMatch.Value);
                }

                items.OrderBy(i => i)
                    .ToList()
                    .ForEach(i => this.listView.Items.Add(new ListViewItem(i)));
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
            this.ImportData().GetAwaiter();
        }

        private async Task ImportData()
        {
            this.Enabled = false;
            try
            {
                if (this.IsRankList)
                {
                    var service = this.kernel.Get<ITaxonRankDataService>();

                    var taxa = new HashSet<TaxonRankServiceModel>(this.listView.Items
                        .Cast<ListViewItem>()
                        .Select(i => new TaxonRankServiceModel
                        {
                            ScientificName = i.SubItems[0].Text,
                            Rank = i.SubItems[1].Text.MapTaxonRankStringToTaxonRankType()
                        }))
                        .ToArray();

                    await service.Add(taxa).ConfigureAwait(false);
                }
                else
                {
                    var service = this.kernel.Get<IBlackListDataService>();

                    var items = new HashSet<string>(this.listView.Items
                        .Cast<ListViewItem>()
                        .Select(i => i.Text))
                        .ToArray();

                    await service.Add(items).ConfigureAwait(false);
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

        private void ListSearchButton_Click(object sender, EventArgs e)
        {
            this.Search().GetAwaiter();
        }

        private void ListSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Search().GetAwaiter();
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
                    var service = this.kernel.Get<ITaxonRankSearchService>();

                    var foundTaxa = await service.Search(textToSearch).ConfigureAwait(false);
                    foreach (var taxon in foundTaxa)
                    {
                        string scientificName = taxon.ScientificName;
                        string rank = taxon.Rank.MapTaxonRankTypeToTaxonRankString();

                        string[] taxonRankPair = { scientificName, rank };
                        var listItem = new ListViewItem(taxonRankPair);
                        this.listView.Items.Add(listItem);
                    }
                }
                else
                {
                    var service = this.kernel.Get<IBlackListSearchService>();

                    var items = await service.Search(textToSearch).ConfigureAwait(false);
                    foreach (var item in items)
                    {
                        this.listView.Items.Add(item);
                    }
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
