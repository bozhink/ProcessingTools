namespace ProcessingTools.Services.Common.Models.Contracts
{
    public interface IGenericServiceModel<T>
    {
        T Id { get; set; }
    }
}
