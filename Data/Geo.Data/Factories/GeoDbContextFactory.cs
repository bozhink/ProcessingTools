namespace ProcessingTools.Geo.Data.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Geo.Data.Common.Constants;

    public class GeoDbContextFactory : IGeoDbContextFactory
    {
        private string connectionString;

        public GeoDbContextFactory()
        {
            this.ConnectionString = ConnectionConstants.GeoDatabseConnectionKey;
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

        public GeoDbContext Create()
        {
            return new GeoDbContext(this.ConnectionString);
        }
    }
}