namespace ProcessingTools.Layout.Processors.Providers
{
    using System.Configuration;

    using Contracts;

    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Factories;

    public class SystemInitialFormatXslTransformProvider : XslTransformAbstractProvider, ISystemInitialFormatXslTransformProvider
    {
        public SystemInitialFormatXslTransformProvider(IXslTransformCache cache)
            : base(cache)
        {
        }

        protected override string XslFileName => ConfigurationManager.AppSettings[AppSettingsKeys.SystemInitialFormatXslPathKey];
    }
}
