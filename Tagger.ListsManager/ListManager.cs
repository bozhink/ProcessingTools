namespace ProcessingTools.ListsManager
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml;
    using System.Xml.Xsl;

    public partial class ListManagerControl : UserControl
    {
        /// <summary>
        /// Get or set the full-path name of the x-list
        /// </summary>
        public string listFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the full-path name of the Xsl file which will be used to clean the x-list file
        /// </summary>
        public string cleanXslFileName
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the TEMP directory path
        /// </summary>
        public string tempDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the name of the main group box of this control
        /// </summary>
        public string listGroupBoxLabel
        {
            get
            {
                return listManagerGroupBox.Text;
            }
            set
            {
                listManagerGroupBox.Text = value;
            }
        }

        /// <summary>
        /// Get or set the boolean value which designates whether current views are rank-related or not
        /// </summary>
        public bool isRankList
        {
            get;
            set;
        }

        public ListManagerControl()
        {
            InitializeComponent();
            listFileName = string.Empty;
            cleanXslFileName = string.Empty;
            tempDirectory = string.Empty;
            isRankList = false;
        }

        private void listParseButton_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                if (isRankList)
                {
                    for (Match m = Regex.Match(listEntriesTextBox.Text, "\\S+\\s+\\S+"); m.Success; m = m.NextMatch())
                    {
                        result.Append(m.Value);
                        result.Append("\r\n");
                    }
                }
                else
                {
                    for (Match m = Regex.Match(listEntriesTextBox.Text, "\\S+"); m.Success; m = m.NextMatch())
                    {
                        result.Append(m.Value);
                        result.Append("\r\n");
                    }
                }
                listEntriesTextBox.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in parse.");
            }

        }

        private void addToListViewButton_Click(object sender, EventArgs e)
        {
            if (isRankList)
            {
                for (Match m = Regex.Match(listEntriesTextBox.Text, "\\S+\\s+\\S+"); m.Success; m = m.NextMatch())
                {
                    try
                    {
                        string[] x = { Regex.Match(m.Value, "\\S+").Value, Regex.Match(m.Value, "\\S+").NextMatch().Value.ToLower() };
                        ListViewItem item = new ListViewItem(x);
                        listView.Items.Add(item);
                    }
                    catch (Exception)
                    {
                        // Skip this item
                    }

                }
            }
            else
            {
                for (Match m = Regex.Match(listEntriesTextBox.Text, "\\S+"); m.Success; m = m.NextMatch())
                {
                    listView.Items.Add(new ListViewItem(m.Value));
                }
            }
        }

        private void listSearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void listSearchTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Search();
            }
        }

        private void Search()
        {
            try
            {
                if (listSearchTextBox.Text.Trim().Length > 0)
                {
                    XmlDocument listFileXml = new XmlDocument();
                    listFileXml.Load(listFileName);

                    if (isRankList)
                    {
                        foreach (XmlNode taxon in listFileXml.SelectNodes("//taxon[part/value[contains(normalize-space(.), '" + listSearchTextBox.Text.Trim() + "')]]"))
                        {
                            foreach (XmlNode part in taxon.SelectNodes("part"))
                            {
                                string partValue = part["value"].InnerText;
                                foreach (XmlNode rank in part.SelectNodes("rank/value"))
                                {
                                    string[] x = { partValue, rank.InnerText };
                                    ListViewItem listItem = new ListViewItem(x);
                                    listView.Items.Add(listItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (XmlNode item in listFileXml.SelectNodes("/*/*[contains(normalize-space(.), '" + listSearchTextBox.Text.Trim() + "')]"))
                        {
                            listView.Items.Add(item.InnerText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in search.");
            }
        }

        private void loadWholeListButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Load whole list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Enabled = false;
                try
                {
                    XmlDocument listFileXml = new XmlDocument();
                    listFileXml.Load(listFileName);

                    if (isRankList)
                    {
                        foreach (XmlNode taxon in listFileXml.SelectNodes("//taxon"))
                        {
                            foreach (XmlNode part in taxon.SelectNodes("part"))
                            {
                                string partValue = part["value"].InnerText;
                                foreach (XmlNode rank in part.SelectNodes("rank/value"))
                                {
                                    string[] x = { partValue, rank.InnerText };
                                    ListViewItem listItem = new ListViewItem(x);
                                    listView.Items.Add(listItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (XmlNode item in listFileXml.SelectNodes("/*/*"))
                        {
                            listView.Items.Add(item.InnerText);
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

        private void listImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (listFileName.Length > 0)
                {
                    XmlDocument listFileXml = new XmlDocument();
                    listFileXml.Load(listFileName);

                    foreach (ListViewItem item in listView.Items)
                    {
                        XmlNode node = null;
                        if (isRankList)
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

                    StreamWriter writer = new StreamWriter(listFileName);
                    writer.Write(listFileXml.OuterXml);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in import.");
            }
        }

        private void clearListViewButton_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
        }

        private void clearXmlListFileButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            try
            {
                string fileName = tempDirectory + @"\" + Path.GetFileName(listFileName);
                xslTransform.Load(cleanXslFileName);
                xslTransform.Transform(listFileName, fileName);
                xslTransform.Transform(fileName, listFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in clean.");
            }
            this.Enabled = true;
        }

        private void clearTextBoxButton_Click(object sender, EventArgs e)
        {
            listEntriesTextBox.Text = string.Empty;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                listView.Items.Remove(item);
            }
        }

    }
}
