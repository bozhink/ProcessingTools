namespace ProcessingTools.Documents.Data.Entity.Factories
{
    using System;
    using Contracts;
    using ProcessingTools.Constants.Configuration;

    public class DocumentsDbContextFactory : IDocumentsDbContextFactory
    {
        private string connectionString;

        public DocumentsDbContextFactory()
        {
            this.ConnectionString = ConnectionStringsKeys.DocumentsDatabaseConnectionKey;
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

        public DocumentsDbContext Create()
        {
            return new DocumentsDbContext(this.ConnectionString);
        }
    }
}