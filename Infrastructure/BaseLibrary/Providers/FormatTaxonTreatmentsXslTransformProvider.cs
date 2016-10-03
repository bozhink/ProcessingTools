namespace ProcessingTools.BaseLibrary.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class FormatTaxonTreatmentsXslTransformProvider : XslTransformAbstractProvider, IFormatTaxonTreatmentsXslTransformProvider
    {
        public FormatTaxonTreatmentsXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatTaxonTreatmentsXslPathKey];
    }
}
