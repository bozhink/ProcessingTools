namespace ProcessingTools.Services.Data.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Services.Contracts.Data;

    public interface IBlackListDataService : IAddableDataService<string>, IDeletableDataService<string>
    {
    }
}
