using System.Threading.Tasks;

namespace ProcessingTools.Tagger
{
    public interface ISingleFileProcessor
    {
        Task Run(ProgramSettings settings);
    }
}