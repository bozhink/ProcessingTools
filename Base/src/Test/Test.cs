using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;

namespace Base
{
    public class Test : Base
    {
        public Test() : base() { }
        public Test(string xml) : base(xml) { }

        public void ExtractSystemChecklistAuthority1()
        {
            ParseXmlStringToXmlDocument();
            XmlDocument newXml = new XmlDocument(namespaceManager.NameTable);
            XmlElement root = newXml.CreateElement("root");

            foreach (XmlNode node in xmlDocument.SelectNodes("//fields[taxon_authors_and_year[normalize-space(.)!='']]", namespaceManager))
            {
                XmlElement newNode = newXml.CreateElement("node");
                newNode.InnerXml = node["taxon_authors_and_year"].OuterXml;
                root.AppendChild(newNode);
            }

            newXml.AppendChild(root);

            xml = newXml.OuterXml;
        }

        public void ExtractSystemChecklistAuthority()
        {
            ParseXmlStringToXmlDocument();

            foreach (XmlNode node in xmlDocument.SelectNodes("//fields/taxon_authors_and_year/value[normalize-space(.)!='']", namespaceManager))
            {
                node.InnerText = Regex.Replace(node.InnerText, @"\s+and\s+", " &amp; ");
                node.InnerText = Regex.Replace(node.InnerText, @"(?<=[^,])\s+(?=\d)", ", ");
            }

            xml = xmlDocument.OuterXml;
        }

        public void TestEnvironments()
        {
            Environments envs = new Environments(config);
            envs.Xml = xml;

            Environments.EnvironmentRecord envr;
            envr.id = "1";
            envr.content = "122";
        }

        public void SqlSelect()
        {
            //
            using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\ProjectsV12;Initial Catalog=Environments;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                Alert.Message(connection.ConnectionString);
                Alert.Message(connection.Database);
                connection.Open();
                string query = "select Content, ContentId from dbo.environments_names order by LEN(Content) desc;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Alert.Message(reader.GetString(0) + " " + reader.GetInt32(1));
                        }
                    }
                }
                connection.Dispose();
                connection.Close();
            }
        }
    }
}
