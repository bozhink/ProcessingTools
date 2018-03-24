namespace ProcessingTools.Bio.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class BioDbContextFactory : IBioDbContextFactory
    {
        private string connectionString;

        public BioDbContextFactory()
        {
            this.ConnectionString = ConnectionStrings.BioDatabaseConnection;
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

        public BioDbContext Create()
        {
            return new BioDbContext(this.ConnectionString);
        }
    }
}
