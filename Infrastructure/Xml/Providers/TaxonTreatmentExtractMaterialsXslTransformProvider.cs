namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Contracts;
    using Contracts.Providers;
    using Factories;

    using ProcessingTools.Constants.Configuration;

    public class TaxonTreatmentExtractMaterialsXslTransformProvider : XslTransformAbstractProvider, ITaxonTreatmentExtractMaterialsXslTransformProvider
    {
        public TaxonTreatmentExtractMaterialsXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslPathKey];
    }
}
