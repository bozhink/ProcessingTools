namespace ProcessingTools.Bio.Environments.Data.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Environments.Data.Common.Constants;

    public class BioEnvironmentsDbContextFactory : IBioEnvironmentsDbContextFactory
    {
        private string connectionString;

        public BioEnvironmentsDbContextFactory()
        {
            this.ConnectionString = ConnectionConstants.BioEnvironmentsDbContextConnectionKey;
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