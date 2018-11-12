namespace ProcessingTools.NlmArchiveConsoleManager.Settings
{
    using System.Reflection;
    using Data.Mongo.Common;
    using Data.Mongo.Common.Contracts;
    using Data.Mongo.Documents;
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Extensions.Factory;
    using global::Ninject.Extensions.Interception.Infrastructure.Language;
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Ninject.Interceptors;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories;
    using ProcessingTools.NlmArchiveConsoleManager.Core;
    using ProcessingTools.Services.IO;

    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Assembly.GetExecutingAssembly())
                .SelectAllClasses()
                .BindDefaultInterface();

                b.FromAssembliesMatching(
                    $"{nameof(ProcessingTools)}.*.{FileConstants.DllFileExtension}")
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Contracts.IDocumentFactory>()
                .To<ProcessingTools.Common.Code.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<ProcessingTools.Services.Contracts.IO.IXmlReadService>()
                .To<ProcessingTools.Services.IO.XmlReadService>()
                .WhenInjectedInto<XmlFileContentDataService>();

            this.Bind<ProcessingTools.Services.Contracts.IO.IXmlWriteService>()
                .To<ProcessingTools.Services.IO.XmlWriteService>()
                .WhenInjectedInto<XmlFileContentDataService>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<ProcessingTools.Contracts.Serialization.IDeserializer>()
                .To<ProcessingTools.Common.Code.Serialization.DataContractJsonDeserializer>()
                .InSingletonScope();

            this.Bind<IProcessorFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IModelFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<ProcessingTools.Services.Contracts.Meta.IJournalMetaDataService>()
                .To<ProcessingTools.Services.Meta.JournalMetaDataServiceWithFiles>();

#if UseFileDirectory
            string journalMetaFilesDirectory = AppSettings.JournalsJsonFilesDirectoryName;

            this.Bind<ProcessingTools.Contracts.Services.Data.Meta.IJournalsMetaDataService>()
                .To<ProcessingTools.Services.Data.Services.Meta.JournalsMetaDataServiceWithFiles>()
                .WhenInjectedInto<Engine>()
                .WithConstructorArgument(
                    ParameterNames.JournalMetaFilesDirectoryName,
                    journalMetaFilesDirectory);

            this.Bind<ProcessingTools.Contracts.Services.Data.Meta.IJournalsMetaDataService>()
                .To<ProcessingTools.Services.Data.Services.Meta.JournalsMetaDataServiceWithFiles>()
                .WhenInjectedInto<HelpProvider>()
                .WithConstructorArgument(
                    ParameterNames.JournalMetaFilesDirectoryName,
                    journalMetaFilesDirectory);
#else
            this.Bind<ProcessingTools.Services.Contracts.Meta.IJournalsMetaDataService>()
                .To<ProcessingTools.Services.Meta.JournalsMetaDataServiceWithDatabase>()
                .WhenInjectedInto<Engine>();

            this.Bind<ProcessingTools.Services.Contracts.Meta.IJournalsMetaDataService>()
                .To<ProcessingTools.Services.Meta.JournalsMetaDataServiceWithDatabase>()
                .WhenInjectedInto<HelpProvider>();

            this.Bind<IJournalMetaDataAccessObject>()
                .To<MongoJournalMetaDataAccessObject>();

            string documentsMongoConnection = AppSettings.DocumentsMongoConnection;
            string documentsMongoDabaseName = AppSettings.DocumentsMongoDatabaseName;

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<MongoJournalMetaDataAccessObject>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    documentsMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    documentsMongoDabaseName);
#endif
        }
    }
}
