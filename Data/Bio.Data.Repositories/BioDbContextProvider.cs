namespace ProcessingTools.Bio.Data.Repositories
{
    using Contracts;

    public class BioDbContextProvider : IBioDbContextProvider
    {
        public BioDbContext Create()
        {
            return BioDbContext.Create();
        }
    }
}