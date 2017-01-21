namespace ProcessingTools.Tagger.Contracts
{
    using System.Threading.Tasks;

    public interface IFileProcessor
    {
        string OutputFileName { get; set; }

        Task Run(IProgramSettings settings);
    }
}
