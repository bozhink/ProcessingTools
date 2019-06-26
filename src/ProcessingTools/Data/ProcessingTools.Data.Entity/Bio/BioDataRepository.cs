namespace ProcessingTools.Data.Entity.Bio
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioDataRepository<T> : EntityRepository<BioDbContext, T>, IBioDataRepository<T>
        where T : class
    {
        public BioDataRepository(BioDbContext context)
            : base(context)
        {
        }
    }
}
