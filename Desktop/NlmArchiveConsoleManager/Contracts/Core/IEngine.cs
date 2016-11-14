namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Core
{
    using System.Threading.Tasks;

    public interface IEngine
    {
        Task Run(params string[] args);
    }
}
