namespace ProcessingTools.Data.Common.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;

    public class EntityDbContext : DbContext, IDbContext
    {
        public EntityDbContext(string connectionString)
            : base(connectionString)
        {
        }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
