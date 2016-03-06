namespace ProcessingTools.Bio.Biorepositories.Data
{
    using Contracts;

    public class BiorepositoriesDbFirstDbContextProvider : IBiorepositoriesDbFirstDbContextProvider
    {
        public BiorepositoriesDbFirstDbContext Create()
        {
            return new BiorepositoriesDbFirstDbContext();
        }
    }
}