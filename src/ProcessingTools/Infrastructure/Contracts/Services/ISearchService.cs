namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchService<Tin, Tout>
    {
        Task<IEnumerable<Tout>> Search(Tin filter);
    }
}
