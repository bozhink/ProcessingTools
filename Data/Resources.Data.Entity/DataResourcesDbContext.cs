using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProcessingTools.Resources.Data.Entity
{
    public class DataResourcesDbContext : DbContext
    {
        public DataResourcesDbContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
