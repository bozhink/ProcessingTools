namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using ProcessingTools.Contracts;

    public interface IFileEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        long ContentLength { get; }

        string ContentType { get; }

        string FileExtension { get; }

        string FileName { get; }

        long OriginalContentLength { get; }

        string OriginalFileName { get; }
    }
}
