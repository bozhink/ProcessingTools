namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Contracts;
    using Contracts.Providers;
    using Abstracts;

    using ProcessingTools.Constants.Configuration;

    public class FormatNlmToSystemXslTransformProvider : AbstractXslTransformProvider, IFormatNlmToSystemXslTransformProvider
    {
        public FormatNlmToSystemXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslPathKey];
    }
}
