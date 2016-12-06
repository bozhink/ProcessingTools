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
        private const string CodesRemoveNonCodeNodesTransformerName = "CodesRemoveNonCodeNodesTransformer";
        private const string FormatToNlmTransformerName = "FormatToNlmTransformer";
        private const string FormatToSystemTransformerName = "FormatToSystemTransformer";
        private const string NlmInitialFormatTransformerName = "NlmInitialFormatTransformer";
        private const string ReferencesGetReferencesTransformerName = "ReferencesGetReferencesTransformer";
        private const string ReferencesTagTemplateTransformerName = "ReferencesTagTemplateTransformer";
        private const string SystemInitialFormatTransformerName = "SystemInitialFormatTransformer";
        private const string TaxonTreatmentExtractMaterialsTransformerName = "TaxonTreatmentExtractMaterialsTransformer";
        private const string TaxonTreatmentFormatTransformerName = "TaxonTreatmentFormatTransformer";
        private const string ZooBankRegistrationTransformerName = "ZooBankRegistrationTransformer";

        public override void Load()
        {
            // Transformers
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

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(CodesRemoveNonCodeNodesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.CodesRemoveNonCodeNodesXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(TaxonTreatmentFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatTaxonTreatmentsXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(TaxonTreatmentExtractMaterialsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ZooBankRegistrationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ZooBankRegistrationNlmXslFileNameKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FormatToNlmTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatSystemToNlmXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FormatToSystemTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(NlmInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.NlmInitialFormatXslPathKey]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(SystemInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.SystemInitialFormatXslPathKey]);

            // Factories
            this.Bind<ProcessingTools.Processors.Contracts.Factories.IReferencesTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Processors.Contracts.Factories.Bio.ICodesTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Processors.Contracts.Factories.Bio.ITaxonTreatmentsTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Processors.Contracts.Factories.IRegistrationTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Layout.Processors.Contracts.Factories.IFormatTransformersFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
