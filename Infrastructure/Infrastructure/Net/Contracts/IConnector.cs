namespace ProcessingTools.Infrastructure.Net.Contracts
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IConnector
    {
        string BaseAddress { get; set; }

        Task<string> Get(string url, string acceptContentType);

        Task<string> Post(string url, string content, string contentType, Encoding encoding);

        Task<string> Post(string url, IDictionary<string, string> values, Encoding encoding);
    }
}
