namespace ProcessingTools.Documents.Services.Data.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Abstractions;
    using ProcessingTools.Xml.Contracts.Cache;

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
