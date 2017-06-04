namespace ProcessingTools.Nlm.Publishing.Tests.RegressionTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Enumerations.Nlm;

    [TestClass]
    public class ExternalLinkTypeTests
    {
        private const string UnexpectedValueMessage = "Unexpected Value.";

        [TestMethod]
        public void ExternalLinkType_AoiMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("aoi", ExternalLinkType.Aoi.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_DoiMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("doi", ExternalLinkType.Doi.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_EcMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("ec", ExternalLinkType.Ec.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_FtpMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("ftp", ExternalLinkType.Ftp.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_GenMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("gen", ExternalLinkType.Gen.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_GenpeptMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("genpept", ExternalLinkType.Genpept.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_HighwireMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("highwire", ExternalLinkType.Highwire.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_NlmTaMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("nlm-ta", ExternalLinkType.NlmTa.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PdbMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pdb", ExternalLinkType.Pdb.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PgrMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pgr", ExternalLinkType.Pgr.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PirMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pir", ExternalLinkType.Pir.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PirdbMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pirdb", ExternalLinkType.Pirdb.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PmcidMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pmcid", ExternalLinkType.Pmcid.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PmidMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pmid", ExternalLinkType.Pmid.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_SprotMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("sprot", ExternalLinkType.Sprot.GetName(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_UriMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("uri", ExternalLinkType.Uri.GetName(), UnexpectedValueMessage);
        }
    }
}
