namespace ProcessingTools.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Contracts;
    using Models;

    public class DataDbContext : DbContext, IDataDbContext
    {
        public DataDbContext()
            : base("DataDbContext")
        {
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Institution> Institutions { get; set; }

        public static DataDbContext Create()
        {
            return new DataDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}