namespace ProcessingTools.Contracts.Services.Data
{
    using Models;

    public interface IListableDataService<T> : IIterableDataService<T>
        where T : IListableServiceModel
    {
    }
}
