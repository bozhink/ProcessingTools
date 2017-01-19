namespace ProcessingTools.Tagger.Contracts.Controllers
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface ITaggerController
    {
        Task<object> Run(IDocument document, IProgramSettings settings);
    }
}
