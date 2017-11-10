namespace ProcessingTools.Services.Contracts.Data.Bio.Taxonomy
{
    using ProcessingTools.Services.Contracts.Data;

    public interface IBlackListDataService : IAddableDataService<string>, IDeletableDataService<string>
    {
    }
}
