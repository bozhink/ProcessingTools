namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Tagger.Commands.Contracts;

    public interface IFileProcessor
    {
        Task Run(IProgramSettings settings);
    }
}
