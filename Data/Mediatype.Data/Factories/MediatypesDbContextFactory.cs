namespace ProcessingTools.MediaType.Data.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.MediaType.Data.Common.Constants;

    public class MediatypesDbContextFactory : IMediatypesDbContextFactory
    {
        private string connectionString;

        public MediatypesDbContextFactory()
        {
            this.ConnectionString = ConnectionConstants.MediatypesDatabaseConnectionKey;
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