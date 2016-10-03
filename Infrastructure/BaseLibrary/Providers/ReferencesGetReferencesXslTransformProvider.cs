namespace ProcessingTools.BaseLibrary.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class ReferencesGetReferencesXslTransformProvider : XslTransformAbstractProvider, IReferencesGetReferencesXslTransformProvider
    {
        public ReferencesGetReferencesXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesGetReferencesXslPathKey];
    }
}
