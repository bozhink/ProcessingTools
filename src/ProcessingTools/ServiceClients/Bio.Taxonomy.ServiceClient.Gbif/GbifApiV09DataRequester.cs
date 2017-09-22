namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Models;
    using ProcessingTools.Contracts;

    public class GbifApiV09DataRequester : IGbifApiV09DataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";

        private readonly INetConnectorFactory connectorFactory;

        public GbifApiV09DataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        public async Task<GbifApiV09ResponseModel> RequestDataAsync(string content)
        {
            string url = $"v0.9/species/match?verbose=true&name={content}";
            var connector = this.connectorFactory.Create(BaseAddress);
            var result = await connector.GetJsonObjectAsync<GbifApiV09ResponseModel>(url).ConfigureAwait(false);
            return result;
        }
    }
}
