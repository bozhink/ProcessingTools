namespace ProcessingTools.Contracts.Processors.Processors.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IReadDocumentHelper
    {
        Task<IDocument> Read(bool mergeInputFiles, params string[] fileNames);
    }
}
