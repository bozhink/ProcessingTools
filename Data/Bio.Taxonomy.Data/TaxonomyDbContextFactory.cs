namespace ProcessingTools.Bio.Taxonomy.Data
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    public class TaxonomyDbContextFactory : ITaxonomyDbContextFactory
    {
        private string connectionString;

        public TaxonomyDbContextFactory()
        {
            this.ConnectionString = ConnectionConstants.TaxonomyDbContextConnectionKey;
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

        public TaxonomyDbContext Create()
        {
            return new TaxonomyDbContext(this.ConnectionString);
        }
    }
}