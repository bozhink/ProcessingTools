namespace ProcessingTools.Services.Validation.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IValidationService<T>
    {
        Task<IEnumerable<IValidationServiceModel<T>>> Validate(params T[] items);
    }
}
