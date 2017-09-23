namespace ProcessingTools.Models.Contracts
{
    public interface IGenericIdentifiable<out TId>
    {
        TId Id { get; }
    }
}
