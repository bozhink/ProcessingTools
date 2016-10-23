namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Abstracts;
    using Contracts.Cache;
    using Contracts.Providers;

    using ProcessingTools.Constants.Configuration;

    public class GetAbbreviationsXQueryTransformProvider : AbstractXQueryTransformProvider, IGetAbbreviationsXQueryTransformProvider
    {
        public GetAbbreviationsXQueryTransformProvider(IXQueryTransformCache cache)
            : base(cache)
        {
        }

        protected override string XQueryFileName => ConfigurationManager.AppSettings[AppSettingsKeys.AbbreviationsXQueryFilePathKey];
    }
}
