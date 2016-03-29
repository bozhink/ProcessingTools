namespace ProcessingTools.Configurator
{
    using System.Runtime.Serialization;

    public partial class Config
    {
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
    }

    [DataContract]
    public partial class Config
    {
        [DataMember(Name = "blackListCleanXslPath")]
        public string BlackListCleanXslPath { get; set; }

        [DataMember(Name = "blackListXmlFilePath")]
        public string BlackListXmlFilePath { get; set; }

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

        [DataMember(Name = "mainDictionaryDataSourceString")]
        public string MainDictionaryDataSourceString { get; set; }

        [DataMember(Name = "rankListCleanXslPath")]
        public string RankListCleanXslPath { get; set; }

        [DataMember(Name = "rankListXmlFilePath")]
        public string RankListXmlFilePath { get; set; }

        [DataMember(Name = "referencesGetReferencesXmlPath")]
        public string ReferencesGetReferencesXmlPath { get; set; }

        [DataMember(Name = "referencesGetReferencesXslPath")]
        public string ReferencesGetReferencesXslPath { get; set; }

        [DataMember(Name = "referencesTagTemplateXmlPath")]
        public string ReferencesTagTemplateXmlPath { get; set; }

        [DataMember(Name = "referencesTagTemplateXslPath")]
        public string ReferencesTagTemplateXslPath { get; set; }

        [DataMember(Name = "tempDirectoryPath")]
        public string TempDirectoryPath { get; set; }

        [DataMember(Name = "whiteListCleanXslPath")]
        public string WhiteListCleanXslPath { get; set; }

        [DataMember(Name = "whiteListXmlFilePath")]
        public string WhiteListXmlFilePath { get; set; }
    }
}