namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBioEnvironmentsRepository<T> : IRepository<T>
        where T : class
    {
    }
}