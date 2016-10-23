namespace ProcessingTools.Tagger.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Abstracts;

    public class ZoobankNlmXslTransformProvider : AbstractXslTransformProvider, IZoobankNlmXslTransformProvider
    {
        public ZoobankNlmXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.ZoobankNlmXslPathKey];
    }
}
