namespace ProcessingTools.Constants.Schema
{
    using System.Configuration;
    using ProcessingTools.Constants.Configuration;

    public static class DocTypeConstants
    {
        public const string TaxPubName = "article";
        public const string TaxPubPublicId = "-//TaxonX//DTD Taxonomic Treatment Publishing DTD v0 20100105//EN";
        public const string TaxPubSystemId = "tax-treatment-NS0.dtd";
        public static readonly string TaxPubDtdPath = ConfigurationManager.AppSettings[AppSettingsKeys.TaxPubDtdPath];
    }
}
