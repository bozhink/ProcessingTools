namespace ProcessingTools.Journals.Data.Entity
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity;
    using ProcessingTools.Journals.Data.Entity.Contracts;
    using ProcessingTools.Journals.Data.Entity.Models;

    public class JournalsDbContext : EntityDbContext, IJournalsDbContext
    {
        public JournalsDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<Publisher> Publishers { get; set; }

        public IDbSet<Institution> Institutions { get; set; }
    }
}
