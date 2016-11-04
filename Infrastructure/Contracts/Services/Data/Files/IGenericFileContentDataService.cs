namespace ProcessingTools.Contracts.Services.Data.Files
{
    using System.Threading.Tasks;

    public interface IGenericFileContentDataService<TContent>
    {
        Task<TContent> Read(object id);

        Task<object> Write(object id, TContent content);
    }
}
