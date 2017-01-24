namespace ProcessingTools.Tagger.Settings
{
    using Ninject.Modules;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;
    using ProcessingTools.Mediatypes.Data.Entity;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Entity.Factories;
    using ProcessingTools.Mediatypes.Data.Entity.Providers;
    using ProcessingTools.Mediatypes.Data.Entity.Repositories;
    using ProcessingTools.Services.Data.Contracts.Mediatypes;
    using ProcessingTools.Services.Data.Services.Mediatypes;

    public class NinjectMediatypesBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMediatypesDbContext>()
                .To<MediatypesDbContext>()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStringsKeys.MediatypesDatabaseConnection);

            this.Bind<IMediatypesDbContextFactory>()
                .To<MediatypesDbContextFactory>()
                .InSingletonScope();

            this.Bind<IMediatypesDbContextProvider>()
                .To<MediatypesDbContextProvider>()
                .InSingletonScope();

            this.Bind<ISearchableMediatypesRepository>()
                .To<MediatypesRepository>();

            this.Bind<IMediatypesRepository>()
                .To<MediatypesRepository>();

            this.Bind<IMediatypeStringResolver>()
                .To<MediatypeStringResolverWithWindowsRegistry>()
                .InSingletonScope();

            this.Bind<IMediatypesResolver>()
                .To<MediatypesResolverWithMediatypeStringResolver>();
        }
    }
}
