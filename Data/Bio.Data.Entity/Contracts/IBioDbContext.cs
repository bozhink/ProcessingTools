namespace ProcessingTools.Bio.Data.Entity.Contracts
{
    using System.Data.Entity;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioDbContext : IDbContext
    {
        IDbSet<MorphologicalEpithet> MorphologicalEpithets { get; set; }

        IDbSet<TypeStatus> TypesStatuses { get; set; }
    }
}
