namespace ProcessingTools.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Models;

    public class DataDbContext : DbContext
    {
        public DataDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Institution> Institutions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}