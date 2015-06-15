using System.Xml;

namespace Base
{

    public class Config
    {
        private XmlDocument configXml;

        public Config()
        {
            configXml = null;
        }

        public Config(string configXmlFilePath)
        {
            configXml = new XmlDocument();
            try
            {
                configXml.Load(configXmlFilePath);
            }
            catch (System.Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 1);
            }
            finally
            {
                if (configXml != null)
                {
                    ParseConfigXml();
                }
            }
        }

        public Config(XmlDocument xml)
        {
            configXml = xml;
            if (configXml != null)
            {
                ParseConfigXml();
            }
        }

        // Global file names
        private string _taxaReplacesQueryXsl;
        public string taxaReplacesQueryXsl
        {
            get { return _taxaReplacesQueryXsl; }
            set { _taxaReplacesQueryXsl = value; }
        }

        private string _taxaReplacesQueryFile;
        public string taxaReplacesQueryFile
        {
            get { return _taxaReplacesQueryFile; }
            set { _taxaReplacesQueryFile = value; }
        }

        private string _taxaBlackListXml;
        public string taxaBlackListXml
        {
            get { return _taxaBlackListXml; }
            set { _taxaBlackListXml = value; }
        }

        private string _taxaWhiteListXml;
        public string taxaWhiteListXml
        {
            get { return _taxaWhiteListXml; }
            set { _taxaWhiteListXml = value; }
        }

        private string _taxaRankListXml;
        public string taxaRankListXml
        {
            get { return _taxaRankListXml; }
            set { _taxaRankListXml = value; }
        }

        private string _getTaxa0Xsl;
        public string getTaxa0Xsl
        {
            get { return _getTaxa0Xsl; }
            set { _getTaxa0Xsl = value; }
        }

        private string _getTaxa1Xsl;
        public string getTaxa1Xsl
        {
            get { return _getTaxa1Xsl; }
            set { _getTaxa1Xsl = value; }
        }

        private string _getTaxaPlain0Xsl;
        public string getTaxaPlain0Xsl
        {
            get { return _getTaxaPlain0Xsl; }
            set { _getTaxaPlain0Xsl = value; }
        }

        private string _getTaxaPlain1Xsl;
        public string getTaxaPlain1Xsl
        {
            get { return _getTaxaPlain1Xsl; }
            set { _getTaxaPlain1Xsl = value; }
        }

        private string _taxaExpandXsl;
        public string taxaExpandXsl
        {
            get { return _taxaExpandXsl; }
            set { _taxaExpandXsl = value; }
        }

        private string _floraDistrinctTaxaXsl; // XSL to get distinct values of extracted taxa
        public string floraDistrinctTaxaXsl
        {
            get { return _floraDistrinctTaxaXsl; }
            set { _floraDistrinctTaxaXsl = value; }
        }

        private string _floraExtractedTaxaList; // Name of the output xml file containing initial extracted taxa list
        public string floraExtractedTaxaList
        {
            get { return _floraExtractedTaxaList; }
            set { _floraExtractedTaxaList = value; }
        }

        private string _floraExtractTaxaPartsOutput;
        public string floraExtractTaxaPartsOutput
        {
            get { return _floraExtractTaxaPartsOutput; }
            set { _floraExtractTaxaPartsOutput = value; }
        }

        private string _floraExtractTaxaPartsXsl;
        public string floraExtractTaxaPartsXsl
        {
            get { return _floraExtractTaxaPartsXsl; }
            set { _floraExtractTaxaPartsXsl = value; }
        }

        private string _floraExtractTaxaXsl; // XSL to extract taxa from input xml
        public string floraExtractTaxaXsl
        {
            get { return _floraExtractTaxaXsl; }
            set { _floraExtractTaxaXsl = value; }
        }

        private string _floraGenerateTemplatesXsl;
        public string floraGenerateTemplatesXsl
        {
            get { return _floraGenerateTemplatesXsl; }
            set { _floraGenerateTemplatesXsl = value; }
        }

        private string _floraTemplatesOutputXml; // Output Xml file for Flora templates
        public string floraTemplatesOutputXml
        {
            get { return _floraTemplatesOutputXml; }
            set { _floraTemplatesOutputXml = value; }
        }

        /*
         * References
         */
        private string _referencesTagTemplateXslPath;
        public string referencesTagTemplateXslPath
        {
            get { return _referencesTagTemplateXslPath; }
            set { _referencesTagTemplateXslPath = value; }
        }

        private string _referencesTagTemplateXmlPath;
        public string referencesTagTemplateXmlPath
        {
            get { return _referencesTagTemplateXmlPath; }
            set { _referencesTagTemplateXmlPath = value; }
        }

        private string _referencesGetReferencesXslPath;
        public string referencesGetReferencesXslPath
        {
            get { return _referencesGetReferencesXslPath; }
            set { _referencesGetReferencesXslPath = value; }
        }

