namespace ProcessingTools.DataResources.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class DataResourcesDbContext : DbContext, IDataResourcesDbContext
    {
        public DataResourcesDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Abbreviation> Abbreviations { get; set; }

        public IDbSet<ContentType> ContentTypes { get; set; }

        public IDbSet<Institution> Institutions { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<SourceId> Sources { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
