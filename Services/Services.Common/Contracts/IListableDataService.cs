namespace ProcessingTools.Services.Common.Contracts
{
    using Models.Contracts;

    public interface IListableDataService<T> : IIterableDataService<T>
        where T : IListableServiceModel
    {
    }
}
