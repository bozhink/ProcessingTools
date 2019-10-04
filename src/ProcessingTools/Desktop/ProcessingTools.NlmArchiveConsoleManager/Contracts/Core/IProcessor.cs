namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Core
{
    using System.Threading.Tasks;

    public interface IProcessor
    {
        Task Process();
    }
}
