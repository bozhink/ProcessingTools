namespace ProcessingTools.Documents.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IDocumentsDbContext : IDbContext
    {
        IDbSet<Address> Addresses { get; set; }

        IDbSet<Affiliation> Affiliations { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<Author> Authors { get; set; }

        IDbSet<Document> Documents { get; set; }

        IDbSet<File> Files { get; set; }

        IDbSet<Institution> Institutions { get; set; }

        IDbSet<Journal> Journals { get; set; }

        IDbSet<Publisher> Publishers { get; set; }
    }
}
