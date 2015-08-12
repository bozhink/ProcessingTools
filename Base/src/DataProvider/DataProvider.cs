using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public class DataProvider : TaggerBase, IDataProvider, IDisposable
    {
        private SqlConnection connection;

        public DataProvider(Config config, string xml)
            : base(config, xml)
        {
            OpenConnection();
        }

        public DataProvider(IBase baseObject)
            : base(baseObject)
        {
            OpenConnection();
        }

        ~DataProvider()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.connection.Dispose();
                this.connection.Close();
            }
        }

        public void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, TagContent tag, bool caseSensitive = false)
        {
            string patternTemplate = string.Empty;
            if (caseSensitive)
            {
                patternTemplate = "(?<!<[^>]+)\\b({0})\\b(?![^<>]*>)";
            }
            else
            {
                patternTemplate = "(?<!<[^>]+)\\b((?i){0})\\b(?![^<>]*>)";
            }

            try
            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);
                using (SqlCommand command = new SqlCommand(query, this.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SetTagAttributes(tag, reader);

                            string replacement = tag.OpenTag + "$1" + tag.CloseTag;
                            string contentString = reader.GetString(0);
                            string pattern = string.Format(patternTemplate, Regex.Replace(Regex.Escape(contentString), "'", "\\W"));
                            Regex matchEntry = new Regex(pattern);
                            foreach (XmlNode node in nodeList)
                            {
                                if (matchEntry.Match(node.InnerText).Success)
                                {
                                    node.InnerXml = matchEntry.Replace(node.InnerXml, replacement);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private static void SetTagAttributes(TagContent tag, SqlDataReader reader)
        {
            if (reader.FieldCount > 1)
            {
                StringBuilder attributes = new StringBuilder();
                for (int i = 1, len = reader.FieldCount; i < len; ++i)
                {
                    string xmlEscapedContent = Regex.Replace(Regex.Replace(reader.GetString(i), "&", "&amp;"), @"""", "&quot;");

                    attributes.Append(string.Format(@" attribute{0}=""{1}""", i, xmlEscapedContent));
                }

                tag.Attributes = attributes.ToString();
            }
            else
            {
                tag.Attributes = string.Empty;
            }
        }

        public void ExecuteSimpleReplaceUsingDatabase(string xpath, string query, string tagName)
        {
            TagContent tag = new TagContent(tagName);
            this.ExecuteSimpleReplaceUsingDatabase(xpath, query, tag);
        }

        private void OpenConnection()
        {
            try
            {
                string connectionString = this.Config.mainDictionaryDataSourceString;
                this.connection = new SqlConnection(connectionString);
                this.connection.Open();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 1);
            }
        }

        private IList<string> ListTables()
        {
            List<string> tables = new List<string>();
            DataTable dt = this.connection.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            return tables;
        }
    }
}
