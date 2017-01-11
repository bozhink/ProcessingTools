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
        private const string AbbreviationsTransformerName = "AbbreviationsTransformer";
        private const string CodesRemoveNonCodeNodesTransformerName = "CodesRemoveNonCodeNodesTransformer";
        private const string ExternalLinksTransformerName = "ExternalLinksTransformer";
        private const string FormatToNlmTransformerName = "FormatToNlmTransformer";
        private const string FormatToSystemTransformerName = "FormatToSystemTransformer";
        private const string NlmInitialFormatTransformerName = "NlmInitialFormatTransformer";
        private const string ReferencesGetReferencesTransformerName = "ReferencesGetReferencesTransformer";
        private const string ReferencesTagTemplateTransformerName = "ReferencesTagTemplateTransformer";
        private const string SystemInitialFormatTransformerName = "SystemInitialFormatTransformer";
        private const string TaxonTreatmentExtractMaterialsTransformerName = "TaxonTreatmentExtractMaterialsTransformer";
        private const string TaxonTreatmentFormatTransformerName = "TaxonTreatmentFormatTransformer";
        private const string TextContentTransformerName = "TextContentTransformer";
        private const string ZooBankRegistrationTransformerName = "ZooBankRegistrationTransformer";
        private const string RemoveTaxonNamePartsTransformerName = "RemoveTaxonNamePartsTransformer";
        private const string ParseTreatmentMetaWithInternalInformationTransformerName = "ParseTreatmentMetaWithInternalInformationTransformer";

        public override void Load()
        {
            // Transformers
            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ReferencesTagTemplateTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesTagTemplateXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ReferencesGetReferencesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesGetReferencesXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(CodesRemoveNonCodeNodesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.CodesRemoveNonCodeNodesXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(TaxonTreatmentFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatTaxonTreatmentsXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(TaxonTreatmentExtractMaterialsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ZooBankRegistrationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ZooBankRegistrationNlmXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FormatToNlmTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatSystemToNlmXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FormatToSystemTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(NlmInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.NlmInitialFormatXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(SystemInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.SystemInitialFormatXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ExternalLinksTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ExternalLinksXslFileFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(TextContentTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.TextContentXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(RemoveTaxonNamePartsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.RemoveTaxonNamePartsXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(ParseTreatmentMetaWithInternalInformationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ParseTreatmentMetaWithInternalInformationXslFileName]);

            this.Bind<IXmlTransformer>().To<XQueryTransformer>().InSingletonScope()
                .Named(AbbreviationsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XQueyFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.AbbreviationsXQueryFileName]);

            // Factories
            this.Bind<ProcessingTools.Xml.Contracts.Factories.IXslTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Xml.Contracts.Factories.IXQueryTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

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

            this.Bind<ProcessingTools.Harvesters.Contracts.Factories.IExternalLinksTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Harvesters.Contracts.Factories.ITextContentTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Bio.Taxonomy.Processors.Contracts.Factories.IBioTaxonomyTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Harvesters.Contracts.Factories.IAbbreviationsTransformersFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
