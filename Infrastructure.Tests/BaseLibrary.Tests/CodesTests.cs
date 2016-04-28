namespace ProcessingTools.BaseLibrary.Tests
{
    using System;
    using System.Xml;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CodesTests
    {
        private static XmlTextWriter writer;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            writer = new XmlTextWriter(Console.Out);
            writer.Formatting = Formatting.Indented;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            writer.Close();
        }

        public void TagKnownSpecimenCodes_Tests(string xmlString)
        {
            ////Codes codes = new Codes(config, xmlString);

            ////writer.WriteNode(codes.Xml.ToXmlReader(), true);

            ////List<SpecimenCode> prefixNumeric = codes.GetPrefixNumericCodes();
            ////foreach (SpecimenCode code in prefixNumeric)
            ////{
            ////    Console.WriteLine("{0} -> {1}", code.Prefix, code.Code);
            ////}

            ////codes.TagKnownSpecimenCodes(xpathProvider);

            ////writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagKnownSpecimenCodes_String7()
        {
            ////TagKnownSpecimenCodes_Tests(TestResourceStrings.String7);
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagKnownSpecimenCodes_String10()
        {
            ////TagKnownSpecimenCodes_Tests(TestResourceStrings.String10);
        }

        [TestMethod]
        [Timeout(5000)]
        public void Test_ListTablesFromDatabaseProvider_String7()
        {
            ////using (DataProvider dp = new DataProvider(config, TestResourceStrings.String7))
            ////{
            ////    foreach (string name in dp.ListTables())
            ////    {
            ////        Console.WriteLine(name);
            ////    }
            ////}
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagCodes_String7()
        {
            ////Codes codes = new Codes(config, TestResourceStrings.String7);
            ////using (DataProvider dataProvider = new DataProvider(config, codes.Xml))
            ////{

            ////    {
            ////        SpecimenCountTagger sp = new SpecimenCountTagger(config, codes.Xml);
            ////        sp.TagSpecimenCount(xpathProvider);
            ////        codes.Xml = sp.Xml;
            ////    }

            ////    writer.WriteNode(codes.Xml.ToXmlReader(), true);

            ////    codes.TagKnownSpecimenCodes(xpathProvider);
            ////    codes.TagInstitutions(xpathProvider, dataProvider);
            ////    codes.TagInstitutionalCodes(xpathProvider, dataProvider);
            ////    codes.TagSpecimenCodes(xpathProvider);

            ////    writer.WriteNode(codes.Xml.ToXmlReader(), true);
            ////}
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagEnvo_10154()
        {
            //////FileStream fs = new FileStream(@"C:\temp\10154-out.xml", FileMode.Create);
            //////XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8);
            //////xw.Formatting = Formatting.Indented;

            ////FileProcessor fp = new FileProcessor(config, @"C:\Users\bozhin\SkyDrive\Work\9949-abbrev.xml");
            ////fp.Read();

            ////config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";

            //////Envo envo = new Envo(config, TestResourceStrings.ZK10154);
            ////Envo envo = new Envo(config, fp.Xml);

            ////envo.Xml = fp.Xml;

            ////envo.Tag(xpathProvider);

            ////writer.WriteNode(envo.Xml.ToXmlReader(), true);
            //////xw.WriteNode(envo.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagCodes_10154()
        {
            ////config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";

            ////Codes codes = new Codes(config, TestResourceStrings.ZK10154);
            ////DataProvider dataProvider = new DataProvider(config, codes.Xml);

            ////codes.TagKnownSpecimenCodes(xpathProvider);

            ////{
            ////    Envo envo = new Envo(config, codes.Xml);
            ////    envo.Tag(xpathProvider);
            ////    codes.Xml = envo.Xml;
            ////}

            ////{
            ////    AbbreviationsTagger abbr = new AbbreviationsTagger(config, codes.Xml);
            ////    abbr.Tag();
            ////    codes.Xml = abbr.Xml;
            ////}

            ////{
            ////    DatesTagger dates = new DatesTagger(config, codes.Xml);
            ////    dates.Tag(xpathProvider);
            ////    codes.Xml = dates.Xml;
            ////}

            ////{
            ////    QuantitiesTagger quant = new QuantitiesTagger(config, codes.Xml);
            ////    quant.TagQuantities(xpathProvider);
            ////    quant.TagDeviation(xpathProvider);
            ////    quant.TagAltitude(xpathProvider);
            ////    codes.Xml = quant.Xml;
            ////}

            ////{
            ////    SpecimenCountTagger sc = new SpecimenCountTagger(config, codes.Xml);
            ////    sc.TagSpecimenCount(xpathProvider);
            ////    codes.Xml = sc.Xml;
            ////}

            ////{
            ////    GeoNamesTagger geo = new GeoNamesTagger(config, codes.Xml);
            ////    geo.Tag(xpathProvider, dataProvider);
            ////    codes.Xml = geo.Xml;
            ////}

            ////codes.TagInstitutions(xpathProvider, dataProvider);
            ////codes.TagInstitutionalCodes(xpathProvider, dataProvider);
            ////codes.TagSpecimenCodes(xpathProvider);

            ////writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagCodes_WithoutEnvo_10154()
        {
            ////Codes codes = new Codes(config, TestResourceStrings._10154);
            ////DataProvider dataProvider = new DataProvider(config, codes.Xml);

            ////codes.TagKnownSpecimenCodes(xpathProvider);

            ////{
            ////    AbbreviationsTagger abbr = new AbbreviationsTagger(config, codes.Xml);
            ////    abbr.Tag();
            ////    codes.Xml = abbr.Xml;
            ////}

            ////{
            ////    DatesTagger dates = new DatesTagger(config, codes.Xml);
            ////    dates.Tag(xpathProvider);
            ////    codes.Xml = dates.Xml;
            ////}

            ////{
            ////    QuantitiesTagger quant = new QuantitiesTagger(config, codes.Xml);
            ////    quant.TagQuantities(xpathProvider);
            ////    quant.TagDeviation(xpathProvider);
            ////    quant.TagAltitude(xpathProvider);
            ////    codes.Xml = quant.Xml;
            ////}

            ////{
            ////    SpecimenCountTagger sc = new SpecimenCountTagger(config, codes.Xml);
            ////    sc.TagSpecimenCount(xpathProvider);
            ////    codes.Xml = sc.Xml;
            ////}

            ////{
            ////    GeoNamesTagger geo = new GeoNamesTagger(config, codes.Xml);
            ////    geo.Tag(xpathProvider, dataProvider);
            ////    codes.Xml = geo.Xml;
            ////}

            ////codes.TagInstitutions(xpathProvider, dataProvider);
            ////codes.TagInstitutionalCodes(xpathProvider, dataProvider);
            //////codes.TagSpecimenCodes(xpathProvider);

            ////codes.XmlDocument.ClearTagsInWrongPositions();

            ////writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(10000)]
        [Ignore]
        public void Test_TagCodes_WithEnvo_10154()
        {
            ////FileProcessor fp = new FileProcessor(config, @"C:\Users\Bozhin Karaivanov\SkyDrive\Work\9915-abbr.xml");
            ////fp.Read();

            ////Codes codes = new Codes(config, fp.Xml);
            ////DataProvider dataProvider = new DataProvider(config, codes.Xml);

            //////codes.TagKnownSpecimenCodes(xpathProvider);

            ////{
            ////    config.EnvoResponseOutputXmlFileName = @"C:\temp\envo-out.xml";
            ////    Envo envo = new Envo(config, codes.Xml);
            ////    envo.Tag(xpathProvider);
            ////    codes.Xml = envo.Xml;
            ////}

            ////{
            ////    AbbreviationsTagger abbr = new AbbreviationsTagger(config, codes.Xml);
            ////    abbr.Tag();
            ////    codes.Xml = abbr.Xml;
            ////}

            ////{
            ////    DatesTagger dates = new DatesTagger(config, codes.Xml);
            ////    dates.Tag(xpathProvider);
            ////    codes.Xml = dates.Xml;
            ////}

            ////{
            ////    QuantitiesTagger quant = new QuantitiesTagger(config, codes.Xml);
            ////    quant.TagQuantities(xpathProvider);
            ////    quant.TagDeviation(xpathProvider);
            ////    quant.TagAltitude(xpathProvider);
            ////    codes.Xml = quant.Xml;
            ////}

            ////{
            ////    SpecimenCountTagger sc = new SpecimenCountTagger(config, codes.Xml);
            ////    sc.TagSpecimenCount(xpathProvider);
            ////    codes.Xml = sc.Xml;
            ////}

            ////{
            ////    GeoNamesTagger geo = new GeoNamesTagger(config, codes.Xml);
            ////    geo.Tag(xpathProvider, dataProvider);
            ////    codes.Xml = geo.Xml;
            ////}

            ////codes.TagInstitutions(xpathProvider, dataProvider);
            ////codes.TagInstitutionalCodes(xpathProvider, dataProvider);
            //////codes.TagSpecimenCodes(xpathProvider);

            ////codes.XmlDocument.ClearTagsInWrongPositions();

            ////writer.WriteNode(codes.Xml.ToXmlReader(), true);
        }

        [TestMethod]
        [Timeout(20000)]
        [Ignore]
        public void Codes_MainAlgirithmTest()
        {
            ////XmlDocument xml = new XmlDocument(Config.TaxPubNamespceManager().NameTable);
            ////xml.PreserveWhitespace = true;
            ////xml.Load(@"C:\Users\Bozhin Karaivanov\SkyDrive\Work\9949-abbrev.xml");

            ////{
            ////    AbbreviationsTagger abbr = new AbbreviationsTagger(config, xml.OuterXml);
            ////    abbr.Tag();
            ////    xml.LoadXml(abbr.Xml);
            ////}

            ////{
            ////    SpecimenCountTagger specimenCountTagger = new SpecimenCountTagger(config, xml.OuterXml);
            ////    specimenCountTagger.TagSpecimenCount(xpathProvider);
            ////    xml.LoadXml(specimenCountTagger.Xml);
            ////}

            ////{
            ////    QuantitiesTagger quantitiesTagger = new QuantitiesTagger(config, xml.OuterXml);
            ////    quantitiesTagger.TagQuantities(xpathProvider);
            ////    quantitiesTagger.TagDeviation(xpathProvider);
            ////    quantitiesTagger.TagAltitude(xpathProvider);
            ////    xml.LoadXml(quantitiesTagger.Xml);
            ////}

            ////{
            ////    DatesTagger datesTagger = new DatesTagger(config, xml.OuterXml);
            ////    datesTagger.Tag(xpathProvider);
            ////    xml.LoadXml(datesTagger.Xml);
            ////}

            ////Console.WriteLine(xml.OuterXml);
        }
    }
}
