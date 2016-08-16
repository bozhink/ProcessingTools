namespace ProcessingTools.Contracts
{
    public interface IAbbreviatedNameableGuidIdentifiable : INameableGuidIdentifiable
    {
        string AbbreviatedName { get; }
    }
}
