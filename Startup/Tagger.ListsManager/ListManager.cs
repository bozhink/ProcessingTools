namespace ProcessingTools.ListsManager
{
    using ProcessingTools.Bio.Taxonomy.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Repositories.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Xsl;

    public partial class ListManagerControl : UserControl
    {
        private readonly ITaxaRepository taxaRepository = new TaxaRepository();

        public ListManagerControl()
        {
            this.InitializeComponent();
            this.ListFileName = string.Empty;
            this.CleanXslFileName = string.Empty;
            this.IsRankList = false;
        }

        /// <summary>
        /// Get or set the full-path name of the Xsl file which will be used to clean the x-list file
        /// </summary>
        public string CleanXslFileName { get; set; }

        /// <summary>
        /// Get or set the boolean value which designates whether current views are rank-related or not
        /// </summary>
        public bool IsRankList { get; set; }

        /// <summary>
        /// Get or set the full-path name of the x-list
        /// </summary>
        public string ListFileName { get; set; }

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

        private void ClearXmlListFileButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            try
            {
                string fileName = Path.GetTempPath() + @"\" + Path.GetFileName(this.ListFileName);
                xslTransform.Load(this.CleanXslFileName);
                xslTransform.Transform(this.ListFileName, fileName);
                xslTransform.Transform(fileName, this.ListFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in clean.");
            }

            this.Enabled = true;
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
            try
            {
                if (string.IsNullOrWhiteSpace(this.ListFileName))
                {
                    throw new ApplicationException("Invalid list name.");
                }

                if (this.IsRankList)
                {
                    var taxa = this.listView.Items.Cast<ListViewItem>()
                        .GroupBy(i => i.SubItems[0].Text)
                        .Select(g => new Taxon
                        {
                            Name = g.Key,
                            Ranks = new HashSet<string>(g.Select(i => i.SubItems[1].Text))
                        });

                    foreach (var taxon in new HashSet<Taxon>(taxa))
                    {
                        this.taxaRepository.Add(taxon).Wait();
                    }

                    int numberOfWrittenItems = this.taxaRepository.SaveChanges().Result;
                }
                else
                {
                    var listHolder = new XmlListHolder(this.ListFileName);
                    listHolder.Load();

                    foreach (ListViewItem item in this.listView.Items)
                    {
                        XmlElement entry = listHolder.XmlDocument.CreateElement("item");
                        entry.InnerXml = item.Text;
                        listHolder.XmlDocument.DocumentElement.AppendChild(entry);
                    }

                    listHolder.Write();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in import.");
            }
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
            this.Search();
        }

        private void ListSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Search();
            }
        }

        private void LoadWholeListButton_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure?", "Load whole list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Enabled = false;
                try
                {
                    var listHolder = new XmlListHolder(this.ListFileName);
                    listHolder.Load();

                    if (this.IsRankList)
                    {
                        this.taxaRepository.All().Result
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
                        foreach (XmlNode item in listHolder.XmlDocument.SelectNodes("/*/*"))
                        {
                            this.listView.Items.Add(item.InnerText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error in load.");
                }

                this.Enabled = true;
            }
        }

        private void Search()
        {
            try
            {
                string textToSearch = this.listSearchTextBox.Text.Trim();

                if (textToSearch.Length > 0)
                {
                    var listHolder = new XmlListHolder(this.ListFileName);
                    listHolder.Load();

                    if (this.IsRankList)
                    {
                        this.taxaRepository.All().Result
                            .Where(t => t.Name.Contains(textToSearch))
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
                        foreach (XmlNode item in listHolder.XmlDocument.SelectNodes($"/*/*[contains(normalize-space(.), '{this.listSearchTextBox.Text.Trim()}')]"))
                        {
                            this.listView.Items.Add(item.InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in search.");
            }
        }
    }
}