namespace ProcessingTools.ListsManager
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Xsl;

    public partial class ListManagerControl : UserControl
    {
        public ListManagerControl()
        {
            this.InitializeComponent();
            this.ListFileName = string.Empty;
            this.CleanXslFileName = string.Empty;
            this.TempDirectory = string.Empty;
            this.IsRankList = false;
        }

        /// <summary>
        /// Get or set the full-path name of the Xsl file which will be used to clean the x-list file
        /// </summary>
        public string CleanXslFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the boolean value which designates whether current views are rank-related or not
        /// </summary>
        public bool IsRankList
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the full-path name of the x-list
        /// </summary>
        public string ListFileName
        {
            get;
            set;
        }

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
        /// Get or set the TEMP directory path
        /// </summary>
        public string TempDirectory
        {
            get;
            set;
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
                    catch (Exception)
                    {
                        // Skip this item
                    }
                }
            }
            else
            {
                for (Match entriesMatch = Regex.Match(listEntriesTextBox.Text, @"\S+"); entriesMatch.Success; entriesMatch = entriesMatch.NextMatch())
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
                string fileName = this.TempDirectory + @"\" + Path.GetFileName(this.ListFileName);
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
            foreach (ListViewItem item in listView.SelectedItems)
            {
                this.listView.Items.Remove(item);
            }
        }

        private void ListImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ListFileName.Length > 0)
                {
                    XmlDocument listFileXml = new XmlDocument();
                    listFileXml.Load(this.ListFileName);

                    foreach (ListViewItem item in this.listView.Items)
                    {
                        XmlNode node = null;
                        if (this.IsRankList)
                        {
                            node = listFileXml.CreateElement("taxon");

                            XmlNode part = listFileXml.CreateElement("part");
                            XmlNode partValue = listFileXml.CreateElement("value");
                            XmlNode rank = listFileXml.CreateElement("rank");
                            XmlNode rankValue = listFileXml.CreateElement("value");

                            partValue.InnerText = item.SubItems[0].Text;
                            rankValue.InnerText = item.SubItems[1].Text;

                            rank.AppendChild(rankValue);
                            part.AppendChild(partValue);
                            part.AppendChild(rank);

                            node.AppendChild(part);
                        }
                        else
                        {
                            node = listFileXml.CreateElement("item");
                            node.InnerXml = item.Text;
                        }

                        listFileXml.FirstChild.AppendChild(node);
                    }

                    StreamWriter writer = new StreamWriter(this.ListFileName);
                    writer.Write(listFileXml.OuterXml);
                    writer.Close();
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
                    XmlDocument listFileXml = new XmlDocument();
                    listFileXml.Load(this.ListFileName);

                    if (this.IsRankList)
                    {
                        foreach (XmlNode taxon in listFileXml.SelectNodes("//taxon"))
                        {
                            foreach (XmlNode part in taxon.SelectNodes("part"))
                            {
                                string partValue = part["value"].InnerText;
                                foreach (XmlNode rank in part.SelectNodes("rank/value"))
                                {
                                    string[] taxonRankPair =
                                        {
                                            partValue,
                                            rank.InnerText
                                        };

                                    var listItem = new ListViewItem(taxonRankPair);
                                    this.listView.Items.Add(listItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (XmlNode item in listFileXml.SelectNodes("/*/*"))
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
                if (this.listSearchTextBox.Text.Trim().Length > 0)
                {
                    XmlDocument listFileXml = new XmlDocument();
                    listFileXml.Load(this.ListFileName);

                    if (this.IsRankList)
                    {
                        foreach (XmlNode taxon in listFileXml.SelectNodes($"//taxon[part/value[contains(normalize-space(.), '{this.listSearchTextBox.Text.Trim()}')]]"))
                        {
                            foreach (XmlNode part in taxon.SelectNodes("part"))
                            {
                                string partValue = part["value"].InnerText;
                                foreach (XmlNode rank in part.SelectNodes("rank/value"))
                                {
                                    string[] taxonRankPair = 
                                        {
                                            partValue,
                                            rank.InnerText
                                        };

                                    var listItem = new ListViewItem(taxonRankPair);
                                    listView.Items.Add(listItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (XmlNode item in listFileXml.SelectNodes($"/*/*[contains(normalize-space(.), '{this.listSearchTextBox.Text.Trim()}')]"))
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
