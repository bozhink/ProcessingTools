namespace ProcessingTools.Services.Validation.Comparers
{
    using System.Collections.Generic;
    using Models.Contracts;

    public class UrlEqualityComparer : IEqualityComparer<IUrlServiceModel>
    {
        public bool Equals(IUrlServiceModel x, IUrlServiceModel y)
        {
            return ((x.BaseAddress == y.BaseAddress) && (x.Address == y.Address)) || (x.FullAddress == y.FullAddress);
        }

        public int GetHashCode(IUrlServiceModel obj)
        {
            return (obj.FullAddress + obj.BaseAddress + obj.Address).GetHashCode();
        }
    }
}
