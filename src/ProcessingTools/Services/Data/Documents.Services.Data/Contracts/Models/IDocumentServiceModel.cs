namespace ProcessingTools.Documents.Services.Data.Contracts.Models
{
    using System;
    using ProcessingTools.Contracts.Models;

    public interface IDocumentServiceModel : IStringIdentifiable, IContentTypable, ICommentable
    {
        long ContentLength { get; }

        DateTime DateCreated { get; }

        DateTime DateModified { get; }

        string FileExtension { get; }

        string FileName { get; }
    }
}
