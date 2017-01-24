namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Tests
{
    using System.Configuration;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;
    using ProcessingTools.Constants.Configuration;

    [TestClass]
    public class BlackListXmlModelTests
    {
        [TestMethod]
        public void BlackListXmlModel_Deserialize_ShouldWork()
        {
            const int NumberOfListItems = 2;
            string directoryFileName = ConfigurationManager.AppSettings[AppSettingsKeys.DataFilesDirectoryName];
            string fileName = ConfigurationManager.AppSettings["BlackListSampleFileName"];

            BlackListXmlModel list = null;
            using (var stream = new FileStream($"{directoryFileName}/{fileName}", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BlackListXmlModel));
                list = serializer.Deserialize(stream) as BlackListXmlModel;
            }

            Assert.IsNotNull(list, "BlackList object should not be null.");

            Assert.IsTrue(list.Items.Length > 0, "Number of items should be greater than zero.");

            Assert.AreEqual(NumberOfListItems, list.Items.Length, $"The number of BlackList items should be {NumberOfListItems}.");

            Assert.AreEqual("Abdominal", list.Items[0], "First item should match.");
            Assert.AreEqual("Abbreviations", list.Items[1], "Second item should match.");
        }

        [TestMethod]
        public void BlackListXmlModel_Serialize_ShouldWork()
        {
            string[] items = new string[]
            {
                "Abdominal",
                "Abbreviations"
            };

            var list = new BlackListXmlModel
            {
                Items = items
            };

            Assert.IsNotNull(list, "List object should not be null.");

            XmlDocument document = null;
            using (var stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BlackListXmlModel));
                serializer.Serialize(stream, list);

                stream.Position = 0;
                document = new XmlDocument();
                document.Load(stream);
            }

            Assert.IsNotNull(document, "XmlDocument should not be null");

            var root = document.DocumentElement;
            Assert.AreEqual(
                XmlModelsConstants.BlackListXmlRootNodeName,
                root.Name,
                $"Document root node name should be '{XmlModelsConstants.BlackListXmlRootNodeName}'.");

            Assert.AreEqual(items.Length, root.ChildNodes.Count, $"Number od child nodes should be {items.Length}.");

            for (int i = 0; i < items.Length; ++i)
            {
                XmlNode itemNode = root.ChildNodes[i];
                Assert.IsNotNull(itemNode, $"Items node #{i} should not be null.");

                Assert.AreEqual(
                    XmlModelsConstants.BlackListXmlItemElementName,
                    itemNode.Name,
                    $"Name of item node #{i} should be {XmlModelsConstants.BlackListXmlItemElementName}.");

                Assert.AreEqual(items[i], itemNode.InnerText, $"Value of item node #{i} should be {items[i]}.");
            }
        }
    }
}
