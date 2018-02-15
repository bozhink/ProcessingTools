namespace ProcessingTools.Processors.Contracts.Bio.Codes
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface ICodesTagger
    {
        Task TagKnownSpecimenCodes(IDocument document);

        Task TagSpecimenCodes(IDocument document);
    }
}
