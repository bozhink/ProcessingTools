namespace ProcessingTools.Journals.Services.Data.Abstractions.Services
{
    using Contracts.Models;
    using Contracts.Services;
    using System;
    using System.Threading.Tasks;

    public abstract class AbstractAddressableDataService<T> : IAddressableDataService<T>
        where T : IAddressable
    {
        public virtual Task<object> AddAddress(object modelId, IAddress address)
        {
            if (modelId == null)
            {
                throw new ArgumentNullException(nameof(modelId));
            }

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            throw new NotImplementedException();
        }

        public virtual Task<object> RemoveAddress(object modelId, object addressId)
        {
            if (modelId == null)
            {
                throw new ArgumentNullException(nameof(modelId));
            }

            if (addressId == null)
            {
                throw new ArgumentNullException(nameof(addressId));
            }

            throw new NotImplementedException();
        }
    }
}
