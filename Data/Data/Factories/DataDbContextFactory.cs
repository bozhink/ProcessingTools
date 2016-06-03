namespace ProcessingTools.Data.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Data.Common.Constants;

    public class DataDbContextFactory : IDataDbContextFactory
    {
        private string connectionString;

        public DataDbContextFactory()
        {
            this.ConnectionString = ConnectionConstants.DataDatabseConnectionKey;
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

        public DataDbContext Create()
        {
            return new DataDbContext(this.ConnectionString);
        }
    }
}