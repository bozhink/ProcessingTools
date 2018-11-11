﻿namespace ProcessingTools.DbSeeder.Settings
{
    using System;
    using System.Reflection;
    using global::Ninject;
    using global::Ninject.Extensions.Conventions;
    using global::Ninject.Extensions.Interception.Infrastructure.Language;
    using global::Ninject.Modules;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.DbSeeder.Contracts.Providers;
    using ProcessingTools.DbSeeder.Contracts.Seeders;
    using ProcessingTools.DbSeeder.Core;
    using ProcessingTools.DbSeeder.Providers;
    using ProcessingTools.Ninject.Interceptors;
    using ProcessingTools.Processors.Contracts;

    /// <summary>
    /// NinjectModule to bind seeder objects.
    /// </summary>
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind(b =>
            {
                b.From(Assembly.GetExecutingAssembly())
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(typeof(ProcessingTools.Data.Contracts.IGenericRepositoryProvider<>))
                .To(typeof(ProcessingTools.Common.Code.Data.Repositories.RepositoryProviderAsync<>));

            this.Bind<ITypesProvider>()
                .To<SeederTypesProvider>()
                .InSingletonScope();

            this.Bind<Func<Type, IDbSeeder>>()
                .ToMethod(context => t => (IDbSeeder)context.Kernel.Get(t))
                .InSingletonScope();

            this.Bind<IHelpProvider>()
                .To<HelpProvider>()
                .InSingletonScope();

            this.Bind<ICommandNamesProvider>()
                .To<CommandNamesProvider>()
                .InSingletonScope();

            this.Bind<ICommandRunner>()
                .To<SeedCommandRunner>()
                .Intercept()
                .With<CommandRunnerTimeLoggingInterceptor>();

            this.Bind<ISandbox>()
                .To<Sandbox>();

            // Geo.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Geo.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();

                b.From(ProcessingTools.Geo.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Mediatypes.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Mediatypes.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<ProcessingTools.Mediatypes.Data.Mongo.Contracts.IMediatypesMongoDatabaseInitializer>()
                .To<ProcessingTools.Mediatypes.Data.Mongo.MediatypesMongoDatabaseInitializer>()
                .InSingletonScope();

            this.Bind(b =>
            {
                b.From(ProcessingTools.Mediatypes.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind<IMongoDatabaseProvider>()
                .To<MongoDatabaseProvider>()
                .WhenInjectedInto<ProcessingTools.Mediatypes.Data.Seed.Seeders.MediatypesMongoDatabaseSeeder>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.MediatypesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.MediatypesMongoDatabaseName);

            this.Bind<IMongoDatabaseProvider>()
               .To<MongoDatabaseProvider>()
               .WhenInjectedInto<ProcessingTools.Mediatypes.Data.Mongo.MediatypesMongoDatabaseInitializer>()
               .InSingletonScope()
               .WithConstructorArgument(
                   ParameterNames.ConnectionString,
                   AppSettings.MediatypesMongoConnection)
               .WithConstructorArgument(
                   ParameterNames.DatabaseName,
                   AppSettings.MediatypesMongoDatabaseName);

            // DataResources
            this.Bind(b =>
            {
                b.From(ProcessingTools.DataResources.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.DataResources.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Biorepositories.Data
            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepository<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiorepositoriesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiorepositoriesMongoDatabaseName);

            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto(typeof(ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.BiorepositoriesRepositoryProvider<>))
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiorepositoriesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiorepositoriesMongoDatabaseName);

            this.Bind<ProcessingTools.Data.Common.Mongo.Contracts.IMongoDatabaseProvider>()
                .To<ProcessingTools.Data.Common.Mongo.MongoDatabaseProvider>()
                .WhenInjectedInto<ProcessingTools.Bio.Biorepositories.Data.Seed.Seeders.BiorepositoriesDataSeeder>()
                .InSingletonScope()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    AppSettings.BiorepositoriesMongoConnection)
                .WithConstructorArgument(
                    ParameterNames.DatabaseName,
                    AppSettings.BiorepositoriesMongoDatabaseName);

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Data.Mongo.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Biorepositories.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Environments.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Environments.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Environments.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            // Bio.Taxonomy.Data
            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Taxonomy.Data.Entity.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });

            this.Bind(b =>
            {
                b.From(ProcessingTools.Bio.Taxonomy.Data.Seed.Assembly.Assembly.GetType().Assembly)
                    .SelectAllClasses()
                    .BindDefaultInterface();
            });
        }
    }
}
