namespace ProcessingTools.Services.Validation.Comparers
{
    using System.Collections.Generic;
    using Models;

    public class UrlEqualityComparer : IEqualityComparer<UrlServiceModel>
    {
        public bool Equals(UrlServiceModel x, UrlServiceModel y)
        {
            return ((x.BaseAddress == y.BaseAddress) && (x.Address == y.Address)) || (x.FullAddress == y.FullAddress);
        }

        public int GetHashCode(UrlServiceModel obj)
        {
            return (obj.FullAddress + obj.BaseAddress + obj.Address).GetHashCode();
        }
    }
}
