namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Common.Constants.Configuration;

    public class BioTaxonomyDbContextFactory : IBioTaxonomyDbContextFactory
    {
        private string connectionString;

        public BioTaxonomyDbContextFactory()
        {
            this.ConnectionString = ConnectionStrings.BioTaxonomyDatabaseConnection;
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
                    throw new ArgumentNullException(nameof(value));
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
