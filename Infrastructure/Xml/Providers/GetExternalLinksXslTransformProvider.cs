namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Contracts;
    using Contracts.Providers;
    using Abstracts;

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
