namespace ProcessingTools.Bio.Taxonomy.Data.Tests
{
    using System.IO;
    using System.Xml.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Xml;

    [TestClass]
    public class WhiteListXmlModelTests
    {
        [TestMethod]
        public void WhiteList_ShouldDeserializeXmlCorrectly()
        {
            const int NumberOfListItems = 2;
            XmlSerializer serializer = new XmlSerializer(typeof(WhiteList));
            WhiteList list = null;

            using (var stream = new FileStream(@"DataFiles\whitelist-sample.xml", FileMode.Open))
            {
                list = serializer.Deserialize(stream) as WhiteList;
            }

            Assert.IsNotNull(list, "WhiteList object should not be null.");

            Assert.IsTrue(list.Items.Length > 0, "Number of items should be greater than zero.");

            Assert.AreEqual(NumberOfListItems, list.Items.Length, $"The number of WhiteList items should be {NumberOfListItems}.");

            Assert.AreEqual("Acalyptrata", list.Items[0], "First item should match.");
            Assert.AreEqual("Amycoida", list.Items[1], "Second item should match.");
        }
    }
}
