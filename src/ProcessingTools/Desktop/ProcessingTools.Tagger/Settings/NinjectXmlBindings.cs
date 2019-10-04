﻿namespace ProcessingTools.Tagger.Settings
{
    using global::Ninject.Extensions.Factory;
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.Services.Abbreviations;
    using ProcessingTools.Contracts.Services.Bio.Codes;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.ZooBank;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Contracts.Services.ExternalLinks;
    using ProcessingTools.Contracts.Services.Layout;
    using ProcessingTools.Contracts.Services.Special;
    using ProcessingTools.Contracts.Services.Xml;
    using ProcessingTools.Services.Xml;

    public class NinjectXmlBindings : NinjectModule
    {
        public override void Load()
        {
            // Transformers
            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.ReferencesTagTemplateTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ReferencesTagTemplateXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.ReferencesGetReferencesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ReferencesGetReferencesXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.CodesRemoveNonCodeNodesTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.CodesRemoveNonCodeNodesXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.TaxonTreatmentFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatTaxonTreatmentsXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.TaxonTreatmentExtractMaterialsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.TaxonTreatmentExtractMaterialsXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.ZooBankRegistrationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ZooBankRegistrationNlmXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.FormatToNlmTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatSystemToNlmXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.FormatToSystemTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.FormatNlmToSystemXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.NlmInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.NlmInitialFormatXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.SystemInitialFormatTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.SystemInitialFormatXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.ExternalLinksTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ExternalLinksXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.TextContentTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.TextContentXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.RemoveTaxonNamePartsTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.RemoveTaxonNamePartsXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.ParseTreatmentMetaWithInternalInformationTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.ParseTreatmentMetaWithInternalInformationXslFileName);

            this.Bind<IXmlTransformer>().To<XslTransformerFromFile>().InSingletonScope()
                .Named(FactoryKeys.GavinLaurensTransformerName)
                .WithConstructorArgument(
                    ParameterNames.XslFileName,
                    AppSettings.GavinLaurensXslFileName);

            ////this.Bind<IXmlTransformer>().To<XQueryTransformer>().InSingletonScope()
            ////    .Named(FactoryKeys.AbbreviationsTransformerName)
            ////    .WithConstructorArgument(
            ////        ParameterNames.XQueyFileName,
            ////        AppSettings.AbbreviationsXQueryFileName);

            // Factories
            this.Bind<IXslTransformerFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IXQueryTransformerFactory>()
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
