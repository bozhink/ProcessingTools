namespace ProcessingTools.Contracts.Services.Data.Files
{
    using IO;

    public interface IGenericFileContentDataService<TContent> : IGenericFileContentReader<TContent>, IGenericFileContentWriter<TContent>
    {
    }
}
