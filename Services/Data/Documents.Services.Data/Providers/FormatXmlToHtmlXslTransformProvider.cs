namespace ProcessingTools.Documents.Services.Data.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class FormatXmlToHtmlXslTransformProvider : XslTransformAbstractProvider, IFormatXmlToHtmlXslTransformProvider
    {
        public FormatXmlToHtmlXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatXmlToHtmlXslFilePathKey];
    }
}
