namespace ProcessingTools.Tagger.Settings
{
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Processors.Contracts;
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
                    AppSettings.ReferencesTagTemplateXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ReferencesGetReferencesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ReferencesGetReferencesXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.CodesRemoveNonCodeNodesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.CodesRemoveNonCodeNodesXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.TaxonTreatmentFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatTaxonTreatmentsXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.TaxonTreatmentExtractMaterialsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.TaxonTreatmentExtractMaterialsXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ZooBankRegistrationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ZooBankRegistrationNlmXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatToNlmTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatSystemToNlmXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatToSystemTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatNlmToSystemXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.NlmInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.NlmInitialFormatXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.SystemInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.SystemInitialFormatXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ExternalLinksTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ExternalLinksXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.TextContentTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.TextContentXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.RemoveTaxonNamePartsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.RemoveTaxonNamePartsXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.ParseTreatmentMetaWithInternalInformationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ParseTreatmentMetaWithInternalInformationXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.GavinLaurensTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.GavinLaurensXslFileName);

            this.Bind<IXmlTransformer>().To<XQueryTransformer>().InSingletonScope()
                .Named(FactoryKeys.AbbreviationsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XQueyFileName,
                    AppSettings.AbbreviationsXQueryFileName);

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
