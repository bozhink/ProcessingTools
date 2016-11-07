namespace ProcessingTools.Tagger.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Tagger.Core;

    [TestClass]
    public class ProgramSettingsTests
    {
        private const string DefaultValueShouldBeFalseMessage = "Default value should be false.";
        private const string ValueShouldBeTrueMessage = "1. Value should be true.";
        private const string ValueShouldBeFalseMessage = "2. Value should be false.";

        private ProgramSettings programSettings;

        [TestInitialize]
        public void Init()
        {
            this.programSettings = new ProgramSettings();
        }

        [TestMethod]
        public void ProgramSettings_HigherStructrureXpathPropertyInNewInstance_ShouldBeDefault()
        {
            string defaultXpath = "//article";
            Assert.AreEqual(defaultXpath, this.programSettings.HigherStructrureXpath, "Default HigherStructrureXpath value should be //article");
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfHigherStructrureXpathProperty_ShouldBePersistent()
        {
            string xpath = "//i";

            {
                this.programSettings.HigherStructrureXpath = xpath;

                Assert.AreEqual(xpath, this.programSettings.HigherStructrureXpath, "1. HigherStructrureXpath and XPath should match.");
            }

            {
                this.programSettings.HigherStructrureXpath = null;

                Assert.IsNull(this.programSettings.HigherStructrureXpath, "2. HigherStructrureXpath value should be null.");
            }
        }

        [TestMethod]
        public void ProgramSettings_ExtractHigherTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfExtractHigherTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ExtractHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractHigherTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExtractHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ExtractLowerTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfExtractLowerTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ExtractLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExtractLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ExtractTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfExtractTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ExtractTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExtractTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ResolveMediaTypesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfResolveMediaTypesProperty_ShouldBePersistent()
        {
            this.programSettings.ResolveMediaTypes = true;
            Assert.IsTrue(this.programSettings.ResolveMediaTypes, ValueShouldBeTrueMessage);

            this.programSettings.ResolveMediaTypes = false;
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_FormatInitPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.InitialFormat, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfFormatInitProperty_ShouldBePersistent()
        {
            this.programSettings.InitialFormat = true;
            Assert.IsTrue(this.programSettings.InitialFormat, ValueShouldBeTrueMessage);

            this.programSettings.InitialFormat = false;
            Assert.IsFalse(this.programSettings.InitialFormat, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_FormatTreatPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.FormatTreat, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfFormatTreatProperty_ShouldBePersistent()
        {
            this.programSettings.FormatTreat = true;
            Assert.IsTrue(this.programSettings.FormatTreat, ValueShouldBeTrueMessage);

            this.programSettings.FormatTreat = false;
            Assert.IsFalse(this.programSettings.FormatTreat, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ParseBySectionPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseBySection, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfParseBySectionProperty_ShouldBePersistent()
        {
            this.programSettings.ParseBySection = true;
            Assert.IsTrue(this.programSettings.ParseBySection, ValueShouldBeTrueMessage);

            this.programSettings.ParseBySection = false;
            Assert.IsFalse(this.programSettings.ParseBySection, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ParseCoordsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseCoordinates, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfParseCoordsProperty_ShouldBePersistent()
        {
            this.programSettings.ParseCoordinates = true;
            Assert.IsTrue(this.programSettings.ParseCoordinates, ValueShouldBeTrueMessage);

            this.programSettings.ParseCoordinates = false;
            Assert.IsFalse(this.programSettings.ParseCoordinates, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ParseReferencesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseReferences, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfParseReferencesProperty_ShouldBePersistent()
        {
            this.programSettings.ParseReferences = true;
            Assert.IsTrue(this.programSettings.ParseReferences, ValueShouldBeTrueMessage);

            this.programSettings.ParseReferences = false;
            Assert.IsFalse(this.programSettings.ParseReferences, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ParseTreatmentMetaWithAphiaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithAphiaProperty_ShouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithAphia, ValueShouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ParseTreatmentMetaWithColPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithColProperty_ShouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithCol = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithCol, ValueShouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithCol = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ParseTreatmentMetaWithGbifPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithGbifProperty_ShouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithGbif, ValueShouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_QueryReplacePropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QueryReplace, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfQueryReplaceProperty_ShouldBePersistent()
        {
            this.programSettings.QueryReplace = true;
            Assert.IsTrue(this.programSettings.QueryReplace, ValueShouldBeTrueMessage);

            this.programSettings.QueryReplace = false;
            Assert.IsFalse(this.programSettings.QueryReplace, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_SplitHigherAboveGenusPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfSplitHigherAboveGenusProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherAboveGenus = true;
            Assert.IsTrue(this.programSettings.ParseHigherAboveGenus, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherAboveGenus = false;
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_SplitHigherBySuffixPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfSplitHigherBySuffixProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherBySuffix = true;
            Assert.IsTrue(this.programSettings.ParseHigherBySuffix, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherBySuffix = false;
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_SplitHigherWithAphiaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfSplitHigherWithAphiaProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithAphia, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_SplitHigherWithCoLPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfSplitHigherWithCoLProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherWithCoL = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithCoL, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherWithCoL = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_SplitHigherWithGbifPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfSplitHigherWithGbifProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithGbif, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagAbbrevPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagAbbreviations, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagAbbrevProperty_ShouldBePersistent()
        {
            this.programSettings.TagAbbreviations = true;
            Assert.IsTrue(this.programSettings.TagAbbreviations, ValueShouldBeTrueMessage);

            this.programSettings.TagAbbreviations = false;
            Assert.IsFalse(this.programSettings.TagAbbreviations, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagCodesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCodes, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagCodesProperty_ShouldBePersistent()
        {
            this.programSettings.TagCodes = true;
            Assert.IsTrue(this.programSettings.TagCodes, ValueShouldBeTrueMessage);

            this.programSettings.TagCodes = false;
            Assert.IsFalse(this.programSettings.TagCodes, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagCoordsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCoordinates, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagCoordsProperty_ShouldBePersistent()
        {
            this.programSettings.TagCoordinates = true;
            Assert.IsTrue(this.programSettings.TagCoordinates, ValueShouldBeTrueMessage);

            this.programSettings.TagCoordinates = false;
            Assert.IsFalse(this.programSettings.TagCoordinates, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagDoiPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDoi, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagDoiProperty_ShouldBePersistent()
        {
            this.programSettings.TagDoi = true;
            Assert.IsTrue(this.programSettings.TagDoi, ValueShouldBeTrueMessage);

            this.programSettings.TagDoi = false;
            Assert.IsFalse(this.programSettings.TagDoi, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagEnvironmentTermsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvironmentTerms, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagEnvironmentTermsProperty_ShouldBePersistent()
        {
            this.programSettings.TagEnvironmentTerms = true;
            Assert.IsTrue(this.programSettings.TagEnvironmentTerms, ValueShouldBeTrueMessage);

            this.programSettings.TagEnvironmentTerms = false;
            Assert.IsFalse(this.programSettings.TagEnvironmentTerms, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagEnvironmentTermsWithExtractPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvironmentTermsWithExtract, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagEnvironmentTermsWithExtractProperty_ShouldBePersistent()
        {
            this.programSettings.TagEnvironmentTermsWithExtract = true;
            Assert.IsTrue(this.programSettings.TagEnvironmentTermsWithExtract, ValueShouldBeTrueMessage);

            this.programSettings.TagEnvironmentTermsWithExtract = false;
            Assert.IsFalse(this.programSettings.TagEnvironmentTermsWithExtract, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagFigTabPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagFloats, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagFigTabProperty_ShouldBePersistent()
        {
            this.programSettings.TagFloats = true;
            Assert.IsTrue(this.programSettings.TagFloats, ValueShouldBeTrueMessage);

            this.programSettings.TagFloats = false;
            Assert.IsFalse(this.programSettings.TagFloats, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagReferencesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagReferences, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagReferencesProperty_ShouldBePersistent()
        {
            this.programSettings.TagReferences = true;
            Assert.IsTrue(this.programSettings.TagReferences, ValueShouldBeTrueMessage);

            this.programSettings.TagReferences = false;
            Assert.IsFalse(this.programSettings.TagReferences, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_RunXslTransformPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.RunXslTransform, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfRunXslTransformProperty_ShouldBePersistent()
        {
            this.programSettings.RunXslTransform = true;
            Assert.IsTrue(this.programSettings.RunXslTransform, ValueShouldBeTrueMessage);

            this.programSettings.RunXslTransform = false;
            Assert.IsFalse(this.programSettings.RunXslTransform, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagTableFnPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagTableFn, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagTableFnProperty_ShouldBePersistent()
        {
            this.programSettings.TagTableFn = true;
            Assert.IsTrue(this.programSettings.TagTableFn, ValueShouldBeTrueMessage);

            this.programSettings.TagTableFn = false;
            Assert.IsFalse(this.programSettings.TagTableFn, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TagWWWPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagWebLinks, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTagWWWProperty_ShouldBePersistent()
        {
            this.programSettings.TagWebLinks = true;
            Assert.IsTrue(this.programSettings.TagWebLinks, ValueShouldBeTrueMessage);

            this.programSettings.TagWebLinks = false;
            Assert.IsFalse(this.programSettings.TagWebLinks, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TaxaAPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTaxaAProperty_ShouldBePersistent()
        {
            this.programSettings.TagLowerTaxa = true;
            Assert.IsTrue(this.programSettings.TagLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.TagLowerTaxa = false;
            Assert.IsFalse(this.programSettings.TagLowerTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TaxaBPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTaxaBProperty_ShouldBePersistent()
        {
            this.programSettings.TagHigherTaxa = true;
            Assert.IsTrue(this.programSettings.TagHigherTaxa, ValueShouldBeTrueMessage);

            this.programSettings.TagHigherTaxa = false;
            Assert.IsFalse(this.programSettings.TagHigherTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TaxaCPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTaxaCProperty_ShouldBePersistent()
        {
            this.programSettings.ParseLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ParseLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ParseLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TaxaDPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTaxaDProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ParseHigherTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_TaxaEPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfTaxaEProperty_ShouldBePersistent()
        {
            this.programSettings.ExpandLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExpandLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExpandLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_UntagSplitPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.UntagSplit, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfUntagSplitProperty_ShouldBePersistent()
        {
            this.programSettings.UntagSplit = true;
            Assert.IsTrue(this.programSettings.UntagSplit, ValueShouldBeTrueMessage);

            this.programSettings.UntagSplit = false;
            Assert.IsFalse(this.programSettings.UntagSplit, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidateTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ValidateTaxa, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfValidateTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ValidateTaxa = true;
            Assert.IsTrue(this.programSettings.ValidateTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ValidateTaxa = false;
            Assert.IsFalse(this.programSettings.ValidateTaxa, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ZoobankCloneJsonPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfZoobankCloneJsonProperty_ShouldBePersistent()
        {
            this.programSettings.ZoobankCloneJson = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneJson, ValueShouldBeTrueMessage);

            this.programSettings.ZoobankCloneJson = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ZoobankCloneXmlPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfZoobankCloneXmlProperty_ShouldBePersistent()
        {
            this.programSettings.ZoobankCloneXml = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneXml, ValueShouldBeTrueMessage);

            this.programSettings.ZoobankCloneXml = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ZoobankGenerateRegistrationXmlPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, DefaultValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ValidChangesOfZoobankGenerateRegistrationXmlProperty_ShouldBePersistent()
        {
            this.programSettings.ZoobankGenerateRegistrationXml = true;
            Assert.IsTrue(this.programSettings.ZoobankGenerateRegistrationXml, ValueShouldBeTrueMessage);

            this.programSettings.ZoobankGenerateRegistrationXml = false;
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, ValueShouldBeFalseMessage);
        }

        [TestMethod]
        public void ProgramSettings_ArticleSchemaTypePropertyInNewInstance_ShouldBeSystem()
        {
            Assert.AreEqual(SchemaType.System, this.programSettings.ArticleSchemaType, "Default value of the ArticleSchemaType Property should be System.");
        }

        [TestMethod]
        public void ProgramSettings_FirstSetValueToSystemOfArticleSchemaTypePropertyBeforeGet_ShouldSetTheValueOfArticleSchemaTypePropertyToSystem()
        {
            this.programSettings.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(SchemaType.System, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be System.");
        }

        [TestMethod]
        public void ProgramSettings_FirstSetValueToNlmOfArticleSchemaTypePropertyBeforeGet_ShouldSetTheValueOfArticleSchemaTypePropertyToNlm()
        {
            this.programSettings.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(SchemaType.Nlm, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be Nlm.");
        }

        [TestMethod]
        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterGetInNewInstance_ShouldNotChangeTheDefaultValue()
        {
            var dafaultValue = this.programSettings.ArticleSchemaType;
            this.programSettings.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(dafaultValue, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be equal to the default value.");
        }

        [TestMethod]
        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterGetInNewInstance_ShouldNotChangeTheDefaultValue()
        {
            var dafaultValue = this.programSettings.ArticleSchemaType;
            this.programSettings.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(dafaultValue, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be equal to the default value.");
        }

        [TestMethod]
        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterInitialSet_SchemaTypeNlm_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.Nlm);
        }

        [TestMethod]
        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterInitialSet_SchemaTypeSystem_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.System);
        }

        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.programSettings.ArticleSchemaType = initialValue;
            this.programSettings.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(initialValue, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [TestMethod]
        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterInitialSet_SchemaTypeNlm_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.Nlm);
        }

        [TestMethod]
        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterInitialSet_SchemaTypeSystem_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.System);
        }

        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.programSettings.ArticleSchemaType = initialValue;
            this.programSettings.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(initialValue, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [TestMethod]
        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterGetAfterInitialSet_SchemaTypeNlm_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.Nlm);
        }

        [TestMethod]
        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterGetAfterInitialSet_SchemaTypeSystem_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.System);
        }

        public void ProgramSettings_SetValueToSystemOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.programSettings.ArticleSchemaType = initialValue;

            var lastValue = this.programSettings.ArticleSchemaType;
            Assert.AreEqual(initialValue, lastValue, "First set of values does not work correctly.");

            this.programSettings.ArticleSchemaType = SchemaType.System;
            Assert.AreEqual(initialValue, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }

        [TestMethod]
        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterGetAfterInitialSet_SchemaTypeNlm_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.Nlm);
        }

        [TestMethod]
        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterGetAfterInitialSet_SchemaTypeSystem_ShouldNotChangeTheInitialValue()
        {
            this.ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType.System);
        }

        public void ProgramSettings_SetValueToNlmOfArticleSchemaTypePropertyAfterGetAfterInitialSet_ShouldNotChangeTheInitialValue(SchemaType initialValue)
        {
            this.programSettings.ArticleSchemaType = initialValue;

            var lastValue = this.programSettings.ArticleSchemaType;
            Assert.AreEqual(initialValue, lastValue, "First set of values does not work correctly.");

            this.programSettings.ArticleSchemaType = SchemaType.Nlm;
            Assert.AreEqual(initialValue, this.programSettings.ArticleSchemaType, "ArticleSchemaType should be equal to the initial value.");
        }
    }
}
