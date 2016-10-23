namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Abstracts;
    using Contracts.Cache;
    using Contracts.Providers;

    using ProcessingTools.Constants.Configuration;

    public class CodesRemoveNonCodeNodesXslTransformProvider : AbstractXslTransformProvider, ICodesRemoveNonCodeNodesXslTransformProvider
    {
        public CodesRemoveNonCodeNodesXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.CodesRemoveNonCodeNodesXslPathKey];
    }
}
