namespace ProcessingTools
{
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;

    public partial class Config
    {
        private static XmlNamespaceManager taxPubNamespaceManager = null;

        private bool nlmStyle;
        private bool nlmStyleIsLockedForModification;

        private bool tagWholeDocument;
        private bool tagWholeDocumentIsLockedForModification;

        public Config()
        {
            this.nlmStyle = false;
            this.nlmStyleIsLockedForModification = false;

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

        public bool NlmStyle
        {
            get
            {
                this.nlmStyleIsLockedForModification = true;
                return this.nlmStyle;
            }

            set
            {
                if (!this.nlmStyleIsLockedForModification)
                {
                    this.nlmStyle = value;
                }

                this.nlmStyleIsLockedForModification = true;
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
        [DataMember]
        public string blackListCleanXslPath { get; set; }

        [DataMember]
        public string blackListXmlFilePath { get; set; }

        [DataMember]
        public string codesRemoveNonCodeNodes { get; set; }

        [DataMember]
        public string environmentsDataSourceString { get; set; }

        [DataMember]
        public string envoTermsWebServiceTransformXslPath { get; set; }

        [DataMember]
        public string floraDistrinctTaxaXslPath { get; set; }

        [DataMember]
        public string floraExtractedTaxaListPath { get; set; }

        [DataMember]
        public string floraExtractTaxaPartsOutputPath { get; set; }

        [DataMember]
        public string floraExtractTaxaPartsXslPath { get; set; }

        [DataMember]
        public string floraExtractTaxaXslPath { get; set; }

        [DataMember]
        public string floraGenerateTemplatesXslPath { get; set; }

        [DataMember]
        public string floraTemplatesOutputXmlPath { get; set; }

        [DataMember]
        public string formatXslNlmToSystem { get; set; }

        [DataMember]
        public string formatXslSystemToNlm { get; set; }

        [DataMember]
        public string mainDictionaryDataSourceString { get; set; }

        [DataMember]
        public string nlmInitialFormatXslPath { get; set; }

        [DataMember]
        public string rankListCleanXslPath { get; set; }

        [DataMember]
        public string rankListXmlFilePath { get; set; }

        [DataMember]
        public string referencesGetReferencesXmlPath { get; set; }

        [DataMember]
        public string referencesGetReferencesXslPath { get; set; }

        [DataMember]
        public string referencesSortReferencesXslPath { get; set; }

        [DataMember]
        public string referencesTagTemplateXmlPath { get; set; }

        [DataMember]
        public string referencesTagTemplateXslPath { get; set; }

        [DataMember]
        public string systemInitialFormatXslPath { get; set; }

        [DataMember]
        public string tempDirectoryPath { get; set; }
        [DataMember]
        public string textContentXslFileName { get; set; }

        [DataMember]
        public string whiteListCleanXslPath { get; set; }

        [DataMember]
        public string whiteListXmlFilePath { get; set; }
        [DataMember]
        public string zoobankNlmXslPath { get; set; }
    }
}