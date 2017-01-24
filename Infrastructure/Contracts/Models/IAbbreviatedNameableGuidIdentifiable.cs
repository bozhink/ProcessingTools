namespace ProcessingTools.Contracts.Models
{
    public interface IAbbreviatedNameableGuidIdentifiable : INameableGuidIdentifiable
    {
        string AbbreviatedName { get; }
    }
}
