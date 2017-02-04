namespace ProcessingTools.Tagger.Commands.Contracts.Commands
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface ITaggerCommand
    {
        Task<object> Run(IDocument document, IProgramSettings settings);
    }
}
