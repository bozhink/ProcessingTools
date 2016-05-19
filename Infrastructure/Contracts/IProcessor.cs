namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface IProcessor
    {
        Task Process();
    }
}