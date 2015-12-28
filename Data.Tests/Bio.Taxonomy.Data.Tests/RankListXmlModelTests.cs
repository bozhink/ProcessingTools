namespace ProcessingTools.Bio.Taxonomy.Data.Tests
{
    using System.IO;
    using System.Xml.Serialization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Xml;

    [TestClass]
    public class RankListXmlModelTests
    {
        [TestMethod]
        public void RankList_ShouldDeserializeXmlCorrectly()
        {
            const int NumberOfListItems = 2;
            XmlSerializer serializer = new XmlSerializer(typeof(RankList));
            RankList list = null;

            using (var stream = new FileStream(@"DataFiles\ranklist-sample.xml", FileMode.Open))
            {
                list = serializer.Deserialize(stream) as RankList;
            }

            Assert.IsNotNull(list, "RankList object should not be null.");

            Assert.IsTrue(list.Taxa.Length > 0, "Number of items should be greater than zero.");

            Assert.AreEqual(NumberOfListItems, list.Taxa.Length, $"The number of RankList items should be {NumberOfListItems}.");

            Assert.AreEqual("Aaroniellinae", list.Taxa[0].Parts[0].Value, "First taxon name should match.");
            Assert.AreEqual("subfamily", list.Taxa[0].Parts[0].Rank.Values[0], "First taxon rank should match.");

            Assert.AreEqual("Abacetini", list.Taxa[1].Parts[0].Value, "Second taxon name should match.");
            Assert.AreEqual("tribe", list.Taxa[1].Parts[0].Rank.Values[0], "Second taxon rank should match.");
        }
    }
}