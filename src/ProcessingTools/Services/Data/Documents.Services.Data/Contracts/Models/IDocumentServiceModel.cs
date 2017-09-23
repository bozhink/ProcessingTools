namespace ProcessingTools.Documents.Services.Data.Contracts.Models
{
    using System;
    using ProcessingTools.Models.Contracts;

    public interface IDocumentServiceModel : IStringIdentifiable, IContentTypeable, ICommentable
    {
        long ContentLength { get; }

        DateTime DateCreated { get; }

        DateTime DateModified { get; }

        string FileExtension { get; }

        string FileName { get; }
    }
}
