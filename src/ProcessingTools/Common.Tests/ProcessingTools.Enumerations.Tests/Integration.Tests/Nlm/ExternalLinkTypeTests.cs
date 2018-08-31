// <copyright file="ExternalLinkTypeTests.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Enumerations.Tests.Integration.Tests.Nlm
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Extensions;

    /// <summary>
    /// <see cref="ExternalLinkType" /> Tests.
    /// </summary>
    [TestClass]
    public class ExternalLinkTypeTests
    {
        /// <summary>
        /// <see cref="ExternalLinkType" /> Aoi member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_AoiMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("aoi", ExternalLinkType.Aoi.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Doi member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_DoiMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("doi", ExternalLinkType.Doi.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Ec member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_EcMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("ec", ExternalLinkType.Ec.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Ftp member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_FtpMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("ftp", ExternalLinkType.Ftp.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Gen member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_GenMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("gen", ExternalLinkType.Gen.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Genpept member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_GenpeptMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("genpept", ExternalLinkType.Genpept.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Highwire member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_HighwireMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("highwire", ExternalLinkType.Highwire.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Nlm Ta member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_NlmTaMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("nlm-ta", ExternalLinkType.NlmTa.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Pdb member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_PdbMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pdb", ExternalLinkType.Pdb.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Pgr member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_PgrMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pgr", ExternalLinkType.Pgr.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Pir member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_PirMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pir", ExternalLinkType.Pir.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Pirdb member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_PirdbMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pirdb", ExternalLinkType.Pirdb.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Pmcid member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_PmcidMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pmcid", ExternalLinkType.Pmcid.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Pmid member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_PmidMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("pmid", ExternalLinkType.Pmid.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Sprot member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_SprotMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("sprot", ExternalLinkType.Sprot.GetName());
        }

        /// <summary>
        /// <see cref="ExternalLinkType" /> Uri member should have correct display name.
        /// </summary>
        [TestMethod]
        public void ExternalLinkType_UriMember_ShouldHaveCorrectDisplayName()
        {
            Assert.AreEqual("uri", ExternalLinkType.Uri.GetName());
        }
    }
}
