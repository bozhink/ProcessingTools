namespace ProcessingTools.MainProgram
{
    public class ProgramSettings
    {
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
            this.FormatInit = false;
            this.FormatTreat = false;
            this.ParseBySection = false;
            this.ParseCoords = false;
            this.ParseReferences = false;
            this.ParseTreatmentMetaWithAphia = false;
            this.ParseTreatmentMetaWithCol = false;
            this.ParseTreatmentMetaWithGbif = false;
            this.QuentinSpecificActions = false;
            this.QueryReplace = false;
            this.SplitHigherAboveGenus = false;
            this.SplitHigherBySuffix = false;
            this.SplitHigherWithAphia = false;
            this.SplitHigherWithCoL = false;
            this.SplitHigherWithGbif = false;
            this.TagAbbrev = false;
            this.TagCodes = false;
            this.TagCoords = false;
            this.TagDates = false;
            this.TagDoi = false;
            this.TagEnvironments = false;
            this.TagEnvo = false;
            this.TagFigTab = false;
            this.TagQuantities = false;
            this.TagReferences = false;
            this.TagTableFn = false;
            this.TagWWW = false;
            this.TaxaA = false;
            this.TaxaB = false;
            this.TaxaC = false;
            this.TaxaD = false;
            this.TaxaE = false;
            this.TestFlag = false;
            this.UntagSplit = false;
            this.ValidateTaxa = false;
            this.ZoobankCloneJson = false;
            this.ZoobankCloneXml = false;
            this.ZoobankGenerateRegistrationXml = false;
        }

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

        public bool FormatInit { get; set; }

        public bool FormatTreat { get; set; }

        public string HigherStructrureXpath { get; set; }

        public string InputFileName { get; set; }

        public string OutputFileName { get; set; }

        public bool ParseBySection { get; set; }

        public bool ParseCoords { get; set; }

        public bool ParseReferences { get; set; }

        public bool ParseTreatmentMetaWithAphia { get; set; }

        public bool ParseTreatmentMetaWithCol { get; set; }

        public bool ParseTreatmentMetaWithGbif { get; set; }

        public bool QuentinSpecificActions { get; set; }

        public string QueryFileName { get; set; }

        public bool QueryReplace { get; set; }

        public bool SplitHigherAboveGenus { get; set; }

        public bool SplitHigherBySuffix { get; set; }

        public bool SplitHigherWithAphia { get; set; }

        public bool SplitHigherWithCoL { get; set; }

        public bool SplitHigherWithGbif { get; set; }

        public bool TagAbbrev { get; set; }

        public bool TagCodes { get; set; }

        public bool TagCoords { get; set; }

        public bool TagDates { get; set; }

        public bool TagDoi { get; set; }

        public bool TagEnvironments { get; set; }

        public bool TagEnvo { get; set; }

        public bool TagFigTab { get; set; }

        public bool TagQuantities { get; set; }

        public bool TagReferences { get; set; }

        public bool TagTableFn { get; set; }

        public bool TagWWW { get; set; }

        public bool TaxaA { get; set; }

        public bool TaxaB { get; set; }

        public bool TaxaC { get; set; }

        public bool TaxaD { get; set; }

        public bool TaxaE { get; set; }

        public bool TestFlag { get; set; }

        public bool UntagSplit { get; set; }

        public bool ValidateTaxa { get; set; }

        public bool ZoobankCloneJson { get; set; }

        public bool ZoobankCloneXml { get; set; }

        public bool ZoobankGenerateRegistrationXml { get; set; }
    }
}