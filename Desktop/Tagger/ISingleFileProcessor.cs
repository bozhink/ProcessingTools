namespace ProcessingTools.Tagger
{
    using System.Threading.Tasks;

    public interface ISingleFileProcessor
    {
        Task Run(ProgramSettings settings);
    }
}
