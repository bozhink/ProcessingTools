namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioEnvironmentsRepository<T> : EntityRepository<BioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        public BioEnvironmentsRepository(BioEnvironmentsDbContext context)
            : base(context)
        {
        }
    }
}
