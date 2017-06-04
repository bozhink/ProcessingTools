namespace ProcessingTools.Contracts.Net
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INetConnector
    {
        Task<string> GetAsync(string url, string acceptContentType);

        Task<T> GetJsonObjectAsync<T>(string url) where T : class;

        Task<T> GetXmlObjectAsync<T>(string url) where T : class;

        Task<string> PostAsync(string url, string content, string contentType, Encoding encoding);

        Task<string> PostAsync(string url, IDictionary<string, string> values, Encoding encoding);

        Task<T> PostXmlObjectAsync<T>(string url, IDictionary<string, string> values, Encoding encoding) where T : class;
    }
}
