﻿// <copyright file="NinjectBindings.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Settings
{
    using System.Reflection;
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Extensions.Factory;
    using global::Ninject.Extensions.Interception.Infrastructure.Language;
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts.DataAccess.Documents;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.IO;
    using ProcessingTools.Contracts.Services.Meta;
    using ProcessingTools.Contracts.Services.Serialization;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.DataAccess.Mongo.Documents;
    using ProcessingTools.Ninject.Interceptors;
    using ProcessingTools.NlmArchiveConsoleManager.Contracts.Factories;
    using ProcessingTools.NlmArchiveConsoleManager.Core;
    using ProcessingTools.Services.IO;
    using ProcessingTools.Services.Serialization;

    /// <summary>
    /// Ninject bindings.
    /// </summary>
    public class NinjectBindings : NinjectModule
    {
        /// <inheritdoc/>
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

            this.Bind<IDocumentFactory>()
                .To<ProcessingTools.Services.TaxPubDocumentFactory>()
                .InSingletonScope();

            this.Bind<IXmlReadService>()
                .To<ProcessingTools.Services.IO.XmlReadService>()
                .WhenInjectedInto<XmlFileContentDataService>();

            this.Bind<IXmlWriteService>()
                .To<ProcessingTools.Services.IO.XmlWriteService>()
                .WhenInjectedInto<XmlFileContentDataService>()
                .Intercept()
                .With<FileExistsRaiseWarningInterceptor>();

            this.Bind<IDeserializer>()
                .To<DataContractJsonDeserializer>()
                .InSingletonScope();

            this.Bind<IProcessorFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IModelFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IJournalMetaDataService>()
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
            this.Bind<IJournalsMetaDataService>()
                .To<ProcessingTools.Services.Meta.JournalsMetaDataServiceWithDatabase>()
                .WhenInjectedInto<Engine>();

            this.Bind<IJournalsMetaDataService>()
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
