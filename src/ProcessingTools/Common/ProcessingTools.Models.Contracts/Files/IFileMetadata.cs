namespace ProcessingTools.Models.Contracts.Files
{
    public interface IFileMetadata : IObjectIdentifiable, IDescribable, IContentTypeable, IModelWithUserInformation, IFileNameable
    {
        long ContentLength { get; }

        string FileExtension { get; }

        string FullName { get; }
    }
}
