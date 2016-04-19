namespace ProcessingTools.Data.Common.Contracts
{
    using System.Threading.Tasks;

    public interface IDatabaseInitializer
    {
        Task Initialize();
    }
}