namespace ProcessingTools.Bio.ServiceClient.MaterialsParser.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.ServiceClient.MaterialsParser.Contracts;
    using ProcessingTools.Constants;
    using ProcessingTools.Net.Factories.Contracts;

    public class MaterialCitationsParser : IMaterialCitationsParser
    {
        private const string BaseAddress = "http://plazi2.cs.umb.edu";
        private const string ParserUrl = "/GgWS/wss/invokeFunction";

        private readonly INetConnectorFactory connectorFactory;
        private readonly Encoding encoding;

        public MaterialCitationsParser(INetConnectorFactory connectorFactory)
            : this(connectorFactory, Defaults.Encoding)
        {
        }

        public MaterialCitationsParser(INetConnectorFactory connectorFactory, Encoding encoding)
        {
            this.encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
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
            var response = await connector.PostAsync(ParserUrl, values, this.encoding);

            return response;
        }
    }
}
