namespace ProcessingTools.Contracts
{
    public interface IIdentifiable<TId>
    {
        TId Id { get; }
    }
}
