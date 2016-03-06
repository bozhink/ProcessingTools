namespace ProcessingTools.Bio.Biorepositories.Data
{
    using Contracts;

    public class BiorepositoriesDbContextProvider : IBiorepositoriesDbContextProvider
    {
        public BiorepositoriesDbContext Create()
        {
            return BiorepositoriesDbContext.Create();
        }
    }
}