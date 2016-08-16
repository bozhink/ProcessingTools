namespace ProcessingTools.Documents.Data.Common.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts;

    public interface IDocumentEntity : IGuidIdentifiable, IModelWithUserInformation
    {
        Guid ArticleId { get; }

        long ContentLength { get; }

        string ContentType { get; }

        string FileExtension { get; }

        string FileName { get; }

        long OriginalContentLength { get; }

        string OriginalFileName { get; }
    }
}
