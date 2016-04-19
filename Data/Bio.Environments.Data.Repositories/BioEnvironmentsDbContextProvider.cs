namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using Contracts;

    public class BioEnvironmentsDbContextProvider : IBioEnvironmentsDbContextProvider
    {
        public BioEnvironmentsDbContext Create()
        {
            return BioEnvironmentsDbContext.Create();
        }
    }
}