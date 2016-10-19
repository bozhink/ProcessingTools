namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using Contracts;
    using Contracts.Providers;
    using Factories;

    using ProcessingTools.Constants.Configuration;

    public class TextContentXslTransformProvider : XslTransformAbstractProvider, ITextContentXslTransformProvider
    {
        public TextContentXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.TextContentXslFileNameKey];
    }
}
