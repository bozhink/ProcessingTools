namespace ProcessingTools.Xml.Providers
{
    using System.Configuration;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class TextContentXslTransformProvider : XslTransformAbstractProvider, ITextContentXslTransformProvider
    {
        public TextContentXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.TextContentXslFileNameKey];
    }
}
