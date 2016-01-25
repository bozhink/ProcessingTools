namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBioEnvironmentsRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}