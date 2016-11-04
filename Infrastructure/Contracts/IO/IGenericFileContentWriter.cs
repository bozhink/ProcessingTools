namespace ProcessingTools.Contracts.IO
{
    using System.Threading.Tasks;

    public interface IGenericFileContentWriter<TContent>
    {
        Task<object> Write(object id, TContent content);
    }
}
