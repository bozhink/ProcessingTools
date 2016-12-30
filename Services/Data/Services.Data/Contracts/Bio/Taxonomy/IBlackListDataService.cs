namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Data;

    public interface IBlackListDataService : IAddableDataService<string>, IDeletableDataService<string>
    {
    }
}
