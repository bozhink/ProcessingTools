namespace ProcessingTools.Harvesters.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AbbreviationsHarvesterTests
    {
        [TestMethod]
        public void AbbreviationsHarvester_WithDefaultConstructor_ShouldReturnValidObject()
        {
            var harvester = new AbbreviationsHarvester();

            Assert.IsNotNull(harvester, "Harvester should not be null");
        }
    }
}