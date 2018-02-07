namespace ProcessingTools.Documents.Data.Entity
{
    using System;
    using ProcessingTools.Constants.Configuration;
    using ProcessingTools.Documents.Data.Entity.Contracts;

    public class DocumentsDbContextFactory : IDocumentsDbContextFactory
    {
        private string connectionString;

        public DocumentsDbContextFactory()
        {
            this.ConnectionString = ConnectionStrings.DocumentsDatabaseConnection;
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

        public DocumentsDbContext Create()
        {
            return new DocumentsDbContext(this.ConnectionString);
        }
    }
}
