namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Contracts.Cache;
    using Contracts.Providers;
    using Abstracts;

    using ProcessingTools.Constants.Configuration;

    public class FormatSystemToNlmXslTransformProvider : AbstractXslTransformProvider, IFormatSystemToNlmXslTransformProvider
    {
        public FormatSystemToNlmXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatSystemToNlmXslPathKey];
    }
}
