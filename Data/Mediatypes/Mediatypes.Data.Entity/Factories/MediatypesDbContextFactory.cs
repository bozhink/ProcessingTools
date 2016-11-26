namespace ProcessingTools.MediaType.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class MediatypesDbContextFactory : IMediatypesDbContextFactory
    {
        private string connectionString;

        public MediatypesDbContextFactory()
        {
            this.ConnectionString = ConnectionStringsKeys.MediatypesDatabaseConnectionKey;
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

        public MediaTypesDbContext Create()
        {
            return new MediaTypesDbContext(this.ConnectionString);
        }
    }
}
