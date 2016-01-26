namespace ProcessingTools.Services.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Contracts;

    public interface IValidationService<T>
    {
        Task<IEnumerable<IValidationServiceModel<T>>> Validate(IEnumerable<T> items);
    }
}