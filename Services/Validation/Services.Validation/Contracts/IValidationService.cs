namespace ProcessingTools.Services.Validation.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Contracts;

    public interface IValidationService<TServiceModel>
    {
        Task<IEnumerable<IValidationServiceModel<TServiceModel>>> Validate(params TServiceModel[] items);
    }
}