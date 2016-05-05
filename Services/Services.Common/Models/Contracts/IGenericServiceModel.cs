namespace ProcessingTools.Services.Common.Models.Contracts
{
    public interface IGenericServiceModel<TId>
    {
        TId Id { get; set; }
    }
}
