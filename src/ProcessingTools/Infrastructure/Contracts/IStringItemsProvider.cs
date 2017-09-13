namespace ProcessingTools.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStringItemsProvider
    {
        Task<IEnumerable<string>> ItemsAsync { get; }
    }
}
