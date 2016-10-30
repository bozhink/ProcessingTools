namespace ProcessingTools.Services.Validation.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Contracts;

    public interface IValidationService<T>
    {
        Task<IEnumerable<IValidationServiceModel<T>>> Validate(params T[] items);
    }
}