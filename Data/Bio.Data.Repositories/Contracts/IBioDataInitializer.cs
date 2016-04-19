namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IBioDataInitializer : IDbContextInitializer<BioDbContext>
    {
    }
}
