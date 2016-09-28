namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    public interface ISimpleProcessor : IProcessor
    {
        Task Process();
    }
}