namespace ProcessingTools.Services.Validation.Comparers
{
    using System.Collections.Generic;
    using Models.Contracts;

    public class UrlEqualityComparer : IEqualityComparer<IUrl>
    {
        public bool Equals(IUrl x, IUrl y)
        {
            return ((x.BaseAddress == y.BaseAddress) && (x.Address == y.Address)) || (x.FullAddress == y.FullAddress);
        }

        public int GetHashCode(IUrl obj)
        {
            return (obj.FullAddress + obj.BaseAddress + obj.Address).GetHashCode();
        }
    }
}
