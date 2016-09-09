namespace ProcessingTools.Resources.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Resources.Data.Common.Constants;

    public class DataResourcesDbContextFactory : IDataResourcesDbContextFactory
    {
        private string connectionString;

        public DataResourcesDbContextFactory()
        {
            this.ConnectionString = ConnectionConstants.DataResourcesDatabaseConnectionKey;
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
