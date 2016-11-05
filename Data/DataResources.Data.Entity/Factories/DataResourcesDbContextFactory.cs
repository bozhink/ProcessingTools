namespace ProcessingTools.DataResources.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class DataResourcesDbContextFactory : IDataResourcesDbContextFactory
    {
        private string connectionString;

        public DataResourcesDbContextFactory()
        {
            this.ConnectionString = ConnectionStringsKeys.DataResourcesDatabaseConnectionKey;
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

        public DataResourcesDbContext Create()
        {
            return new DataResourcesDbContext(this.ConnectionString);
        }
    }
}
