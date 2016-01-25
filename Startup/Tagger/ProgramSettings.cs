namespace ProcessingTools.MainProgram
{
    using System;
    using Configurator;
    using Bio.Taxonomy.Services.Data;

    public class ProgramSettings
    {
        private Lazy<TaxonomicListDataService> blackList;
        private Lazy<TaxonomicListDataService> whiteList;

        public ProgramSettings()
        {
            this.Config = null;
            this.InputFileName = null;
            this.OutputFileName = null;
            this.QueryFileName = null;

            this.HigherStructrureXpath = "//article";

            this.ExtractHigherTaxa = false;
            this.ExtractLowerTaxa = false;
            this.ExtractTaxa = false;
            this.Flag1 = false;
            this.Flag2 = false;
            this.Flag3 = false;
            this.Flag4 = false;
            this.Flag5 = false;
            this.Flag6 = false;
            this.Flag7 = false;
            this.Flag8 = false;
            this.Flora = false;
            this.InitialFormat = false;
            this.FormatTreat = false;
            this.ParseBySection = false;
            this.ParseCoordinates = false;
            this.ParseReferences = false;
            this.ParseTreatmentMetaWithAphia = false;
            this.ParseTreatmentMetaWithCol = false;
            this.ParseTreatmentMetaWithGbif = false;
            this.QuentinSpecificActions = false;
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
            this.TagDates = false;
            this.TagDoi = false;
            this.TagEnvironmentTerms = false;
            this.TagEnvironmentTermsWithExtract = false;
            this.TagFloats = false;
            this.TagMorphologicalEpithets = false;
            this.TagGeoNames = false;
            this.TagGeoEpithets = false;
            this.TagInstitutions = false;
            this.TagProducts = false;
            this.TagQuantities = false;
            this.TagReferences = false;
            this.TagTableFn = false;
            this.TagWebLinks = false;
            this.TagLowerTaxa = false;
            this.TagHigherTaxa = false;
            this.ParseLowerTaxa = false;
            this.ParseHigherTaxa = false;
            this.ExpandLowerTaxa = false;
            this.TestFlag = false;
            this.UntagSplit = false;
            this.ValidateTaxa = false;
            this.RunXslTransform = false;
            this.ZoobankCloneJson = false;
            this.ZoobankCloneXml = false;
            this.ZoobankGenerateRegistrationXml = false;

            this.blackList = new Lazy<TaxonomicListDataService>(() => new TaxonomicListDataService(this.Config.BlackListXmlFilePath));
            this.whiteList = new Lazy<TaxonomicListDataService>(() => new TaxonomicListDataService(this.Config.WhiteListXmlFilePath));
        }

        public TaxonomicListDataService BlackList => this.blackList.Value;

        public TaxonomicListDataService WhiteList => this.whiteList.Value;

        public Config Config { get; set; }

        public bool ExtractHigherTaxa { get; set; }

        public bool ExtractLowerTaxa { get; set; }

        public bool ExtractTaxa { get; set; }

        public bool Flag1 { get; set; }

        public bool Flag2 { get; set; }

        public bool Flag3 { get; set; }

        public bool Flag4 { get; set; }

        public bool Flag5 { get; set; }

        public bool Flag6 { get; set; }

        public bool Flag7 { get; set; }

        public bool Flag8 { get; set; }

        public bool Flora { get; set; }

        public bool InitialFormat { get; set; }

        public bool FormatTreat { get; set; }

        public string HigherStructrureXpath { get; set; }

        public string InputFileName { get; set; }

        public string OutputFileName { get; set; }

        public bool ParseBySection { get; set; }

        public bool ParseCoordinates { get; set; }

        public bool ParseReferences { get; set; }

        public bool ParseTreatmentMetaWithAphia { get; set; }

        public bool ParseTreatmentMetaWithCol { get; set; }

        public bool ParseTreatmentMetaWithGbif { get; set; }

        public bool QuentinSpecificActions { get; set; }

        public string QueryFileName { get; set; }

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

        public bool TagDates { get; set; }

        public bool TagDoi { get; set; }

        public bool TagEnvironmentTerms { get; set; }

        public bool TagEnvironmentTermsWithExtract { get; set; }

        public bool TagFloats { get; set; }

        public bool TagMorphologicalEpithets { get; set; }

        public bool TagGeoNames { get; set; }

        public bool TagGeoEpithets { get; set; }

        public bool TagInstitutions { get; set; }

        public bool TagProducts { get; set; }

        public bool TagQuantities { get; set; }

        public bool TagReferences { get; set; }

        public bool TagTableFn { get; set; }

        public bool TagWebLinks { get; set; }

        public bool TagLowerTaxa { get; set; }

        public bool TagHigherTaxa { get; set; }

        public bool ParseLowerTaxa { get; set; }

        public bool ParseHigherTaxa { get; set; }

        public bool ExpandLowerTaxa { get; set; }

        public bool TestFlag { get; set; }

        public bool UntagSplit { get; set; }

        public bool ValidateTaxa { get; set; }

        public bool RunXslTransform { get; set; }

        public bool ZoobankCloneJson { get; set; }

        public bool ZoobankCloneXml { get; set; }

        public bool ZoobankGenerateRegistrationXml { get; set; }
    }
}