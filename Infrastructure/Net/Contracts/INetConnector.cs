namespace ProcessingTools.Net.Contracts
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INetConnector
    {
        string BaseAddress { get; set; }

        Task<string> Get(string url, string acceptContentType);

        Task<string> Post(string url, string content, string contentType, Encoding encoding);

        Task<string> Post(string url, IDictionary<string, string> values, Encoding encoding);

        Task<T> GetAndDeserializeXml<T>(string url) where T : class;

        Task<T> GetAndDeserializeDataContractJson<T>(string url) where T : class;

        Task<T> PostAndDeserializeXml<T>(string url, Dictionary<string, string> values, Encoding encoding) where T : class;
    }
}
