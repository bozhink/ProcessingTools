namespace ProcessingTools.DataResources.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class ResourcesDbContextFactory : IResourcesDbContextFactory
    {
        private string connectionString;

        public ResourcesDbContextFactory()
        {
            this.ConnectionString = ConnectionStrings.DataResourcesDatabaseConnection;
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

        public ResourcesDbContext Create()
        {
            return new ResourcesDbContext(this.ConnectionString);
        }
    }
}
