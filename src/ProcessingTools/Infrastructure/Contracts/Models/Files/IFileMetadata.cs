namespace ProcessingTools.Contracts.Models.Files
{
    public interface IFileMetadata : IObjectIdentifiable, IDescribable, IContentTypable, IModelWithUserInformation, IFileNameable
    {
        long ContentLength { get; }

        string FileExtension { get; }

        string FullName { get; }
    }
}
