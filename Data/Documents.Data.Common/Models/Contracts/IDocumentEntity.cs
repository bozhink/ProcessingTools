namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts;

    // TODO: separation with IFileEntity
    public interface IDocumentEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        Guid ArticleId { get; }

        // TODO: add
        // Guid FileId { get; }

        // TODO: add
        // string Description { get; }

        // TODO: remove
        long ContentLength { get; }

        // TODO: remove
        string ContentType { get; }

        // TODO: remove
        string FileExtension { get; }

        // TODO: remove
        string FileName { get; }

        // TODO: remove
        long OriginalContentLength { get; }

        // TODO: remove
        string OriginalFileName { get; }
    }
}
