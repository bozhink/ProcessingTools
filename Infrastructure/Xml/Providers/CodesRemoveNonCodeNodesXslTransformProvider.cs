namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Contracts.Cache;
    using Contracts.Providers;
    using Abstracts;

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
