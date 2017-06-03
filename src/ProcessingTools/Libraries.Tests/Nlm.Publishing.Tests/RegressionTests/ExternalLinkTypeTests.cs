namespace ProcessingTools.Nlm.Publishing.Tests.RegressionTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Nlm.Publishing.Types;

    [TestClass]
    public class ExternalLinkTypeTests
    {
        private const string UnexpectedValueMessage = "Unexpected Value.";

        [TestMethod]
        public void ExternalLinkType_AoiMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("aoi", ExternalLinkType.Aoi.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_DoiMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("doi", ExternalLinkType.Doi.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_EcMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("ec", ExternalLinkType.Ec.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_FtpMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("ftp", ExternalLinkType.Ftp.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_GenMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("gen", ExternalLinkType.Gen.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_GenpeptMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("genpept", ExternalLinkType.Genpept.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_HighwireMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("highwire", ExternalLinkType.Highwire.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_NlmTaMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("nlm-ta", ExternalLinkType.NlmTa.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PdbMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pdb", ExternalLinkType.Pdb.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PgrMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pgr", ExternalLinkType.Pgr.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PirMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pir", ExternalLinkType.Pir.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PirdbMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pirdb", ExternalLinkType.Pirdb.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PmcidMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pmcid", ExternalLinkType.Pmcid.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_PmidMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("pmid", ExternalLinkType.Pmid.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_SprotMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("sprot", ExternalLinkType.Sprot.GetValue(), UnexpectedValueMessage);
        }

        [TestMethod]
        public void ExternalLinkType_UriMember_ShouldHaveCorrectStringValue()
        {
            Assert.AreEqual("uri", ExternalLinkType.Uri.GetValue(), UnexpectedValueMessage);
        }
    }
}
