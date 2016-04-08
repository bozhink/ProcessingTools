namespace ProcessingTools.Bio.ServiceClient.MaterialsParser
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Infrastructure.Net;

    public class MaterialCitationsParser : IMaterialCitationsParser
    {
        private const string BaseAddress = "http://plazi2.cs.umb.edu";
        private const string ParserUrl = "/GgWS/wss/test";

        private readonly Encoding encoding;

        public MaterialCitationsParser()
            : this(Encoding.UTF8)
        {
        }

        public MaterialCitationsParser(Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            this.encoding = encoding;
        }

        public async Task<string> Invoke(string citations)
        {
            if (string.IsNullOrWhiteSpace(citations))
            {
                throw new ArgumentNullException(nameof(citations));
            }

            var values = new Dictionary<string, string>
            {
                { "dataField", citations },
                { "dataUrl", string.Empty },
                { "functionName", "XmlExample.webService" },
                { "dataFormat", "XML" },
                { "INTERACTIVE", "no" }
            };

            var connector = new Connector(BaseAddress);
            var response = await connector.PostAsync(ParserUrl, values, this.encoding);

            return response;
        }
    }
}
