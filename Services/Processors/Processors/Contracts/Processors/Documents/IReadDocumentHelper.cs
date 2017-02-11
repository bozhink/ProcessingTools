namespace ProcessingTools.Processors.Contracts.Processors.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    public interface IReadDocumentHelper
    {
        Task<IDocument> Read(bool mergeInputFiles, params string[] fileNames);
    }
}
