namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Abstractions;
    using Contracts.Cache;
    using Contracts.Providers;

    using ProcessingTools.Constants.Configuration;

    public class TaxonTreatmentExtractMaterialsXslTransformProvider : AbstractXslTransformProvider, ITaxonTreatmentExtractMaterialsXslTransformProvider
    {
        public TaxonTreatmentExtractMaterialsXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslPathKey];
    }
}
