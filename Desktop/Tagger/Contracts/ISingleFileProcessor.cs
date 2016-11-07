namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;

    public interface ISingleFileProcessor
    {
        Task Run(IProgramSettings settings);
    }
}
