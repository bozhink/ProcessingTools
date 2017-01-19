namespace ProcessingTools.Tagger.Contracts.Commands
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface ITaggerController
    {
        Task<object> Run(IDocument document, IProgramSettings settings);
    }
}
