namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using System.Threading.Tasks;

    public interface IBioDataInitializer
    {
        Task Init();
    }
}
