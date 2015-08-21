using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace ProcessingTools
{
    public static class ConfigBuilder
    {
        /// <summary>
        /// This method reads a config Xml file and builds a config object from it.
        /// </summary>
        /// <param name="configFilePath">full path of the config fule</param>
        /// <returns>initialized config object</returns>
        public static Config CreateConfig(string configFilePath)
        {
            Config config = null;
            try
            {
                string jsonText = File.ReadAllText(configFilePath);
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Config));
                config = (Config)data.ReadObject(stream);
            }
            catch (Exception e)
            {
                Alert.Log("ReadAllLinesToString Exception:");
                Alert.Log(e.Message);
                Alert.Exit(1);
            }

            return config;
        }
    }

    [DataContract]
    public partial class Config
    {
        [DataMember]
        public string tempDirectoryPath { get; set; }

        [DataMember]
        public string blackListXmlFilePath { get; set; }
        [DataMember]
        public string whiteListXmlFilePath { get; set; }
        [DataMember]
        public string rankListXmlFilePath { get; set; }

        [DataMember]
        public string rankListCleanXslPath { get; set; }
        [DataMember]
        public string whiteListCleanXslPath { get; set; }
        [DataMember]
        public string blackListCleanXslPath { get; set; }

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
        public string referencesTagTemplateXslPath { get; set; }
        [DataMember]
        public string referencesTagTemplateXmlPath { get; set; }
        [DataMember]
        public string referencesGetReferencesXslPath { get; set; }
        [DataMember]
        public string referencesSortReferencesXslPath { get; set; }
        [DataMember]
        public string referencesGetReferencesXmlPath { get; set; }

        [DataMember]
        public string zoobankNlmXslPath { get; set; }

        [DataMember]
        public string formatXslNlmToSystem { get; set; }
        [DataMember]
        public string formatXslSystemToNlm { get; set; }
        [DataMember]
        public string systemInitialFormatXslPath { get; set; }
        [DataMember]
        public string nlmInitialFormatXslPath { get; set; }

        [DataMember]
        public string environmentsDataSourceString { get; set; }

        [DataMember]
        public string mainDictionaryDataSourceString { get; set; }

        [DataMember]
        public string codesRemoveNonCodeNodes { get; set; }

        [DataMember]
        public string textContentXslFileName { get; set; }

        [DataMember]
        public string envoTermsWebServiceTransformXslPath { get; set; }

        /*
         * Tagging parameters
         */
        public bool NlmStyle { get; set; }

        public bool TagWholeDocument { get; set; }

        public string EnvoResponseOutputXmlFileName { get; set; }
    }

    public partial class Config
    {
        public static Encoding DefaultEncoding
        {
            get
            {
                return new UTF8Encoding(false);
            }
        }

        public static XmlNamespaceManager TaxPubNamespceManager()
        {
            return TaxPubNamespceManager(new XmlDocument().NameTable);
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
}
