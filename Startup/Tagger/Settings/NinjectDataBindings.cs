namespace ProcessingTools.MainProgram.Settings
{
    using Ninject.Modules;

    /// <summary>
    /// NinjectModule to bind database objects.
    /// </summary>
    public class NinjectDataBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<Data.Contracts.IDataDbContext>()
                .To<Data.DataDbContext>();
            this.Bind(typeof(Data.Repositories.Contracts.IDataRepository<>))
                .To(typeof(Data.Repositories.DataRepository<>));

            this.Bind<Bio.Data.Contracts.IBioDbContext>()
                .To<Bio.Data.BioDbContext>();
            this.Bind(typeof(Bio.Data.Repositories.Contracts.IBioDataRepository<>))
                .To(typeof(Bio.Data.Repositories.BioDataRepository<>));

            this.Bind<Bio.Biorepositories.Data.Contracts.IBiorepositoriesDbContext>()
                .To<Bio.Biorepositories.Data.BiorepositoriesDbContext>();
            this.Bind(typeof(Bio.Biorepositories.Data.Repositories.Contracts.IBiorepositoriesRepository<>))
                .To(typeof(Bio.Biorepositories.Data.Repositories.BiorepositoriesRepository<>));
            this.Bind<Bio.Biorepositories.Data.Contracts.IBiorepositoriesDbFirstDbContext>()
                .To<Bio.Biorepositories.Data.BiorepositoriesDbFirstDbContext>();
            this.Bind(typeof(Bio.Biorepositories.Data.Repositories.Contracts.IBiorepositoriesDbFirstGenericRepository<>))
                .To(typeof(Bio.Biorepositories.Data.Repositories.BiorepositoriesDbFirstGenericRepository<>));

            this.Bind<Bio.Environments.Data.Contracts.IBioEnvironmentsDbContext>()
                .To<Bio.Environments.Data.BioEnvironmentsDbContext>();
            this.Bind(typeof(Bio.Environments.Data.Repositories.Contracts.IBioEnvironmentsRepository<>))
                .To(typeof(Bio.Environments.Data.Repositories.BioEnvironmentsRepository<>));

            this.Bind<Geo.Data.Contracts.IGeoDbContext>()
                .To<Geo.Data.GeoDbContext>();
            this.Bind(typeof(Geo.Data.Repositories.Contracts.IGeoDataRepository<>))
                .To(typeof(Geo.Data.Repositories.GeoDataRepository<>));

            this.Bind<MediaType.Data.Contracts.IMediaTypesDbContext>()
                .To<MediaType.Data.MediaTypesDbContext>();
            this.Bind(typeof(MediaType.Data.Repositories.Contracts.IMediaTypesRepository<>))
                .To(typeof(MediaType.Data.Repositories.MediaTypesRepository<>));
        }
    }
}