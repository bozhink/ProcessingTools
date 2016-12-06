namespace ProcessingTools.Tagger.Settings
{
    using System.Configuration;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Transformers;

    public class NinjectXmlBindings : NinjectModule
    {
        private const string ReferencesTagTemplateTransformerName = "ReferencesTagTemplateTransformer";
        private const string ReferencesGetReferencesTransformerName = "ReferencesGetReferencesTransformer";

        public override void Load()
        {
            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ReferencesTagTemplateTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesTagTemplateXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ReferencesGetReferencesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesGetReferencesXslPathKey]);

            this.Bind<ProcessingTools.Processors.Contracts.Factories.IReferencesTransformersFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
