// <copyright file="BlackListXmlModelTests.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Integration.Tests
{
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Common.Constants.Data.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Xml.Bio.Taxonomy;

    /// <summary>
    /// <see cref="BlackListXmlModel"/> tests.
    /// </summary>
    [TestClass]
    public class BlackListXmlModelTests
    {
        /// <summary>
        /// <see cref="BlackListXmlModel"/> deserialize should work.
        /// </summary>
        [TestMethod]
        public void BlackListXmlModel_Deserialize_ShouldWork()
        {
            const int NumberOfListItems = 2;

            string dataFilesDirectory = "DataFiles";
            string sampleFileName = "blacklist-sample.xml";

            string source = Path.Combine(Directory.GetCurrentDirectory(), dataFilesDirectory, sampleFileName);

            BlackListXmlModel list = null;
            using (var stream = new FileStream(source, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BlackListXmlModel));
                list = serializer.Deserialize(stream) as BlackListXmlModel;
            }

            Assert.IsNotNull(list, "BlackList object should not be null.");

            Assert.IsTrue(list.Items.Any(), "Number of items should be greater than zero.");

            Assert.AreEqual(NumberOfListItems, list.Items.Count(), $"The number of BlackList items should be {NumberOfListItems}.");

            var items = list.Items.ToArray();
            Assert.AreEqual("Abdominal", items[0], "First item should match.");
            Assert.AreEqual("Abbreviations", items[1], "Second item should match.");
        }

        /// <summary>
        /// <see cref="BlackListXmlModel"/> serialize should work.
        /// </summary>
        [TestMethod]
        public void BlackListXmlModel_Serialize_ShouldWork()
        {
            string[] items = new[]
            {
                "Abdominal",
                "Abbreviations",
            };

            var list = new BlackListXmlModel
            {
                Items = items,
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
