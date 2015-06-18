using System.Xml;

namespace Base
{
    public class ConfigBuilder
    {
        /// <summary>
        /// This method reads a config Xml file and builds a config object from it.
        /// </summary>
        /// <param name="configFilePath">full path of the config fule</param>
        /// <returns>initialized config object</returns>
        public static Config CreateConfig(string configFilePath)
        {
            Config config = new Config();
            XmlDocument configXml = null;

            try
            {
                configXml.Load(configFilePath);
            }
            catch (System.Exception e)
            {
                Alert.Message("Error parsing config xml");
                Alert.Message(e.Message);
            }
            finally
            {
                if (configXml != null)
                {
                    config.temp = configXml["config"]["temp"].InnerText;

                    config.taxaReplacesQueryXsl = configXml["config"]["taxa-replaces-query-xsl-path"].InnerText;
                    config.taxaReplacesQueryFile = configXml["config"]["taxa-replaces-query-xml-path"].InnerText;
                    config.taxaBlackListXml = configXml["config"]["black-list-xml-file-path"].InnerText;
                    config.taxaWhiteListXml = configXml["config"]["white-list-xml-file-path"].InnerText;
                    config.taxaRankListXml = configXml["config"]["rank-list-xml-file-path"].InnerText;

                    config.getTaxa0Xsl = configXml["config"]["get-taxa-0-path"].InnerText;
                    config.getTaxa1Xsl = configXml["config"]["get-taxa-1-path"].InnerText;
                    config.getTaxaPlain0Xsl = configXml["config"]["get-taxa-plain-0-path"].InnerText;
                    config.getTaxaPlain1Xsl = configXml["config"]["get-taxa-plain-1-path"].InnerText;

                    config.taxaExpandXsl = configXml["config"]["taxa-expand-xsl-path"].InnerText;

                    config.floraDistrinctTaxaXsl = configXml["config"]["flora-distrinct-taxa-xsl-path"].InnerText;
                    config.floraExtractedTaxaList = configXml["config"]["flora-extracted-taxa-list-path"].InnerText;
                    config.floraExtractTaxaPartsOutput = configXml["config"]["flora-extract-taxa-parts-output-path"].InnerText;
                    config.floraExtractTaxaPartsXsl = configXml["config"]["flora-extract-taxa-parts-xsl-path"].InnerText;
                    config.floraExtractTaxaXsl = configXml["config"]["flora-extract-taxa-xsl-path"].InnerText;
                    config.floraGenerateTemplatesXsl = configXml["config"]["flora-generate-templates-xsl-path"].InnerText;
                    config.floraTemplatesOutputXml = configXml["config"]["flora-templates-output-xml-path"].InnerText;

                    config.referencesTagTemplateXslPath = configXml["config"]["references-tag-template-xsl-path"].InnerText;
                    config.referencesTagTemplateXmlPath = configXml["config"]["references-tag-template-xml-path"].InnerText;
                    config.referencesGetReferencesXslPath = configXml["config"]["references-get-references-xsl-path"].InnerText;
                    config.referencesSortReferencesXslPath = configXml["config"]["references-sort-references-xsl-path"].InnerText;
                    config.referencesGetReferencesXmlPath = configXml["config"]["references-get-references-xml-path"].InnerText;

                    config.systemInitialFormat = configXml["config"]["system-initial-format-xsl-path"].InnerText;
                    config.nlmInitialFormatXslPath = configXml["config"]["nlm-initial-format-xsl-path"].InnerText;

                    config.zoobankNlmPath = configXml["config"]["zoobank-nlm-xsl-path"].InnerText;

                    config.formatXslNlmToSystem = configXml["config"]["format-nlm-to-system"].InnerText;
                    config.formatXslSystemToNlm = configXml["config"]["format-system-to-nlm"].InnerText;
                }
            }

            return config;
        }
    }

    public class Config
    {
        public string taxaReplacesQueryXsl { get; set; }

        public string taxaReplacesQueryFile { get; set; }

        public string taxaBlackListXml { get; set; }

        public string taxaWhiteListXml { get; set; }

        public string taxaRankListXml { get; set; }

        public string getTaxa0Xsl { get; set; }

        public string getTaxa1Xsl { get; set; }

        public string getTaxaPlain0Xsl { get; set; }

        public string getTaxaPlain1Xsl { get; set; }

        public string taxaExpandXsl { get; set; }

        // XSL to get distinct values of extracted taxa
        public string floraDistrinctTaxaXsl { get; set; }

        // Name of the output xml file containing initial extracted taxa list
        public string floraExtractedTaxaList { get; set; }

        public string floraExtractTaxaPartsOutput { get; set; }

        public string floraExtractTaxaPartsXsl { get; set; }

        // XSL to extract taxa from input xml
        public string floraExtractTaxaXsl { get; set; }

        public string floraGenerateTemplatesXsl { get; set; }

        // Output Xml file for Flora templates
        public string floraTemplatesOutputXml { get; set; }

        /*
         * References
         */
        public string referencesTagTemplateXslPath { get; set; }

        public string referencesTagTemplateXmlPath { get; set; }

        public string referencesGetReferencesXslPath { get; set; }

        public string referencesSortReferencesXslPath { get; set; }

        public string referencesGetReferencesXmlPath { get; set; }

        /*
         * ZooBank NLM
         */
        public string zoobankNlmPath { get; set; }

        /*
         * Format
         */
        public string systemInitialFormat { get; set; }

        public string nlmInitialFormatXslPath { get; set; }

        public string formatXslNlmToSystem { get; set; }

        public string formatXslSystemToNlm { get; set; }

        /*
         * Directories
         */
        // temp directory path
        public string temp { get; set; }

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
