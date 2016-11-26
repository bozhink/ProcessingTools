namespace ProcessingTools.Resources.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IResourcesDbContext : IDbContext
    {
        IDbSet<Abbreviation> Abbreviations { get; set; }

        IDbSet<ContentType> ContentTypes { get; set; }

        IDbSet<Institution> Institutions { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<SourceId> Sources { get; set; }
    }
}
