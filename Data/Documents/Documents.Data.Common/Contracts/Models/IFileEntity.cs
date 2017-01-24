namespace ProcessingTools.Documents.Data.Common.Contracts.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IFileEntity : IGuidIdentifiable, IModelWithUserInformation, IDescribable
    {
        long ContentLength { get; }

        string ContentType { get; }

        string FileExtension { get; }

        string FileName { get; }

        string FullName { get; }
    }
}
