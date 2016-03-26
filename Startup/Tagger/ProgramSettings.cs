namespace ProcessingTools.Tagger
{
    using System;
    using System.Collections.Generic;

    using ProcessingTools.Bio.Taxonomy.Services.Data;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Configurator;

    public class ProgramSettings
    {
        private Lazy<IStringTaxonomicListDataService> blackList;
        private Lazy<IStringTaxonomicListDataService> whiteList;

        public ProgramSettings()
        {
            this.FileNames = new List<string>();
            this.CalledControllers = new List<Type>();

            this.Config = null;

            this.HigherStructrureXpath = "//article";

            this.ExtractHigherTaxa = false;
            this.ExtractLowerTaxa = false;
            this.ExtractTaxa = false;
            this.InitialFormat = false;
            this.FormatTreat = false;
            this.ParseBySection = false;
            this.ParseCoordinates = false;
            this.ParseReferences = false;
            this.ParseTreatmentMetaWithAphia = false;
            this.ParseTreatmentMetaWithCol = false;
            this.ParseTreatmentMetaWithGbif = false;
            this.QueryReplace = false;
            this.ParseHigherAboveGenus = false;
            this.ParseHigherBySuffix = false;
            this.ParseHigherWithAphia = false;
            this.ParseHigherWithCoL = false;
            this.ParseHigherWithGbif = false;
            this.ResolveMediaTypes = false;
            this.TagAbbreviations = false;
            this.TagCodes = false;
            this.TagCoordinates = false;
            this.TagDoi = false;
            this.TagEnvironmentTerms = false;
            this.TagEnvironmentTermsWithExtract = false;
            this.TagFloats = false;
            this.TagReferences = false;
            this.TagTableFn = false;
            this.TagWebLinks = false;
            this.TagLowerTaxa = false;
            this.TagHigherTaxa = false;
            this.ParseLowerTaxa = false;
            this.ParseHigherTaxa = false;
            this.ExpandLowerTaxa = false;
            this.UntagSplit = false;
            this.ValidateTaxa = false;
            this.RunXslTransform = false;
            this.ZoobankCloneJson = false;
            this.ZoobankCloneXml = false;
            this.ZoobankGenerateRegistrationXml = false;

            this.blackList = new Lazy<IStringTaxonomicListDataService>(() => new StringTaxonomicListDataService(this.Config.BlackListXmlFilePath));
            this.whiteList = new Lazy<IStringTaxonomicListDataService>(() => new StringTaxonomicListDataService(this.Config.WhiteListXmlFilePath));
        }

        public IStringTaxonomicListDataService BlackList => this.blackList.Value;

        public IStringTaxonomicListDataService WhiteList => this.whiteList.Value;

        public Config Config { get; set; }

        public ICollection<string> FileNames { get; set; }

        public ICollection<Type> CalledControllers { get; set; }

        public bool ExtractHigherTaxa { get; set; }

        public bool ExtractLowerTaxa { get; set; }

        public bool ExtractTaxa { get; set; }

        public bool InitialFormat { get; set; }

        public bool FormatTreat { get; set; }

        public string HigherStructrureXpath { get; set; }

        public bool ParseBySection { get; set; }

        public bool ParseCoordinates { get; set; }

        public bool ParseReferences { get; set; }

        public bool ParseTreatmentMetaWithAphia { get; set; }

        public bool ParseTreatmentMetaWithCol { get; set; }

        public bool ParseTreatmentMetaWithGbif { get; set; }

        public bool QueryReplace { get; set; }

        public bool ParseHigherAboveGenus { get; set; }

        public bool ParseHigherBySuffix { get; set; }

        public bool ParseHigherWithAphia { get; set; }

        public bool ParseHigherWithCoL { get; set; }

        public bool ParseHigherWithGbif { get; set; }

        public bool ResolveMediaTypes { get; set; }

        public bool TagAbbreviations { get; set; }

        public bool TagCodes { get; set; }

        public bool TagCoordinates { get; set; }

        public bool TagDoi { get; set; }

        public bool TagEnvironmentTerms { get; set; }

        public bool TagEnvironmentTermsWithExtract { get; set; }

        public bool TagFloats { get; set; }

        public bool TagReferences { get; set; }

        public bool TagTableFn { get; set; }

        public bool TagWebLinks { get; set; }

        public bool TagLowerTaxa { get; set; }

        public bool TagHigherTaxa { get; set; }

        public bool ParseLowerTaxa { get; set; }

        public bool ParseHigherTaxa { get; set; }

        public bool ExpandLowerTaxa { get; set; }

        public bool UntagSplit { get; set; }

        public bool ValidateTaxa { get; set; }

        public bool RunXslTransform { get; set; }

        public bool ZoobankCloneJson { get; set; }

        public bool ZoobankCloneXml { get; set; }

        public bool ZoobankGenerateRegistrationXml { get; set; }
    }
}