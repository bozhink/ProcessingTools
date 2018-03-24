namespace ProcessingTools.Web.Documents.Settings
{
    using Ninject.Extensions.Factory;
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Harvesters.Contracts.Abbreviations;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Harvesters.Contracts.ExternalLinks;
    using ProcessingTools.Processors.Contracts.Bio.Codes;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Processors.Contracts.Special;
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

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatXmlToHtmlTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatXmlToHtmlXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformer>().InSingletonScope()
                .Named(FactoryKeys.FormatHtmlToXmlTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatHtmlToXmlXslFileName);

            // Factories
            this.Bind<IReferencesTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ICodesTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ITaxonTreatmentsTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IZooBankTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IFormatTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Services.Contracts.Documents.IDocumentsFormatTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IXslTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IXQueryTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IExternalLinksTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ITextContentTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IBioTaxonomyTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IAbbreviationsTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ISpecialTransformerFactory>()
                .ToFactory()
                .InSingletonScope();
        }
    }
}
