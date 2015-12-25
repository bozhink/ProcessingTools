namespace ProcessingTools.MainProgram.Tests
{
    using Configurator;
    using NUnit.Framework;

    [TestFixture]
    public class ProgramSettingsTests
    {
        private const string DefaultValueShouldBeFalseMessage = "Default value should be false.";
        private const string ValueSchouldBeTrueMessage = "1. Value should be true.";
        private const string ValueSchouldBeFalseMessage = "2. Value should be false.";

        private ProgramSettings programSettings;

        [SetUp]
        public void Init()
        {
            this.programSettings = new ProgramSettings();
        }

        [Test]
        public void ProgramSettings_ConfigPropertyInNewInstance_SchouldBeNull()
        {
            Assert.IsNull(this.programSettings.Config, "Default Config value should be null.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfConfigProperty_SchouldBePersistent()
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
        public void ProgramSettings_InputFileNamePropertyInNewInstance_SchouldBeNull()
        {
            Assert.IsNull(this.programSettings.InputFileName, "Default InputFileName value should be null.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfInputFileNameProperty_SchouldBePersistent()
        {
            string fileNamePath = "/tmp/file";

            {
                this.programSettings.InputFileName = fileNamePath;

                Assert.AreEqual(fileNamePath, this.programSettings.InputFileName, "1. Input File Name Path path should match.");
            }

            {
                this.programSettings.InputFileName = null;

                Assert.IsNull(this.programSettings.InputFileName, "2. InputFileName value should be null.");
            }
        }

        [Test]
        public void ProgramSettings_OutputFileNamePropertyInNewInstance_SchouldBeNull()
        {
            Assert.IsNull(this.programSettings.OutputFileName, "Default OutputFileName value should be null.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfOutputFileNameProperty_SchouldBePersistent()
        {
            string fileNamePath = "/tmp/file";

            {
                this.programSettings.OutputFileName = fileNamePath;

                Assert.AreEqual(fileNamePath, this.programSettings.OutputFileName, "1. Output File Name Path path should match.");
            }

            {
                this.programSettings.OutputFileName = null;

                Assert.IsNull(this.programSettings.OutputFileName, "2. OutputFileName value should be null.");
            }
        }

        [Test]
        public void ProgramSettings_QueryFileNamePropertyInNewInstance_SchouldBeNull()
        {
            Assert.IsNull(this.programSettings.QueryFileName, "Default QueryFileName value should be null.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQueryFileNameProperty_SchouldBePersistent()
        {
            string fileNamePath = "/tmp/file";

            {
                this.programSettings.QueryFileName = fileNamePath;

                Assert.AreEqual(fileNamePath, this.programSettings.QueryFileName, "1. Query File Name Path path should match.");
            }

            {
                this.programSettings.QueryFileName = null;

                Assert.IsNull(this.programSettings.QueryFileName, "2. QueryFileName value should be null.");
            }
        }

        [Test]
        public void ProgramSettings_HigherStructrureXpathPropertyInNewInstance_SchouldBeDefault()
        {
            string defaultXpath = "//article";
            Assert.AreEqual(defaultXpath, this.programSettings.HigherStructrureXpath, "Default HigherStructrureXpath value should be //article");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfHigherStructrureXpathProperty_SchouldBePersistent()
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
        public void ProgramSettings_ExtractHigherTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractHigherTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ExtractHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractHigherTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ExtractHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ExtractLowerTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractLowerTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ExtractLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractLowerTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ExtractLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ExtractTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ExtractTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ExtractTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag1PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag1, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag1Property_SchouldBePersistent()
        {
            this.programSettings.Flag1 = true;
            Assert.IsTrue(this.programSettings.Flag1, ValueSchouldBeTrueMessage);

            this.programSettings.Flag1 = false;
            Assert.IsFalse(this.programSettings.Flag1, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag2PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag2, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag2Property_SchouldBePersistent()
        {
            this.programSettings.Flag2 = true;
            Assert.IsTrue(this.programSettings.Flag2, ValueSchouldBeTrueMessage);

            this.programSettings.Flag2 = false;
            Assert.IsFalse(this.programSettings.Flag2, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag3PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag3, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag3Property_SchouldBePersistent()
        {
            this.programSettings.Flag3 = true;
            Assert.IsTrue(this.programSettings.Flag3, ValueSchouldBeTrueMessage);

            this.programSettings.Flag3 = false;
            Assert.IsFalse(this.programSettings.Flag3, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag4PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag4, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag4Property_SchouldBePersistent()
        {
            this.programSettings.Flag4 = true;
            Assert.IsTrue(this.programSettings.Flag4, ValueSchouldBeTrueMessage);

            this.programSettings.Flag4 = false;
            Assert.IsFalse(this.programSettings.Flag4, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag5PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag5, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag5Property_SchouldBePersistent()
        {
            this.programSettings.Flag5 = true;
            Assert.IsTrue(this.programSettings.Flag5, ValueSchouldBeTrueMessage);

            this.programSettings.Flag5 = false;
            Assert.IsFalse(this.programSettings.Flag5, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag6PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag6, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag6Property_SchouldBePersistent()
        {
            this.programSettings.Flag6 = true;
            Assert.IsTrue(this.programSettings.Flag6, ValueSchouldBeTrueMessage);

            this.programSettings.Flag6 = false;
            Assert.IsFalse(this.programSettings.Flag6, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag7PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag7, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag7Property_SchouldBePersistent()
        {
            this.programSettings.Flag7 = true;
            Assert.IsTrue(this.programSettings.Flag7, ValueSchouldBeTrueMessage);

            this.programSettings.Flag7 = false;
            Assert.IsFalse(this.programSettings.Flag7, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_Flag8PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag8, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag8Property_SchouldBePersistent()
        {
            this.programSettings.Flag8 = true;
            Assert.IsTrue(this.programSettings.Flag8, ValueSchouldBeTrueMessage);

            this.programSettings.Flag8 = false;
            Assert.IsFalse(this.programSettings.Flag8, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagMorphologicalEpithetsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagMorphologicalEpithets, "Default value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagMorphologicalEpithetsProperty_SchouldBePersistent()
        {
            this.programSettings.TagMorphologicalEpithets = true;
            Assert.IsTrue(this.programSettings.TagMorphologicalEpithets, "1. Value should be true.");

            this.programSettings.TagMorphologicalEpithets = false;
            Assert.IsFalse(this.programSettings.TagMorphologicalEpithets, "2. Value should be false.");
        }

        [Test]
        public void ProgramSettings_TagProductsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagProducts, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagProductsProperty_SchouldBePersistent()
        {
            this.programSettings.TagProducts = true;
            Assert.IsTrue(this.programSettings.TagProducts, ValueSchouldBeTrueMessage);

            this.programSettings.TagProducts = false;
            Assert.IsFalse(this.programSettings.TagProducts, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagInstitutionsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagInstitutions, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagInstitutionsProperty_SchouldBePersistent()
        {
            this.programSettings.TagInstitutions = true;
            Assert.IsTrue(this.programSettings.TagInstitutions, ValueSchouldBeTrueMessage);

            this.programSettings.TagInstitutions = false;
            Assert.IsFalse(this.programSettings.TagInstitutions, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ResolveMediaTypesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfResolveMediaTypesProperty_SchouldBePersistent()
        {
            this.programSettings.ResolveMediaTypes = true;
            Assert.IsTrue(this.programSettings.ResolveMediaTypes, ValueSchouldBeTrueMessage);

            this.programSettings.ResolveMediaTypes = false;
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_FloraPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flora, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFloraProperty_SchouldBePersistent()
        {
            this.programSettings.Flora = true;
            Assert.IsTrue(this.programSettings.Flora, ValueSchouldBeTrueMessage);

            this.programSettings.Flora = false;
            Assert.IsFalse(this.programSettings.Flora, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_FormatInitPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.InitialFormat, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFormatInitProperty_SchouldBePersistent()
        {
            this.programSettings.InitialFormat = true;
            Assert.IsTrue(this.programSettings.InitialFormat, ValueSchouldBeTrueMessage);

            this.programSettings.InitialFormat = false;
            Assert.IsFalse(this.programSettings.InitialFormat, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_FormatTreatPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.FormatTreat, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFormatTreatProperty_SchouldBePersistent()
        {
            this.programSettings.FormatTreat = true;
            Assert.IsTrue(this.programSettings.FormatTreat, ValueSchouldBeTrueMessage);

            this.programSettings.FormatTreat = false;
            Assert.IsFalse(this.programSettings.FormatTreat, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseBySectionPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseBySection, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseBySectionProperty_SchouldBePersistent()
        {
            this.programSettings.ParseBySection = true;
            Assert.IsTrue(this.programSettings.ParseBySection, ValueSchouldBeTrueMessage);

            this.programSettings.ParseBySection = false;
            Assert.IsFalse(this.programSettings.ParseBySection, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseCoordsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseCoordinates, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseCoordsProperty_SchouldBePersistent()
        {
            this.programSettings.ParseCoordinates = true;
            Assert.IsTrue(this.programSettings.ParseCoordinates, ValueSchouldBeTrueMessage);

            this.programSettings.ParseCoordinates = false;
            Assert.IsFalse(this.programSettings.ParseCoordinates, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseReferencesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseReferences, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseReferencesProperty_SchouldBePersistent()
        {
            this.programSettings.ParseReferences = true;
            Assert.IsTrue(this.programSettings.ParseReferences, ValueSchouldBeTrueMessage);

            this.programSettings.ParseReferences = false;
            Assert.IsFalse(this.programSettings.ParseReferences, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithAphiaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithAphiaProperty_SchouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithAphia, ValueSchouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithColPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithColProperty_SchouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithCol = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithCol, ValueSchouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithCol = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithGbifPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithGbifProperty_SchouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithGbif, ValueSchouldBeTrueMessage);

            this.programSettings.ParseTreatmentMetaWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_QuentinSpecificActionsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QuentinSpecificActions, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQuentinSpecificActionsProperty_SchouldBePersistent()
        {
            this.programSettings.QuentinSpecificActions = true;
            Assert.IsTrue(this.programSettings.QuentinSpecificActions, ValueSchouldBeTrueMessage);

            this.programSettings.QuentinSpecificActions = false;
            Assert.IsFalse(this.programSettings.QuentinSpecificActions, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_QueryReplacePropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QueryReplace, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQueryReplaceProperty_SchouldBePersistent()
        {
            this.programSettings.QueryReplace = true;
            Assert.IsTrue(this.programSettings.QueryReplace, ValueSchouldBeTrueMessage);

            this.programSettings.QueryReplace = false;
            Assert.IsFalse(this.programSettings.QueryReplace, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherAboveGenusPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherAboveGenusProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherAboveGenus = true;
            Assert.IsTrue(this.programSettings.ParseHigherAboveGenus, ValueSchouldBeTrueMessage);

            this.programSettings.ParseHigherAboveGenus = false;
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherBySuffixPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherBySuffixProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherBySuffix = true;
            Assert.IsTrue(this.programSettings.ParseHigherBySuffix, ValueSchouldBeTrueMessage);

            this.programSettings.ParseHigherBySuffix = false;
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherWithAphiaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithAphiaProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithAphia, ValueSchouldBeTrueMessage);

            this.programSettings.ParseHigherWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherWithCoLPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithCoLProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherWithCoL = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithCoL, ValueSchouldBeTrueMessage);

            this.programSettings.ParseHigherWithCoL = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_SplitHigherWithGbifPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithGbifProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithGbif, ValueSchouldBeTrueMessage);

            this.programSettings.ParseHigherWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagAbbrevPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagAbbreviations, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagAbbrevProperty_SchouldBePersistent()
        {
            this.programSettings.TagAbbreviations = true;
            Assert.IsTrue(this.programSettings.TagAbbreviations, ValueSchouldBeTrueMessage);

            this.programSettings.TagAbbreviations = false;
            Assert.IsFalse(this.programSettings.TagAbbreviations, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagCodesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCodes, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagCodesProperty_SchouldBePersistent()
        {
            this.programSettings.TagCodes = true;
            Assert.IsTrue(this.programSettings.TagCodes, ValueSchouldBeTrueMessage);

            this.programSettings.TagCodes = false;
            Assert.IsFalse(this.programSettings.TagCodes, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagCoordsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCoordinates, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagCoordsProperty_SchouldBePersistent()
        {
            this.programSettings.TagCoordinates = true;
            Assert.IsTrue(this.programSettings.TagCoordinates, ValueSchouldBeTrueMessage);

            this.programSettings.TagCoordinates = false;
            Assert.IsFalse(this.programSettings.TagCoordinates, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagDatesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDates, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagDatesProperty_SchouldBePersistent()
        {
            this.programSettings.TagDates = true;
            Assert.IsTrue(this.programSettings.TagDates, ValueSchouldBeTrueMessage);

            this.programSettings.TagDates = false;
            Assert.IsFalse(this.programSettings.TagDates, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagDoiPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDoi, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagDoiProperty_SchouldBePersistent()
        {
            this.programSettings.TagDoi = true;
            Assert.IsTrue(this.programSettings.TagDoi, ValueSchouldBeTrueMessage);

            this.programSettings.TagDoi = false;
            Assert.IsFalse(this.programSettings.TagDoi, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagEnvironmentsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvironments, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagEnvironmentsProperty_SchouldBePersistent()
        {
            this.programSettings.TagEnvironments = true;
            Assert.IsTrue(this.programSettings.TagEnvironments, ValueSchouldBeTrueMessage);

            this.programSettings.TagEnvironments = false;
            Assert.IsFalse(this.programSettings.TagEnvironments, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagEnvoPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvo, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagEnvoProperty_SchouldBePersistent()
        {
            this.programSettings.TagEnvo = true;
            Assert.IsTrue(this.programSettings.TagEnvo, ValueSchouldBeTrueMessage);

            this.programSettings.TagEnvo = false;
            Assert.IsFalse(this.programSettings.TagEnvo, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagFigTabPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagFloats, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagFigTabProperty_SchouldBePersistent()
        {
            this.programSettings.TagFloats = true;
            Assert.IsTrue(this.programSettings.TagFloats, ValueSchouldBeTrueMessage);

            this.programSettings.TagFloats = false;
            Assert.IsFalse(this.programSettings.TagFloats, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagQuantitiesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagQuantities, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagQuantitiesProperty_SchouldBePersistent()
        {
            this.programSettings.TagQuantities = true;
            Assert.IsTrue(this.programSettings.TagQuantities, ValueSchouldBeTrueMessage);

            this.programSettings.TagQuantities = false;
            Assert.IsFalse(this.programSettings.TagQuantities, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagReferencesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagReferences, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagReferencesProperty_SchouldBePersistent()
        {
            this.programSettings.TagReferences = true;
            Assert.IsTrue(this.programSettings.TagReferences, ValueSchouldBeTrueMessage);

            this.programSettings.TagReferences = false;
            Assert.IsFalse(this.programSettings.TagReferences, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_RunXslTransformPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.RunXslTransform, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfRunXslTransformProperty_SchouldBePersistent()
        {
            this.programSettings.RunXslTransform = true;
            Assert.IsTrue(this.programSettings.RunXslTransform, ValueSchouldBeTrueMessage);

            this.programSettings.RunXslTransform = false;
            Assert.IsFalse(this.programSettings.RunXslTransform, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagTableFnPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagTableFn, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagTableFnProperty_SchouldBePersistent()
        {
            this.programSettings.TagTableFn = true;
            Assert.IsTrue(this.programSettings.TagTableFn, ValueSchouldBeTrueMessage);

            this.programSettings.TagTableFn = false;
            Assert.IsFalse(this.programSettings.TagTableFn, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TagWWWPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagWebLinks, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagWWWProperty_SchouldBePersistent()
        {
            this.programSettings.TagWebLinks = true;
            Assert.IsTrue(this.programSettings.TagWebLinks, ValueSchouldBeTrueMessage);

            this.programSettings.TagWebLinks = false;
            Assert.IsFalse(this.programSettings.TagWebLinks, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaAPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaAProperty_SchouldBePersistent()
        {
            this.programSettings.TagLowerTaxa = true;
            Assert.IsTrue(this.programSettings.TagLowerTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.TagLowerTaxa = false;
            Assert.IsFalse(this.programSettings.TagLowerTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaBPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaBProperty_SchouldBePersistent()
        {
            this.programSettings.TagHigherTaxa = true;
            Assert.IsTrue(this.programSettings.TagHigherTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.TagHigherTaxa = false;
            Assert.IsFalse(this.programSettings.TagHigherTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaCPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaCProperty_SchouldBePersistent()
        {
            this.programSettings.ParseLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ParseLowerTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ParseLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaDPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaDProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ParseHigherTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ParseHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TaxaEPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaEProperty_SchouldBePersistent()
        {
            this.programSettings.ExpandLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExpandLowerTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ExpandLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_TestFlagPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TestFlag, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTestFlagProperty_SchouldBePersistent()
        {
            this.programSettings.TestFlag = true;
            Assert.IsTrue(this.programSettings.TestFlag, ValueSchouldBeTrueMessage);

            this.programSettings.TestFlag = false;
            Assert.IsFalse(this.programSettings.TestFlag, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_UntagSplitPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.UntagSplit, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfUntagSplitProperty_SchouldBePersistent()
        {
            this.programSettings.UntagSplit = true;
            Assert.IsTrue(this.programSettings.UntagSplit, ValueSchouldBeTrueMessage);

            this.programSettings.UntagSplit = false;
            Assert.IsFalse(this.programSettings.UntagSplit, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidateTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ValidateTaxa, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfValidateTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ValidateTaxa = true;
            Assert.IsTrue(this.programSettings.ValidateTaxa, ValueSchouldBeTrueMessage);

            this.programSettings.ValidateTaxa = false;
            Assert.IsFalse(this.programSettings.ValidateTaxa, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ZoobankCloneJsonPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankCloneJsonProperty_SchouldBePersistent()
        {
            this.programSettings.ZoobankCloneJson = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneJson, ValueSchouldBeTrueMessage);

            this.programSettings.ZoobankCloneJson = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ZoobankCloneXmlPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankCloneXmlProperty_SchouldBePersistent()
        {
            this.programSettings.ZoobankCloneXml = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneXml, ValueSchouldBeTrueMessage);

            this.programSettings.ZoobankCloneXml = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, ValueSchouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ZoobankGenerateRegistrationXmlPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, DefaultValueShouldBeFalseMessage);
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankGenerateRegistrationXmlProperty_SchouldBePersistent()
        {
            this.programSettings.ZoobankGenerateRegistrationXml = true;
            Assert.IsTrue(this.programSettings.ZoobankGenerateRegistrationXml, ValueSchouldBeTrueMessage);

            this.programSettings.ZoobankGenerateRegistrationXml = false;
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, ValueSchouldBeFalseMessage);
        }
    }
}