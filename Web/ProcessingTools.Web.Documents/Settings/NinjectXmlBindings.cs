namespace ProcessingTools.Web.Documents.Settings
{
    using System.Configuration;
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Transformers;

    public class NinjectXmlBindings : NinjectModule
    {
        public override void Load()
        {
            // Transformers
            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ReferencesTagTemplateTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesTagTemplateXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ReferencesGetReferencesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ReferencesGetReferencesXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.CodesRemoveNonCodeNodesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.CodesRemoveNonCodeNodesXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.TaxonTreatmentFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatTaxonTreatmentsXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.TaxonTreatmentExtractMaterialsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.TaxonTreatmentExtractMaterialsXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ZooBankRegistrationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ZooBankRegistrationNlmXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatToNlmTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatSystemToNlmXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatToSystemTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatNlmToSystemXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.NlmInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.NlmInitialFormatXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.SystemInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.SystemInitialFormatXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ExternalLinksTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ExternalLinksXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.TextContentTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.TextContentXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.RemoveTaxonNamePartsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.RemoveTaxonNamePartsXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ParseTreatmentMetaWithInternalInformationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.ParseTreatmentMetaWithInternalInformationXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.GavinLaurensTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.GavinLaurensXslFileName]);

            this.Bind<IXmlTransformer>().To<XQueryTransformer>().InSingletonScope()
                .Named(FactoryKeys.AbbreviationsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XQueyFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.AbbreviationsXQueryFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatXmlToHtmlTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatXmlToHtmlXslFileName]);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatHtmlToXmlTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    ConfigurationManager.AppSettings[AppSettingsKeys.FormatHtmlToXmlXslFileName]);

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

            this.Bind<ProcessingTools.Documents.Services.Data.Contracts.Factories.IDocumentsFormatTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Xml.Contracts.Factories.IXslTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Xml.Contracts.Factories.IXQueryTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Harvesters.Contracts.Factories.IExternalLinksTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Harvesters.Contracts.Factories.ITextContentTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Processors.Contracts.Factories.Bio.IBioTaxonomyTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Harvesters.Contracts.Factories.IAbbreviationsTransformersFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Special.Processors.Contracts.Factories.ISpecialTransformersFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
