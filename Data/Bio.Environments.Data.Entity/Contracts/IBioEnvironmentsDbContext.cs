namespace ProcessingTools.Bio.Environments.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioEnvironmentsDbContext : IDbContext
    {
        IDbSet<EnvoEntity> EnvoEntities { get; set; }

        IDbSet<EnvoGlobal> EnvoGlobals { get; set; }

        IDbSet<EnvoGroup> EnvoGroups { get; set; }

        IDbSet<EnvoName> EnvoNames { get; set; }
    }
}
