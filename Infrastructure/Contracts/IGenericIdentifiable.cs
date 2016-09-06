namespace ProcessingTools.Contracts
{
    public interface IGenericIdentifiable<TId>
    {
        TId Id { get; }
    }
}
