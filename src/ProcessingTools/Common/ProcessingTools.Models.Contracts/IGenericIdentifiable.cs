namespace ProcessingTools.Contracts.Models
{
    public interface IGenericIdentifiable<out TId>
    {
        TId Id { get; }
    }
}
