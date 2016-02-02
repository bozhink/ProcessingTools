namespace ProcessingTools.MainProgram.Tests.UnitTests
{
    using NUnit.Framework;
    using ProcessingTools.Configurator;

    [TestFixture]
    public class ProgramSettingsTests
    {
        private const string DefaultValueShouldBeFalseMessage = "Default value should be false.";
        private const string ValueShouldBeTrueMessage = "1. Value should be true.";
        private const string ValueShouldBeFalseMessage = "2. Value should be false.";

        private ProgramSettings programSettings;

        [SetUp]
        public void Init()
        {
            this.programSettings = new ProgramSettings();
        }

        [Test]
        public void ProgramSettings_ConfigPropertyInNewInstance_ShouldBeNull()
        {
            Assert.IsNull(this.programSettings.Config, "Default Config value should be null.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfConfigProperty_ShouldBePersistent()
        {
            string tempDirectoryPath = "/tmp";

            Config config = new Config();
            config.TempDirectoryPath = tempDirectoryPath;

            {
                this.programSettings.Config = config;

                Assert.AreEqual(tempDirectoryPath, this.programSettings.Config.TempDirectoryPath, "1. Temp Directory path should match.");
            }

            {
                this.programSettings.Config = null;

                Assert.IsNull(this.programSettings.Config, "2. Config value should be null.");
            }
        }

        [Test]
        public void ProgramSettings_HigherStructrureXpathPropertyInNewInstance_ShouldBeDefault()
        {
            string defaultXpath = "//article";
            Assert.AreEqual(defaultXpath, this.programSettings.HigherStructrureXpath, "Default HigherStructrureXpath value should be //article");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfHigherStructrureXpathProperty_ShouldBePersistent()
        {
            string xpath = "//i";

            {
                this.programSettings.HigherStructrureXpath = xpath;

                Assert.AreEqual(xpath, this.programSettings.HigherStructrureXpath, "1. HigherStructrureXpath and xpath should match.");
            }

            {
                this.programSettings.HigherStructrureXpath = null;

                Assert.IsNull(this.programSettings.HigherStructrureXpath, "2. HigherStructrureXpath value should be null.");
            }
        }

        [Test]
        public void ProgramSettings_ExtractHigherTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractHigherTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ExtractHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractHigherTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExtractHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ExtractLowerTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractLowerTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ExtractLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExtractLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ExtractTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ExtractTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExtractTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagGeoNamesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagGeoNames, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagGeoNamesProperty_ShouldBePersistent()
        {
            this.programSettings.TagGeoNames = true;
            Assert.IsTrue(this.programSettings.TagGeoNames, ValueShouldBeTrueMessage);

            this.programSettings.TagGeoNames = false;
            Assert.IsFalse(this.programSettings.TagGeoNames, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagGeoEpithetsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagGeoEpithets, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagGeoEpithetsProperty_ShouldBePersistent()
        {
            this.programSettings.TagGeoEpithets = true;
            Assert.IsTrue(this.programSettings.TagGeoEpithets, ValueShouldBeTrueMessage);

            this.programSettings.TagGeoEpithets = false;
            Assert.IsFalse(this.programSettings.TagGeoEpithets, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagMorphologicalEpithetsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagMorphologicalEpithets, "Default value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagMorphologicalEpithetsProperty_ShouldBePersistent()
        {
            this.programSettings.TagMorphologicalEpithets = true;
            Assert.IsTrue(this.programSettings.TagMorphologicalEpithets, "1. Value should be true.");

            this.programSettings.TagMorphologicalEpithets = false;
            Assert.IsFalse(this.programSettings.TagMorphologicalEpithets, "2. Value should be false.");
        }

        [Test]
        public void ProgramSettings_TagProductsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagProducts, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagProductsProperty_ShouldBePersistent()
        {
            this.programSettings.TagProducts = true;
            Assert.IsTrue(this.programSettings.TagProducts, ValueShouldBeTrueMessage);

            this.programSettings.TagProducts = false;
            Assert.IsFalse(this.programSettings.TagProducts, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagInstitutionsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagInstitutions, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagInstitutionsProperty_ShouldBePersistent()
        {
            this.programSettings.TagInstitutions = true;
            Assert.IsTrue(this.programSettings.TagInstitutions, ValueShouldBeTrueMessage);

            this.programSettings.TagInstitutions = false;
            Assert.IsFalse(this.programSettings.TagInstitutions, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ResolveMediaTypesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfResolveMediaTypesProperty_ShouldBePersistent()
        {
            this.programSettings.ResolveMediaTypes = true;
            Assert.IsTrue(this.programSettings.ResolveMediaTypes, ValueShouldBeTrueMessage);

            this.programSettings.ResolveMediaTypes = false;
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_FloraPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flora, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFloraProperty_ShouldBePersistent()
        {
            this.programSettings.Flora = true;
            Assert.IsTrue(this.programSettings.Flora, ValueShouldBeTrueMessage);

            this.programSettings.Flora = false;
            Assert.IsFalse(this.programSettings.Flora, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_FormatInitPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.InitialFormat, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFormatInitProperty_ShouldBePersistent()
        {
            this.programSettings.InitialFormat = true;
            Assert.IsTrue(this.programSettings.InitialFormat, ValueShouldBeTrueMessage);

            this.programSettings.InitialFormat = false;
            Assert.IsFalse(this.programSettings.InitialFormat, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_FormatTreatPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.FormatTreat, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFormatTreatProperty_ShouldBePersistent()
        {
            this.programSettings.FormatTreat = true;
            Assert.IsTrue(this.programSettings.FormatTreat, ValueShouldBeTrueMessage);

            this.programSettings.FormatTreat = false;
            Assert.IsFalse(this.programSettings.FormatTreat, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseBySectionPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseBySection, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseBySectionProperty_ShouldBePersistent()
        {
            this.programSettings.ParseBySection = true;
            Assert.IsTrue(this.programSettings.ParseBySection, ValueShouldBeTrueMessage);

            this.programSettings.ParseBySection = false;
            Assert.IsFalse(this.programSettings.ParseBySection, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseCoordsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseCoordinates, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseCoordsProperty_ShouldBePersistent()
        {
            this.programSettings.ParseCoordinates = true;
            Assert.IsTrue(this.programSettings.ParseCoordinates, ValueShouldBeTrueMessage);

            this.programSettings.ParseCoordinates = false;
            Assert.IsFalse(this.programSettings.ParseCoordinates, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseReferencesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseReferences, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseReferencesProperty_ShouldBePersistent()
        {
            this.programSettings.ParseReferences = true;
            Assert.IsTrue(this.programSettings.ParseReferences, ValueShouldBeTrueMessage);

            this.programSettings.ParseReferences = false;
            Assert.IsFalse(this.programSettings.ParseReferences, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithAphiaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithAphiaProperty_ShouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithAphia, ValueShouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithColPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithColProperty_ShouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithCol = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithCol, ValueShouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithCol = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithGbifPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithGbifProperty_ShouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithGbif, ValueShouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_QuentinSpecificActionsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QuentinSpecificActions, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQuentinSpecificActionsProperty_ShouldBePersistent()
        {
            this.programSettings.QuentinSpecificActions = true;
            Assert.IsTrue(this.programSettings.QuentinSpecificActions, ValueShouldBeTrueMessage);

            this.programSettings.QuentinSpecificActions = false;
            Assert.IsFalse(this.programSettings.QuentinSpecificActions, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_QueryReplacePropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QueryReplace, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQueryReplaceProperty_ShouldBePersistent()
        {
            this.programSettings.QueryReplace = true;
            Assert.IsTrue(this.programSettings.QueryReplace, ValueShouldBeTrueMessage);

            this.programSettings.QueryReplace = false;
            Assert.IsFalse(this.programSettings.QueryReplace, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherAboveGenusPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherAboveGenusProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherAboveGenus = true;
            Assert.IsTrue(this.programSettings.ParseHigherAboveGenus, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherAboveGenus = false;
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherBySuffixPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherBySuffixProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherBySuffix = true;
            Assert.IsTrue(this.programSettings.ParseHigherBySuffix, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherBySuffix = false;
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherWithAphiaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithAphiaProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithAphia, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherWithCoLPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithCoLProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherWithCoL = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithCoL, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherWithCoL = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherWithGbifPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithGbifProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithGbif, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagAbbrevPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagAbbreviations, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagAbbrevProperty_ShouldBePersistent()
        {
            this.programSettings.TagAbbreviations = true;
            Assert.IsTrue(this.programSettings.TagAbbreviations, ValueShouldBeTrueMessage);

            this.programSettings.TagAbbreviations = false;
            Assert.IsFalse(this.programSettings.TagAbbreviations, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagCodesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCodes, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagCodesProperty_ShouldBePersistent()
        {
            this.programSettings.TagCodes = true;
            Assert.IsTrue(this.programSettings.TagCodes, ValueShouldBeTrueMessage);

            this.programSettings.TagCodes = false;
            Assert.IsFalse(this.programSettings.TagCodes, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagCoordsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCoordinates, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagCoordsProperty_ShouldBePersistent()
        {
            this.programSettings.TagCoordinates = true;
            Assert.IsTrue(this.programSettings.TagCoordinates, ValueShouldBeTrueMessage);

            this.programSettings.TagCoordinates = false;
            Assert.IsFalse(this.programSettings.TagCoordinates, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagDatesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDates, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagDatesProperty_ShouldBePersistent()
        {
            this.programSettings.TagDates = true;
            Assert.IsTrue(this.programSettings.TagDates, ValueShouldBeTrueMessage);

            this.programSettings.TagDates = false;
            Assert.IsFalse(this.programSettings.TagDates, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagDoiPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDoi, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagDoiProperty_ShouldBePersistent()
        {
            this.programSettings.TagDoi = true;
            Assert.IsTrue(this.programSettings.TagDoi, ValueShouldBeTrueMessage);

            this.programSettings.TagDoi = false;
            Assert.IsFalse(this.programSettings.TagDoi, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagEnvironmentTermsPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvironmentTerms, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagEnvironmentTermsProperty_ShouldBePersistent()
        {
            this.programSettings.TagEnvironmentTerms = true;
            Assert.IsTrue(this.programSettings.TagEnvironmentTerms, ValueShouldBeTrueMessage);

            this.programSettings.TagEnvironmentTerms = false;
            Assert.IsFalse(this.programSettings.TagEnvironmentTerms, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagEnvironmentTermsWithExtractPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvironmentTermsWithExtract, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagEnvironmentTermsWithExtractProperty_ShouldBePersistent()
        {
            this.programSettings.TagEnvironmentTermsWithExtract = true;
            Assert.IsTrue(this.programSettings.TagEnvironmentTermsWithExtract, ValueShouldBeTrueMessage);

            this.programSettings.TagEnvironmentTermsWithExtract = false;
            Assert.IsFalse(this.programSettings.TagEnvironmentTermsWithExtract, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagFigTabPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagFloats, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagFigTabProperty_ShouldBePersistent()
        {
            this.programSettings.TagFloats = true;
            Assert.IsTrue(this.programSettings.TagFloats, ValueShouldBeTrueMessage);

            this.programSettings.TagFloats = false;
            Assert.IsFalse(this.programSettings.TagFloats, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagQuantitiesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagQuantities, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagQuantitiesProperty_ShouldBePersistent()
        {
            this.programSettings.TagQuantities = true;
            Assert.IsTrue(this.programSettings.TagQuantities, ValueShouldBeTrueMessage);

            this.programSettings.TagQuantities = false;
            Assert.IsFalse(this.programSettings.TagQuantities, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagReferencesPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagReferences, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagReferencesProperty_ShouldBePersistent()
        {
            this.programSettings.TagReferences = true;
            Assert.IsTrue(this.programSettings.TagReferences, ValueShouldBeTrueMessage);

            this.programSettings.TagReferences = false;
            Assert.IsFalse(this.programSettings.TagReferences, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_RunXslTransformPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.RunXslTransform, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfRunXslTransformProperty_ShouldBePersistent()
        {
            this.programSettings.RunXslTransform = true;
            Assert.IsTrue(this.programSettings.RunXslTransform, ValueShouldBeTrueMessage);

            this.programSettings.RunXslTransform = false;
            Assert.IsFalse(this.programSettings.RunXslTransform, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagTableFnPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagTableFn, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagTableFnProperty_ShouldBePersistent()
        {
            this.programSettings.TagTableFn = true;
            Assert.IsTrue(this.programSettings.TagTableFn, ValueShouldBeTrueMessage);

            this.programSettings.TagTableFn = false;
            Assert.IsFalse(this.programSettings.TagTableFn, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagWWWPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagWebLinks, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagWWWProperty_ShouldBePersistent()
        {
            this.programSettings.TagWebLinks = true;
            Assert.IsTrue(this.programSettings.TagWebLinks, ValueShouldBeTrueMessage);

            this.programSettings.TagWebLinks = false;
            Assert.IsFalse(this.programSettings.TagWebLinks, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaAPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaAProperty_ShouldBePersistent()
        {
            this.programSettings.TagLowerTaxa = true;
            Assert.IsTrue(this.programSettings.TagLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.TagLowerTaxa = false;
            Assert.IsFalse(this.programSettings.TagLowerTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaBPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaBProperty_ShouldBePersistent()
        {
            this.programSettings.TagHigherTaxa = true;
            Assert.IsTrue(this.programSettings.TagHigherTaxa, ValueShouldBeTrueMessage);

            this.programSettings.TagHigherTaxa = false;
            Assert.IsFalse(this.programSettings.TagHigherTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaCPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaCProperty_ShouldBePersistent()
        {
            this.programSettings.ParseLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ParseLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ParseLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaDPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaDProperty_ShouldBePersistent()
        {
            this.programSettings.ParseHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ParseHigherTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ParseHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaEPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaEProperty_ShouldBePersistent()
        {
            this.programSettings.ExpandLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExpandLowerTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ExpandLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TestFlagPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TestFlag, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTestFlagProperty_ShouldBePersistent()
        {
            this.programSettings.TestFlag = true;
            Assert.IsTrue(this.programSettings.TestFlag, ValueShouldBeTrueMessage);

            this.programSettings.TestFlag = false;
            Assert.IsFalse(this.programSettings.TestFlag, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_UntagSplitPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.UntagSplit, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfUntagSplitProperty_ShouldBePersistent()
        {
            this.programSettings.UntagSplit = true;
            Assert.IsTrue(this.programSettings.UntagSplit, ValueShouldBeTrueMessage);

            this.programSettings.UntagSplit = false;
            Assert.IsFalse(this.programSettings.UntagSplit, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidateTaxaPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ValidateTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfValidateTaxaProperty_ShouldBePersistent()
        {
            this.programSettings.ValidateTaxa = true;
            Assert.IsTrue(this.programSettings.ValidateTaxa, ValueShouldBeTrueMessage);

            this.programSettings.ValidateTaxa = false;
            Assert.IsFalse(this.programSettings.ValidateTaxa, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ZoobankCloneJsonPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankCloneJsonProperty_ShouldBePersistent()
        {
            this.programSettings.ZoobankCloneJson = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneJson, ValueShouldBeTrueMessage);

            this.programSettings.ZoobankCloneJson = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ZoobankCloneXmlPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankCloneXmlProperty_ShouldBePersistent()
        {
            this.programSettings.ZoobankCloneXml = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneXml, ValueShouldBeTrueMessage);

            this.programSettings.ZoobankCloneXml = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, ValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ZoobankGenerateRegistrationXmlPropertyInNewInstance_ShouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankGenerateRegistrationXmlProperty_ShouldBePersistent()
        {
            this.programSettings.ZoobankGenerateRegistrationXml = true;
            Assert.IsTrue(this.programSettings.ZoobankGenerateRegistrationXml, ValueShouldBeTrueMessage);

            this.programSettings.ZoobankGenerateRegistrationXml = false;
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, ValueShouldBeFalseMessage);
        }
    }
}