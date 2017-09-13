namespace ProcessingTools.Data.Miners.Tests.Integration.Tests.Miners.Dates
{
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using ProcessingTools.Data.Miners.Miners.Dates;

    [TestFixture(Author = "Bozhin Karaivanov", Category = "Integration", TestOf = typeof(DatesDataMiner))]
    public class DatesDataMinerIntegrationTests
    {
        [Test(Author = "Bozhin Karaivanov", TestOf = typeof(DatesDataMiner), Description = "DatesDataMiner with text with single date should work.")]
        [TestCase(@"01. Oct. 1930")]
        [TestCase(@"11.vii.9.viii.1961")]
        [TestCase(@"13-III-08-V-1998")]
        [TestCase(@"15th October 2014")]
        [TestCase(@"19/August/2002")]
        [TestCase(@"1996.08.29")]
        [TestCase(@"1996.2.4")]
        [TestCase(@"2012/12/10")]
        [TestCase(@"2014.04.24")]
        [TestCase(@"23.ix.5.x.2009")]
        [TestCase(@"24.- 29.09.1929")]
        [TestCase(@"24Nov2001")]
        [TestCase(@"26.ix.5.x.2006")]
        [TestCase(@"26–30. June, 2014")]
        [TestCase(@"29th of April, 2015")]
        [TestCase(@"2nd March 2015")]
        [TestCase(@"3. - 6. vi. 2015")]
        [TestCase(@"3.-6. vi. 2015")]
        [TestCase(@"30Aug1923")]
        [TestCase(@"5 September–10 October")]
        [TestCase(@"9.iv.19.v.2007")]
        [TestCase(@"January 1st 1930")]
        [TestCase(@"July 01.2015")]
        [TestCase(@"June-July 2011")]
        [TestCase(@"June-July 2011–2013")]
        [TestCase(@"X. 13, 1965")]
        [TestCase(@"16.6.2013")]
        [TestCase(@"1999-07-27")]
        [TestCase(@"March 12.2014")]
        [TestCase(@"22–25.I.2007")]
        [TestCase(@"2011.IX.27–29")]
        [TestCase(@"2012.VIII–X")]
        [TestCase(@"24–30 March 2013")]
        [TestCase(@"18 Jan 2008")]
        [Timeout(8000)]
        public async Task DatesDataMiner_WithTextWithSingleDate_ShouldWork(string dateText)
        {
            // Arrange
            string content = dateText;
            var miner = new DatesDataMiner();

            // Act
            var result = await miner.MineAsync(content).ConfigureAwait(false);

            // Assert
            Assert.IsTrue(result.Any(), "Number of dates found should be greater than 0.");

            var bestMatch = result.OrderByDescending(i => i.Length).First();
            Assert.AreEqual(dateText, bestMatch);
        }
    }
}
