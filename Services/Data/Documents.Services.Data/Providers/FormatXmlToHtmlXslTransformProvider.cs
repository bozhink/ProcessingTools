namespace ProcessingTools.Documents.Services.Data.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts.Cache;
    using ProcessingTools.Xml.Abstracts;

    // TODO: move to Xml
    public class FormatXmlToHtmlXslTransformProvider : AbstractXslTransformProvider, IFormatXmlToHtmlXslTransformProvider
    {
        public FormatXmlToHtmlXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatXmlToHtmlXslFilePathKey];
    }
}
