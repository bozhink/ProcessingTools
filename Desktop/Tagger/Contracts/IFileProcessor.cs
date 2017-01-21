namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;

    public interface IFileProcessor
    {
        Task Run(IProgramSettings settings);
    }
}
