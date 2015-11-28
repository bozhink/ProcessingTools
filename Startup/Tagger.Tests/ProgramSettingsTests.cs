namespace ProcessingTools.MainProgram.Tests
{
    using Configurator;
    using NUnit.Framework;

    [TestFixture]
    public class ProgramSettingsTests
    {
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
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, "Default ExtractHigherTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractHigherTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ExtractHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractHigherTaxa, "1. ExtractHigherTaxa value should be true.");

            this.programSettings.ExtractHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractHigherTaxa, "2. ExtractHigherTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ExtractLowerTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, "Default ExtractLowerTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractLowerTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ExtractLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractLowerTaxa, "1. ExtractLowerTaxa value should be true.");

            this.programSettings.ExtractLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractLowerTaxa, "2. ExtractLowerTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ExtractTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExtractTaxa, "Default ExtractTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfExtractTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ExtractTaxa = true;
            Assert.IsTrue(this.programSettings.ExtractTaxa, "1. ExtractTaxa value should be true.");

            this.programSettings.ExtractTaxa = false;
            Assert.IsFalse(this.programSettings.ExtractTaxa, "2. ExtractTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag1PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag1, "Default Flag1 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag1Property_SchouldBePersistent()
        {
            this.programSettings.Flag1 = true;
            Assert.IsTrue(this.programSettings.Flag1, "1. Flag1 value should be true.");

            this.programSettings.Flag1 = false;
            Assert.IsFalse(this.programSettings.Flag1, "2. Flag1 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag2PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag2, "Default Flag2 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag2Property_SchouldBePersistent()
        {
            this.programSettings.Flag2 = true;
            Assert.IsTrue(this.programSettings.Flag2, "1. Flag2 value should be true.");

            this.programSettings.Flag2 = false;
            Assert.IsFalse(this.programSettings.Flag2, "2. Flag2 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag3PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag3, "Default Flag3 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag3Property_SchouldBePersistent()
        {
            this.programSettings.Flag3 = true;
            Assert.IsTrue(this.programSettings.Flag3, "1. Flag3 value should be true.");

            this.programSettings.Flag3 = false;
            Assert.IsFalse(this.programSettings.Flag3, "2. Flag3 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag4PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag4, "Default Flag4 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag4Property_SchouldBePersistent()
        {
            this.programSettings.Flag4 = true;
            Assert.IsTrue(this.programSettings.Flag4, "1. Flag4 value should be true.");

            this.programSettings.Flag4 = false;
            Assert.IsFalse(this.programSettings.Flag4, "2. Flag4 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag5PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag5, "Default Flag5 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag5Property_SchouldBePersistent()
        {
            this.programSettings.Flag5 = true;
            Assert.IsTrue(this.programSettings.Flag5, "1. Flag5 value should be true.");

            this.programSettings.Flag5 = false;
            Assert.IsFalse(this.programSettings.Flag5, "2. Flag5 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag6PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag6, "Default Flag6 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag6Property_SchouldBePersistent()
        {
            this.programSettings.Flag6 = true;
            Assert.IsTrue(this.programSettings.Flag6, "1. Flag6 value should be true.");

            this.programSettings.Flag6 = false;
            Assert.IsFalse(this.programSettings.Flag6, "2. Flag6 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag7PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag7, "Default Flag7 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag7Property_SchouldBePersistent()
        {
            this.programSettings.Flag7 = true;
            Assert.IsTrue(this.programSettings.Flag7, "1. Flag7 value should be true.");

            this.programSettings.Flag7 = false;
            Assert.IsFalse(this.programSettings.Flag7, "2. Flag7 value should be false.");
        }

        [Test]
        public void ProgramSettings_Flag8PropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flag8, "Default Flag8 value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFlag8Property_SchouldBePersistent()
        {
            this.programSettings.Flag8 = true;
            Assert.IsTrue(this.programSettings.Flag8, "1. Flag8 value should be true.");

            this.programSettings.Flag8 = false;
            Assert.IsFalse(this.programSettings.Flag8, "2. Flag8 value should be false.");
        }

        [Test]
        public void ProgramSettings_ResolveMediaTypesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, "Default ResolveMediaTypes value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfResolveMediaTypesProperty_SchouldBePersistent()
        {
            this.programSettings.ResolveMediaTypes = true;
            Assert.IsTrue(this.programSettings.ResolveMediaTypes, "1. ResolveMediaTypes value should be true.");

            this.programSettings.ResolveMediaTypes = false;
            Assert.IsFalse(this.programSettings.ResolveMediaTypes, "2. ResolveMediaTypes value should be false.");
        }

        [Test]
        public void ProgramSettings_FloraPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.Flora, "Default Flora value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFloraProperty_SchouldBePersistent()
        {
            this.programSettings.Flora = true;
            Assert.IsTrue(this.programSettings.Flora, "1. Flora value should be true.");

            this.programSettings.Flora = false;
            Assert.IsFalse(this.programSettings.Flora, "2. Flora value should be false.");
        }

        [Test]
        public void ProgramSettings_FormatInitPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.InitialFormat, "Default FormatInit value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFormatInitProperty_SchouldBePersistent()
        {
            this.programSettings.InitialFormat = true;
            Assert.IsTrue(this.programSettings.InitialFormat, "1. FormatInit value should be true.");

            this.programSettings.InitialFormat = false;
            Assert.IsFalse(this.programSettings.InitialFormat, "2. FormatInit value should be false.");
        }

        [Test]
        public void ProgramSettings_FormatTreatPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.FormatTreat, "Default FormatTreat value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfFormatTreatProperty_SchouldBePersistent()
        {
            this.programSettings.FormatTreat = true;
            Assert.IsTrue(this.programSettings.FormatTreat, "1. FormatTreat value should be true.");

            this.programSettings.FormatTreat = false;
            Assert.IsFalse(this.programSettings.FormatTreat, "2. FormatTreat value should be false.");
        }

        [Test]
        public void ProgramSettings_ParseBySectionPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseBySection, "Default ParseBySection value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseBySectionProperty_SchouldBePersistent()
        {
            this.programSettings.ParseBySection = true;
            Assert.IsTrue(this.programSettings.ParseBySection, "1. ParseBySection value should be true.");

            this.programSettings.ParseBySection = false;
            Assert.IsFalse(this.programSettings.ParseBySection, "2. ParseBySection value should be false.");
        }

        [Test]
        public void ProgramSettings_ParseCoordsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseCoordinates, "Default ParseCoords value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseCoordsProperty_SchouldBePersistent()
        {
            this.programSettings.ParseCoordinates = true;
            Assert.IsTrue(this.programSettings.ParseCoordinates, "1. ParseCoords value should be true.");

            this.programSettings.ParseCoordinates = false;
            Assert.IsFalse(this.programSettings.ParseCoordinates, "2. ParseCoords value should be false.");
        }

        [Test]
        public void ProgramSettings_ParseReferencesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseReferences, "Default ParseReferences value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseReferencesProperty_SchouldBePersistent()
        {
            this.programSettings.ParseReferences = true;
            Assert.IsTrue(this.programSettings.ParseReferences, "1. ParseReferences value should be true.");

            this.programSettings.ParseReferences = false;
            Assert.IsFalse(this.programSettings.ParseReferences, "2. ParseReferences value should be false.");
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithAphiaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, "Default ParseTreatmentMetaWithAphia value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithAphiaProperty_SchouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithAphia, "1. ParseTreatmentMetaWithAphia value should be true.");

            this.programSettings.ParseTreatmentMetaWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithAphia, "2. ParseTreatmentMetaWithAphia value should be false.");
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithColPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, "Default ParseTreatmentMetaWithCol value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithColProperty_SchouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithCol = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithCol, "1. ParseTreatmentMetaWithCol value should be true.");

            this.programSettings.ParseTreatmentMetaWithCol = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithCol, "2. ParseTreatmentMetaWithCol value should be false.");
        }

        [Test]
        public void ProgramSettings_ParseTreatmentMetaWithGbifPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, "Default ParseTreatmentMetaWithGbif value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfParseTreatmentMetaWithGbifProperty_SchouldBePersistent()
        {
            this.programSettings.ParseTreatmentMetaWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseTreatmentMetaWithGbif, "1. ParseTreatmentMetaWithGbif value should be true.");

            this.programSettings.ParseTreatmentMetaWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseTreatmentMetaWithGbif, "2. ParseTreatmentMetaWithGbif value should be false.");
        }

        [Test]
        public void ProgramSettings_QuentinSpecificActionsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QuentinSpecificActions, "Default QuentinSpecificActions value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQuentinSpecificActionsProperty_SchouldBePersistent()
        {
            this.programSettings.QuentinSpecificActions = true;
            Assert.IsTrue(this.programSettings.QuentinSpecificActions, "1. QuentinSpecificActions value should be true.");

            this.programSettings.QuentinSpecificActions = false;
            Assert.IsFalse(this.programSettings.QuentinSpecificActions, "2. QuentinSpecificActions value should be false.");
        }

        [Test]
        public void ProgramSettings_QueryReplacePropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.QueryReplace, "Default QueryReplace value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfQueryReplaceProperty_SchouldBePersistent()
        {
            this.programSettings.QueryReplace = true;
            Assert.IsTrue(this.programSettings.QueryReplace, "1. QueryReplace value should be true.");

            this.programSettings.QueryReplace = false;
            Assert.IsFalse(this.programSettings.QueryReplace, "2. QueryReplace value should be false.");
        }

        [Test]
        public void ProgramSettings_SplitHigherAboveGenusPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, "Default SplitHigherAboveGenus value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherAboveGenusProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherAboveGenus = true;
            Assert.IsTrue(this.programSettings.ParseHigherAboveGenus, "1. SplitHigherAboveGenus value should be true.");

            this.programSettings.ParseHigherAboveGenus = false;
            Assert.IsFalse(this.programSettings.ParseHigherAboveGenus, "2. SplitHigherAboveGenus value should be false.");
        }

        [Test]
        public void ProgramSettings_SplitHigherBySuffixPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, "Default SplitHigherBySuffix value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherBySuffixProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherBySuffix = true;
            Assert.IsTrue(this.programSettings.ParseHigherBySuffix, "1. SplitHigherBySuffix value should be true.");

            this.programSettings.ParseHigherBySuffix = false;
            Assert.IsFalse(this.programSettings.ParseHigherBySuffix, "2. SplitHigherBySuffix value should be false.");
        }

        [Test]
        public void ProgramSettings_SplitHigherWithAphiaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, "Default SplitHigherWithAphia value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithAphiaProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherWithAphia = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithAphia, "1. SplitHigherWithAphia value should be true.");

            this.programSettings.ParseHigherWithAphia = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithAphia, "2. SplitHigherWithAphia value should be false.");
        }

        [Test]
        public void ProgramSettings_SplitHigherWithCoLPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, "Default SplitHigherWithCoL value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithCoLProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherWithCoL = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithCoL, "1. SplitHigherWithCoL value should be true.");

            this.programSettings.ParseHigherWithCoL = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithCoL, "2. SplitHigherWithCoL value should be false.");
        }

        [Test]
        public void ProgramSettings_SplitHigherWithGbifPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, "Default SplitHigherWithGbif value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfSplitHigherWithGbifProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherWithGbif = true;
            Assert.IsTrue(this.programSettings.ParseHigherWithGbif, "1. SplitHigherWithGbif value should be true.");

            this.programSettings.ParseHigherWithGbif = false;
            Assert.IsFalse(this.programSettings.ParseHigherWithGbif, "2. SplitHigherWithGbif value should be false.");
        }

        [Test]
        public void ProgramSettings_TagAbbrevPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagAbbreviations, "Default TagAbbrev value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagAbbrevProperty_SchouldBePersistent()
        {
            this.programSettings.TagAbbreviations = true;
            Assert.IsTrue(this.programSettings.TagAbbreviations, "1. TagAbbrev value should be true.");

            this.programSettings.TagAbbreviations = false;
            Assert.IsFalse(this.programSettings.TagAbbreviations, "2. TagAbbrev value should be false.");
        }

        [Test]
        public void ProgramSettings_TagCodesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCodes, "Default TagCodes value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagCodesProperty_SchouldBePersistent()
        {
            this.programSettings.TagCodes = true;
            Assert.IsTrue(this.programSettings.TagCodes, "1. TagCodes value should be true.");

            this.programSettings.TagCodes = false;
            Assert.IsFalse(this.programSettings.TagCodes, "2. TagCodes value should be false.");
        }

        [Test]
        public void ProgramSettings_TagCoordsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagCoordinates, "Default TagCoords value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagCoordsProperty_SchouldBePersistent()
        {
            this.programSettings.TagCoordinates = true;
            Assert.IsTrue(this.programSettings.TagCoordinates, "1. TagCoords value should be true.");

            this.programSettings.TagCoordinates = false;
            Assert.IsFalse(this.programSettings.TagCoordinates, "2. TagCoords value should be false.");
        }

        [Test]
        public void ProgramSettings_TagDatesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDates, "Default TagDates value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagDatesProperty_SchouldBePersistent()
        {
            this.programSettings.TagDates = true;
            Assert.IsTrue(this.programSettings.TagDates, "1. TagDates value should be true.");

            this.programSettings.TagDates = false;
            Assert.IsFalse(this.programSettings.TagDates, "2. TagDates value should be false.");
        }

        [Test]
        public void ProgramSettings_TagDoiPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagDoi, "Default TagDoi value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagDoiProperty_SchouldBePersistent()
        {
            this.programSettings.TagDoi = true;
            Assert.IsTrue(this.programSettings.TagDoi, "1. TagDoi value should be true.");

            this.programSettings.TagDoi = false;
            Assert.IsFalse(this.programSettings.TagDoi, "2. TagDoi value should be false.");
        }

        [Test]
        public void ProgramSettings_TagEnvironmentsPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvironments, "Default TagEnvironments value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagEnvironmentsProperty_SchouldBePersistent()
        {
            this.programSettings.TagEnvironments = true;
            Assert.IsTrue(this.programSettings.TagEnvironments, "1. TagEnvironments value should be true.");

            this.programSettings.TagEnvironments = false;
            Assert.IsFalse(this.programSettings.TagEnvironments, "2. TagEnvironments value should be false.");
        }

        [Test]
        public void ProgramSettings_TagEnvoPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagEnvo, "Default TagEnvo value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagEnvoProperty_SchouldBePersistent()
        {
            this.programSettings.TagEnvo = true;
            Assert.IsTrue(this.programSettings.TagEnvo, "1. TagEnvo value should be true.");

            this.programSettings.TagEnvo = false;
            Assert.IsFalse(this.programSettings.TagEnvo, "2. TagEnvo value should be false.");
        }

        [Test]
        public void ProgramSettings_TagFigTabPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagFloats, "Default TagFigTab value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagFigTabProperty_SchouldBePersistent()
        {
            this.programSettings.TagFloats = true;
            Assert.IsTrue(this.programSettings.TagFloats, "1. TagFigTab value should be true.");

            this.programSettings.TagFloats = false;
            Assert.IsFalse(this.programSettings.TagFloats, "2. TagFigTab value should be false.");
        }

        [Test]
        public void ProgramSettings_TagQuantitiesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagQuantities, "Default TagQuantities value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagQuantitiesProperty_SchouldBePersistent()
        {
            this.programSettings.TagQuantities = true;
            Assert.IsTrue(this.programSettings.TagQuantities, "1. TagQuantities value should be true.");

            this.programSettings.TagQuantities = false;
            Assert.IsFalse(this.programSettings.TagQuantities, "2. TagQuantities value should be false.");
        }

        [Test]
        public void ProgramSettings_TagReferencesPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagReferences, "Default TagReferences value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagReferencesProperty_SchouldBePersistent()
        {
            this.programSettings.TagReferences = true;
            Assert.IsTrue(this.programSettings.TagReferences, "1. TagReferences value should be true.");

            this.programSettings.TagReferences = false;
            Assert.IsFalse(this.programSettings.TagReferences, "2. TagReferences value should be false.");
        }

        [Test]
        public void ProgramSettings_TagTableFnPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagTableFn, "Default TagTableFn value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagTableFnProperty_SchouldBePersistent()
        {
            this.programSettings.TagTableFn = true;
            Assert.IsTrue(this.programSettings.TagTableFn, "1. TagTableFn value should be true.");

            this.programSettings.TagTableFn = false;
            Assert.IsFalse(this.programSettings.TagTableFn, "2. TagTableFn value should be false.");
        }

        [Test]
        public void ProgramSettings_TagWWWPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagWebLinks, "Default TagWWW value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTagWWWProperty_SchouldBePersistent()
        {
            this.programSettings.TagWebLinks = true;
            Assert.IsTrue(this.programSettings.TagWebLinks, "1. TagWWW value should be true.");

            this.programSettings.TagWebLinks = false;
            Assert.IsFalse(this.programSettings.TagWebLinks, "2. TagWWW value should be false.");
        }

        [Test]
        public void ProgramSettings_TaxaAPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagLowerTaxa, "Default TaxaA value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaAProperty_SchouldBePersistent()
        {
            this.programSettings.TagLowerTaxa = true;
            Assert.IsTrue(this.programSettings.TagLowerTaxa, "1. TaxaA value should be true.");

            this.programSettings.TagLowerTaxa = false;
            Assert.IsFalse(this.programSettings.TagLowerTaxa, "2. TaxaA value should be false.");
        }

        [Test]
        public void ProgramSettings_TaxaBPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TagHigherTaxa, "Default TaxaB value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaBProperty_SchouldBePersistent()
        {
            this.programSettings.TagHigherTaxa = true;
            Assert.IsTrue(this.programSettings.TagHigherTaxa, "1. TaxaB value should be true.");

            this.programSettings.TagHigherTaxa = false;
            Assert.IsFalse(this.programSettings.TagHigherTaxa, "2. TaxaB value should be false.");
        }

        [Test]
        public void ProgramSettings_TaxaCPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, "Default TaxaC value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaCProperty_SchouldBePersistent()
        {
            this.programSettings.ParseLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ParseLowerTaxa, "1. TaxaC value should be true.");

            this.programSettings.ParseLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ParseLowerTaxa, "2. TaxaC value should be false.");
        }

        [Test]
        public void ProgramSettings_TaxaDPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, "Default TaxaD value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaDProperty_SchouldBePersistent()
        {
            this.programSettings.ParseHigherTaxa = true;
            Assert.IsTrue(this.programSettings.ParseHigherTaxa, "1. TaxaD value should be true.");

            this.programSettings.ParseHigherTaxa = false;
            Assert.IsFalse(this.programSettings.ParseHigherTaxa, "2. TaxaD value should be false.");
        }

        [Test]
        public void ProgramSettings_TaxaEPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, "Default TaxaE value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTaxaEProperty_SchouldBePersistent()
        {
            this.programSettings.ExpandLowerTaxa = true;
            Assert.IsTrue(this.programSettings.ExpandLowerTaxa, "1. TaxaE value should be true.");

            this.programSettings.ExpandLowerTaxa = false;
            Assert.IsFalse(this.programSettings.ExpandLowerTaxa, "2. TaxaE value should be false.");
        }

        [Test]
        public void ProgramSettings_TestFlagPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.TestFlag, "Default TestFlag value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfTestFlagProperty_SchouldBePersistent()
        {
            this.programSettings.TestFlag = true;
            Assert.IsTrue(this.programSettings.TestFlag, "1. TestFlag value should be true.");

            this.programSettings.TestFlag = false;
            Assert.IsFalse(this.programSettings.TestFlag, "2. TestFlag value should be false.");
        }

        [Test]
        public void ProgramSettings_UntagSplitPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.UntagSplit, "Default UntagSplit value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfUntagSplitProperty_SchouldBePersistent()
        {
            this.programSettings.UntagSplit = true;
            Assert.IsTrue(this.programSettings.UntagSplit, "1. UntagSplit value should be true.");

            this.programSettings.UntagSplit = false;
            Assert.IsFalse(this.programSettings.UntagSplit, "2. UntagSplit value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidateTaxaPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ValidateTaxa, "Default ValidateTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfValidateTaxaProperty_SchouldBePersistent()
        {
            this.programSettings.ValidateTaxa = true;
            Assert.IsTrue(this.programSettings.ValidateTaxa, "1. ValidateTaxa value should be true.");

            this.programSettings.ValidateTaxa = false;
            Assert.IsFalse(this.programSettings.ValidateTaxa, "2. ValidateTaxa value should be false.");
        }

        [Test]
        public void ProgramSettings_ZoobankCloneJsonPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, "Default ZoobankCloneJson value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankCloneJsonProperty_SchouldBePersistent()
        {
            this.programSettings.ZoobankCloneJson = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneJson, "1. ZoobankCloneJson value should be true.");

            this.programSettings.ZoobankCloneJson = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneJson, "2. ZoobankCloneJson value should be false.");
        }

        [Test]
        public void ProgramSettings_ZoobankCloneXmlPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, "Default ZoobankCloneXml value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankCloneXmlProperty_SchouldBePersistent()
        {
            this.programSettings.ZoobankCloneXml = true;
            Assert.IsTrue(this.programSettings.ZoobankCloneXml, "1. ZoobankCloneXml value should be true.");

            this.programSettings.ZoobankCloneXml = false;
            Assert.IsFalse(this.programSettings.ZoobankCloneXml, "2. ZoobankCloneXml value should be false.");
        }

        [Test]
        public void ProgramSettings_ZoobankGenerateRegistrationXmlPropertyInNewInstance_SchouldBeFalse()
        {
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, "Default ZoobankGenerateRegistrationXml value should be false.");
        }

        [Test]
        public void ProgramSettings_ValidChangesOfZoobankGenerateRegistrationXmlProperty_SchouldBePersistent()
        {
            this.programSettings.ZoobankGenerateRegistrationXml = true;
            Assert.IsTrue(this.programSettings.ZoobankGenerateRegistrationXml, "1. ZoobankGenerateRegistrationXml value should be true.");

            this.programSettings.ZoobankGenerateRegistrationXml = false;
            Assert.IsFalse(this.programSettings.ZoobankGenerateRegistrationXml, "2. ZoobankGenerateRegistrationXml value should be false.");
        }
    }
}