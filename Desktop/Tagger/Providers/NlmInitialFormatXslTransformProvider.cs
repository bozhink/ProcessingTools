namespace ProcessingTools.Tagger.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class NlmInitialFormatXslTransformProvider : XslTransformAbstractProvider, INlmInitialFormatXslTransformProvider
    {
        public NlmInitialFormatXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.NlmInitialFormatXslPathKey];
    }
}
