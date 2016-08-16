namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using ProcessingTools.Contracts;

    public interface IDocumentEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        string ContentType { get; }

        long ContentLength { get; }

        long OriginalContentLength { get; }

        string FileName { get; }

        string OriginalFileName { get; }

        string FileExtension { get; }

        IArticleEntity Article { get; }
    }
}
