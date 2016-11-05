namespace ProcessingTools.Bio.Environments.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class BioEnvironmentsDbContextFactory : IBioEnvironmentsDbContextFactory
    {
        private string connectionString;

        public BioEnvironmentsDbContextFactory()
        {
            this.ConnectionString = ConnectionStringsKeys.BioEnvironmentsDatabaseConnectionKey;
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

        public BioEnvironmentsDbContext Create()
        {
            return new BioEnvironmentsDbContext(this.ConnectionString);
        }
    }
}