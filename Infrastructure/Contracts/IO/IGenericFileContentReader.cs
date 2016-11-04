namespace ProcessingTools.Contracts.IO
{
    using System.Threading.Tasks;

    public interface IGenericFileContentReader<TContent>
    {
        Task<TContent> Read(object id);
    }
}
