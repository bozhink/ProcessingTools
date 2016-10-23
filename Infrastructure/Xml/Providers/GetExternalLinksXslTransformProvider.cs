namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Abstracts;
    using Contracts.Cache;
    using Contracts.Providers;

    using ProcessingTools.Constants.Configuration;

    public class GetExternalLinksXslTransformProvider : AbstractXslTransformProvider, IGetExternalLinksXslTransformProvider
    {
        public GetExternalLinksXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.ExternalLinksXslFilePathKey];
    }
}
