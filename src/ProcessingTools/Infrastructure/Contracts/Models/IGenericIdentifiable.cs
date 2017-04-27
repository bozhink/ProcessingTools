namespace ProcessingTools.Contracts.Models
{
    public interface IGenericIdentifiable<TId>
    {
        TId Id { get; }
    }
}
