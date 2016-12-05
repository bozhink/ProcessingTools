using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts;
using ProcessingTools.Common;
using ProcessingTools.Net.Factories.Contracts;

namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Clients
{
    public class MaterialCitationsParser : IMaterialCitationsParser
    {
        private const string BaseAddress = "http://plazi2.cs.umb.edu";
        private const string ParserUrl = "/GgWS/wss/invokeFunction";

        private readonly INetConnectorFactory connectorFactory;
        private readonly Encoding encoding;

        public MaterialCitationsParser(INetConnectorFactory connectorFactory)
            : this(connectorFactory, Defaults.DefaultEncoding)
        {
        }

        public MaterialCitationsParser(INetConnectorFactory connectorFactory, Encoding encoding)
        {
            if (connectorFactory == null)
            {
                throw new ArgumentNullException(nameof(connectorFactory));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            this.encoding = encoding;

            this.connectorFactory = connectorFactory;
        }

        public async Task<string> Invoke(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var values = new Dictionary<string, string>
            {
                { "data", content },
                { "functionName", "XmlExample.webService" },
                { "dataFormat", "XML" },
                { "INTERACTIVE", "no" }
            };

            var connector = this.connectorFactory.Create(BaseAddress);
            var response = await connector.Post(ParserUrl, values, this.encoding);

            return response;
        }
    }
}
