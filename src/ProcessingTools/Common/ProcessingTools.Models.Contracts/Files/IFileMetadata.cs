namespace ProcessingTools.Models.Contracts.Files
{
    public interface IFileMetadata : IIdentifiable, IDescribable, IContentTypeable, IModelWithUserInformation, IFileNameable
    {
        long ContentLength { get; }

        string FileExtension { get; }

        string FullName { get; }
    }
}