        private string _referencesSortReferencesXslPath;
        public string referencesSortReferencesXslPath
        {
            get { return _referencesSortReferencesXslPath; }
            set { _referencesSortReferencesXslPath = value; }
        }

        private string _referencesGetReferencesXmlPath;
        public string referencesGetReferencesXmlPath
        {
            get { return _referencesGetReferencesXmlPath; }
            set { _referencesGetReferencesXmlPath = value; }
        }


        private string _systemInitialFormat;
        public string systemInitialFormat
        {
            get { return _systemInitialFormat; }
            set { _systemInitialFormat = value; }
        }

        private string _nlmInitialFormatXslPath;
        public string nlmInitialFormatXslPath
        {
            get { return _nlmInitialFormatXslPath; }
            set { _nlmInitialFormatXslPath = value; }
        }

        /*
         * ZooBank NLM
         */
        private string _zoobankNlmPath;
        public string zoobankNlmPath
        {
            get { return _zoobankNlmPath; }
            set { _zoobankNlmPath = value; }
        }

        /*
         * Format
         */
        private string _formatXslNlmToSystem;
        public string formatXslNlmToSystem
        {
            get { return _formatXslNlmToSystem; }
            set { _formatXslNlmToSystem = value; }
        }

        private string _formatXslSystemToNlm;
        public string formatXslSystemToNlm
        {
            get { return _formatXslSystemToNlm; }
            set { _formatXslSystemToNlm = value; }
        }

        private string _temp;
        public string temp
        {
            get { return _temp; }
            set { _temp = value; }
        }

        private void ParseConfigXml()
        {
            _temp = configXml["config"]["temp"].InnerText;

            _taxaReplacesQueryXsl = configXml["config"]["taxa-replaces-query-xsl-path"].InnerText;
            _taxaReplacesQueryFile = configXml["config"]["taxa-replaces-query-xml-path"].InnerText;
            _taxaBlackListXml = configXml["config"]["black-list-xml-file-path"].InnerText;
            _taxaWhiteListXml = configXml["config"]["white-list-xml-file-path"].InnerText;
            _taxaRankListXml = configXml["config"]["rank-list-xml-file-path"].InnerText;

            _getTaxa0Xsl = configXml["config"]["get-taxa-0-path"].InnerText;
            _getTaxa1Xsl = configXml["config"]["get-taxa-1-path"].InnerText;
            _getTaxaPlain0Xsl = configXml["config"]["get-taxa-plain-0-path"].InnerText;
            _getTaxaPlain1Xsl = configXml["config"]["get-taxa-plain-1-path"].InnerText;

            _taxaExpandXsl = configXml["config"]["taxa-expand-xsl-path"].InnerText;

            _floraDistrinctTaxaXsl = configXml["config"]["flora-distrinct-taxa-xsl-path"].InnerText;
            _floraExtractedTaxaList = configXml["config"]["flora-extracted-taxa-list-path"].InnerText;
            _floraExtractTaxaPartsOutput = configXml["config"]["flora-extract-taxa-parts-output-path"].InnerText;
            _floraExtractTaxaPartsXsl = configXml["config"]["flora-extract-taxa-parts-xsl-path"].InnerText;
            _floraExtractTaxaXsl = configXml["config"]["flora-extract-taxa-xsl-path"].InnerText;
            _floraGenerateTemplatesXsl = configXml["config"]["flora-generate-templates-xsl-path"].InnerText;
            _floraTemplatesOutputXml = configXml["config"]["flora-templates-output-xml-path"].InnerText;

            _referencesTagTemplateXslPath = configXml["config"]["references-tag-template-xsl-path"].InnerText;
            _referencesTagTemplateXmlPath = configXml["config"]["references-tag-template-xml-path"].InnerText;
            _referencesGetReferencesXslPath = configXml["config"]["references-get-references-xsl-path"].InnerText;
            _referencesSortReferencesXslPath = configXml["config"]["references-sort-references-xsl-path"].InnerText;
            _referencesGetReferencesXmlPath = configXml["config"]["references-get-references-xml-path"].InnerText;

            _systemInitialFormat = configXml["config"]["system-initial-format-xsl-path"].InnerText;
            //_nlmInitialFormatXslPath = configXml["config"]["nlm-initial-format-xsl-path"].InnerText;

            _zoobankNlmPath = configXml["config"]["zoobank-nlm-xsl-path"].InnerText;

            _formatXslNlmToSystem = configXml["config"]["format-nlm-to-system"].InnerText;
            _formatXslSystemToNlm = configXml["config"]["format-system-to-nlm"].InnerText;
        }

        /*
         * Tagging parameters
         */
        public bool NlmStyle { get; set; }

        public bool TagWholeDocument { get; set; }

        /*
         * Taxa XML file names
         */
        public string ExtractedTaxaXml { get; set; }
        public string ExpandedTaxaXml { get; set; }
    }
}
