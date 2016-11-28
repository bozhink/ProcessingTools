namespace ProcessingTools.Tagger.Settings
{
    using System.Configuration;
    using Ninject.Extensions.Conventions;
    using Ninject.Modules;
    using ProcessingTools.Mediatypes.Data.Entity;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Entity.Factories;
    using ProcessingTools.Mediatypes.Data.Entity.Providers;
    using ProcessingTools.Mediatypes.Data.Entity.Repositories;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Mediatypes.Data.Common.Contracts.Repositories;


    public class NinjectMediatypesDataBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMediatypesDbContext>()
                .To<MediatypesDbContext>()
                .WithConstructorArgument(
                    ParameterNames.ConnectionString,
                    ConnectionStringsKeys.MediatypesDatabaseConnectionKey);

            this.Bind<IMediatypesDbContextFactory>()
                .To<MediatypesDbContextFactory>()
                .InSingletonScope();

            this.Bind<IMediatypesDbContextProvider>()
                .To<MediatypesDbContextProvider>()
                .InSingletonScope();

            this.Bind<IMediatypesRepository>()
                .To<MediatypesRepository>();
        }
    }
}
