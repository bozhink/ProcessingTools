using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
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
            Config config = null;
            try
            {
                string jsonText = FileProcessor.GetStringContent(configFilePath);
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                DataContractJsonSerializer data = new DataContractJsonSerializer(typeof(Config));
                config = (Config)data.ReadObject(stream);
            }
            catch (Exception e)
            {
                Alert.Message("ReadAllLinesToString Exception:");
                Alert.Message(e.Message);
                Alert.Exit(1);
            }

            return config;
        }
    }

    [DataContract]
    public class Config
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
        public string taxaReplacesQueryXslPath { get; set; }
        [DataMember]
        public string taxaReplacesQueryXmlPath { get; set; }
        [DataMember]
        public string taxaExpandXslPath { get; set; }

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
        public string codesRemoveNonCodeNodes { get; set; }

        /*
         * Tagging parameters
         */
        public bool NlmStyle { get; set; }

        public bool TagWholeDocument { get; set; }
    }
}
