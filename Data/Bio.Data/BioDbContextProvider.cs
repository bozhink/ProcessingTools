namespace ProcessingTools.Bio.Data
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