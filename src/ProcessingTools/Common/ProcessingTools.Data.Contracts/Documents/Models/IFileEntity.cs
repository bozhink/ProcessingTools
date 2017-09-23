namespace ProcessingTools.Contracts.Data.Documents.Models
{
    using ProcessingTools.Models.Contracts;

    public interface IFileEntity : IGuidIdentifiable, IModelWithUserInformation, IDescribable
    {
        long ContentLength { get; }

        string ContentType { get; }

        string FileExtension { get; }

        string FileName { get; }

        string FullName { get; }
    }
}
