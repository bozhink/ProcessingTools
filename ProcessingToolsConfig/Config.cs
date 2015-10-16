namespace ProcessingTools.Configurator
{
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;

    public partial class Config
    {
        private static XmlNamespaceManager taxPubNamespaceManager = null;

        private SchemaType articleSchemaType;
        private bool articleSchemaTypeStyleIsLockedForModification;

        private bool tagWholeDocument;
        private bool tagWholeDocumentIsLockedForModification;

        public Config()
        {
            this.articleSchemaType = SchemaType.System;
            this.articleSchemaTypeStyleIsLockedForModification = false;

            this.tagWholeDocument = false;
            this.tagWholeDocumentIsLockedForModification = false;
        }

        public static Encoding DefaultEncoding
        {
            get
            {
                return new UTF8Encoding(false);
            }
        }

        public string EnvoResponseOutputXmlFileName { get; set; }

        public string GnrOutputFileName { get; set; }

        public SchemaType ArticleSchemaType
        {
            get
            {
                this.articleSchemaTypeStyleIsLockedForModification = true;
                return this.articleSchemaType;
            }

            set
            {
                if (!this.articleSchemaTypeStyleIsLockedForModification)
                {
                    this.articleSchemaType = value;
                }

                this.articleSchemaTypeStyleIsLockedForModification = true;
            }
        }

        public bool TagWholeDocument
        {
            get
            {
                this.tagWholeDocumentIsLockedForModification = true;
                return this.tagWholeDocument;
            }

            set
            {
                if (!this.tagWholeDocumentIsLockedForModification)
                {
                    this.tagWholeDocument = value;
                }

                this.tagWholeDocumentIsLockedForModification = true;
            }
        }

        public static XmlNamespaceManager TaxPubNamespceManager()
        {
            object syncLock = new object();
            if (taxPubNamespaceManager == null)
            {
                lock (syncLock)
                {
                    if (taxPubNamespaceManager == null)
                    {
                        taxPubNamespaceManager = TaxPubNamespceManager(new XmlDocument().NameTable);
                    }
                }
            }

            return taxPubNamespaceManager;
        }

        public static XmlNamespaceManager TaxPubNamespceManager(XmlDocument xmlDocument)
        {
            return TaxPubNamespceManager(xmlDocument.NameTable);
        }

        public static XmlNamespaceManager TaxPubNamespceManager(XmlNameTable nameTable)
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(nameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }
    }

    [DataContract]
    public partial class Config
    {
        [DataMember(Name = "blackListCleanXslPath")]
        public string BlackListCleanXslPath { get; set; }

        [DataMember(Name = "blackListXmlFilePath")]
        public string BlackListXmlFilePath { get; set; }

        [DataMember(Name = "codesRemoveNonCodeNodes")]
        public string CodesRemoveNonCodeNodes { get; set; }

        [DataMember(Name = "environmentsDataSourceString")]
        public string EnvironmentsDataSourceString { get; set; }

        [DataMember(Name = "envoTermsWebServiceTransformXslPath")]
        public string EnvoTermsWebServiceTransformXslPath { get; set; }

        [DataMember(Name = "floraDistrinctTaxaXslPath")]
        public string FloraDistrinctTaxaXslPath { get; set; }

        [DataMember(Name = "floraExtractedTaxaListPath")]
        public string FloraExtractedTaxaListPath { get; set; }

        [DataMember(Name = "floraExtractTaxaPartsOutputPath")]
        public string FloraExtractTaxaPartsOutputPath { get; set; }

        [DataMember(Name = "floraExtractTaxaPartsXslPath")]
        public string FloraExtractTaxaPartsXslPath { get; set; }

        [DataMember(Name = "floraExtractTaxaXslPath")]
        public string FloraExtractTaxaXslPath { get; set; }

        [DataMember(Name = "floraGenerateTemplatesXslPath")]
        public string FloraGenerateTemplatesXslPath { get; set; }

        [DataMember(Name = "floraTemplatesOutputXmlPath")]
        public string FloraTemplatesOutputXmlPath { get; set; }

        [DataMember(Name = "formatXslNlmToSystem")]
        public string FormatXslNlmToSystem { get; set; }

        [DataMember(Name = "formatXslSystemToNlm")]
        public string FormatXslSystemToNlm { get; set; }

        [DataMember(Name = "mainDictionaryDataSourceString")]
        public string MainDictionaryDataSourceString { get; set; }

        [DataMember(Name = "nlmInitialFormatXslPath")]
        public string NlmInitialFormatXslPath { get; set; }

        [DataMember(Name = "rankListCleanXslPath")]
        public string RankListCleanXslPath { get; set; }

        [DataMember(Name = "rankListXmlFilePath")]
        public string RankListXmlFilePath { get; set; }

        [DataMember(Name = "referencesGetReferencesXmlPath")]
        public string ReferencesGetReferencesXmlPath { get; set; }

        [DataMember(Name = "referencesGetReferencesXslPath")]
        public string ReferencesGetReferencesXslPath { get; set; }

        [DataMember(Name = "referencesSortReferencesXslPath")]
        public string ReferencesSortReferencesXslPath { get; set; }

        [DataMember(Name = "referencesTagTemplateXmlPath")]
        public string ReferencesTagTemplateXmlPath { get; set; }

        [DataMember(Name = "referencesTagTemplateXslPath")]
        public string ReferencesTagTemplateXslPath { get; set; }

        [DataMember(Name = "systemInitialFormatXslPath")]
        public string SystemInitialFormatXslPath { get; set; }

        [DataMember(Name = "tempDirectoryPath")]
        public string TempDirectoryPath { get; set; }

        [DataMember(Name = "textContentXslFileName")]
        public string TextContentXslFileName { get; set; }

        [DataMember(Name = "whiteListCleanXslPath")]
        public string WhiteListCleanXslPath { get; set; }

        [DataMember(Name = "whiteListXmlFilePath")]
        public string WhiteListXmlFilePath { get; set; }

        [DataMember(Name = "zoobankNlmXslPath")]
        public string ZoobankNlmXslPath { get; set; }
    }
}