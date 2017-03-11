namespace ProcessingTools.Journals.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IJournalsDbContext : IDbContext
    {
        IDbSet<Address> Addresses { get; set; }

        IDbSet<Publisher> Publishers { get; set; }

        IDbSet<Institution> Institutions { get; set; }
    }
}
