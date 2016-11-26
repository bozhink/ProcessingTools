namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class BioTaxonomyDbContextFactory : IBioTaxonomyDbContextFactory
    {
        private string connectionString;

        public BioTaxonomyDbContextFactory()
        {
            this.ConnectionString = ConnectionStringsKeys.BioTaxonomyDatabaseConnectionKey;
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(this.ConnectionString));
                }

                this.connectionString = value;
            }
        }

        public BioTaxonomyDbContext Create()
        {
            return new BioTaxonomyDbContext(this.ConnectionString);
        }
    }
}
