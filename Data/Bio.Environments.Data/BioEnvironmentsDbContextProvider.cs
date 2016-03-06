namespace ProcessingTools.Bio.Environments.Data
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