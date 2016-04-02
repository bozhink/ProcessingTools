namespace ProcessingTools.ListsManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;

    public partial class ListManagerControl : UserControl
    {
        public ListManagerControl()
        {
            this.InitializeComponent();
            this.IsRankList = false;
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
            this.Enabled = false;
            try
            {
                if (this.IsRankList)
                {
                    ITaxaRepository taxaRepository = new TaxaRepository(MainForm.Config);

                    var taxa = this.listView.Items.Cast<ListViewItem>()
                        .GroupBy(i => i.SubItems[0].Text)
                        .Select(g => new Taxon
                        {
                            Name = g.Key,
                            Ranks = new HashSet<string>(g.Select(i => i.SubItems[1].Text)),
                            IsWhiteListed = g.Any(i => i.Checked)
                        });

                    foreach (var taxon in new HashSet<Taxon>(taxa))
                    {
                        taxaRepository.Add(taxon).Wait();
                    }

                    int numberOfWrittenItems = taxaRepository.SaveChanges().Result;
                }
                else
                {
                    ITaxonomicBlackListRepository blackListRepository = new TaxonomicBlackListRepository(MainForm.Config);

                    var items = new HashSet<string>(this.listView.Items
                        .Cast<ListViewItem>()
                        .Select(i => i.Text));

                    foreach (var item in items)
                    {
                        blackListRepository.Add(item).Wait();
                    }

                    int numberOfWrittenItems = blackListRepository.SaveChanges().Result;
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

            this.Enabled = false;
            try
            {
                if (this.IsRankList)
                {
                    ITaxaRepository taxaRepository = new TaxaRepository(MainForm.Config);

                    taxaRepository.All().Result
                        .ToList()
                        .ForEach(taxon =>
                        {
                            foreach (var rank in taxon.Ranks)
                            {
                                string[] taxonRankPair = { taxon.Name, rank };
                                var listItem = new ListViewItem(taxonRankPair);
                                this.listView.Items.Add(listItem);
                            }
                        });
                }
                else
                {
                    ITaxonomicBlackListRepository blackListRepository = new TaxonomicBlackListRepository(MainForm.Config);

                    blackListRepository.All().Result
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
            this.Search();
        }

        private void ListSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Search();
            }
        }

        private void Search()
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
                    ITaxaRepository taxaRepository = new TaxaRepository(MainForm.Config);

                    taxaRepository.All().Result
                        .Where(t => t.Name.Contains(textToSearch))
                        .ToList()
                        .ForEach(taxon =>
                        {
                            foreach (var rank in taxon.Ranks)
                            {
                                string[] taxonRankPair = { taxon.Name, rank };
                                var listItem = new ListViewItem(taxonRankPair);
                                if (taxon.IsWhiteListed)
                                {
                                    listItem.Checked = true;
                                }

                                this.listView.Items.Add(listItem);
                            }
                        });
                }
                else
                {
                    ITaxonomicBlackListRepository blackListRepository = new TaxonomicBlackListRepository(MainForm.Config);

                    blackListRepository.All().Result
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