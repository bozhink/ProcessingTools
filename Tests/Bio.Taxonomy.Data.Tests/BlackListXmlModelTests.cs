namespace ProcessingTools.Bio.Taxonomy.Data.Tests
{
    using System.IO;
    using System.Xml.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Xml;

    [TestClass]
    public class BlackListXmlModelTests
    {
        [TestMethod]
        public void BlackList_ShouldDeserializeXmlCorrectly()
        {
            const int NumberOfListItems = 2;
            XmlSerializer serializer = new XmlSerializer(typeof(BlackList));
            BlackList list = null;

            using (var stream = new FileStream(@"DataFiles\blacklist-sample.xml", FileMode.Open))
            {
                list = serializer.Deserialize(stream) as BlackList;
            }

            Assert.IsNotNull(list, "BlackList object schould not be null.");

            Assert.IsTrue(list.Items.Length > 0, "Number of items should be greater than zero.");

            Assert.AreEqual(NumberOfListItems, list.Items.Length, $"The number of BlackList items schould be {NumberOfListItems}.");

            Assert.AreEqual("Abdominal", list.Items[0], "First item schould match.");
            Assert.AreEqual("Abbreviations", list.Items[1], "Second item schould match.");
        }
    }
}