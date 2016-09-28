namespace ProcessingTools.BaseLibrary.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class TaxonTreatmentExtractMaterialsXslTransformProvider : XslTransformAbstractProvider, ITaxonTreatmentExtractMaterialsXslTransformProvider
    {
        public TaxonTreatmentExtractMaterialsXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslPathKey];
    }
}
