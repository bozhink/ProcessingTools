namespace ProcessingTools.Documents.Services.Data.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Abstracts;
    using ProcessingTools.Xml.Contracts.Cache;

    // TODO: move to Xml
    public class FormatHtmlToXmlXslTransformProvider : AbstractXslTransformProvider, IFormatHtmlToXmlXslTransformProvider
    {
        public FormatHtmlToXmlXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatHtmlToXmlXslFilePathKey];
    }
}
