namespace ProcessingTools.Data.Common.Models.Contracts
{
    public interface IGenericEntity<T>
    {
        T Id { get; set; }
    }
}
