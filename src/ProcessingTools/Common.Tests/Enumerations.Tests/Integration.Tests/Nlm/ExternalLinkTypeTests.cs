namespace ProcessingTools.Enumerations.Tests.Integration.Tests.Nlm
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Enumerations.Nlm;

    [TestClass]
    public class ExternalLinkTypeTests
    {
        [TestMethod]
        public void ExternalLinkType_AoiMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("aoi", ExternalLinkType.Aoi.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_DoiMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("doi", ExternalLinkType.Doi.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_EcMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("ec", ExternalLinkType.Ec.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_FtpMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("ftp", ExternalLinkType.Ftp.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_GenMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("gen", ExternalLinkType.Gen.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_GenpeptMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("genpept", ExternalLinkType.Genpept.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_HighwireMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("highwire", ExternalLinkType.Highwire.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_NlmTaMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("nlm-ta", ExternalLinkType.NlmTa.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_PdbMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pdb", ExternalLinkType.Pdb.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_PgrMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pgr", ExternalLinkType.Pgr.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_PirMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pir", ExternalLinkType.Pir.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_PirdbMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pirdb", ExternalLinkType.Pirdb.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_PmcidMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pmcid", ExternalLinkType.Pmcid.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_PmidMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pmid", ExternalLinkType.Pmid.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_SprotMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("sprot", ExternalLinkType.Sprot.GetName());
        }

        [TestMethod]
        public void ExternalLinkType_UriMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("uri", ExternalLinkType.Uri.GetName());
        }
    }
}
