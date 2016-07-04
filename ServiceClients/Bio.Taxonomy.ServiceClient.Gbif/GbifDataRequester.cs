namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using ProcessingTools.Net;
    using ProcessingTools.Net.Contracts;

    public class GbifDataRequester : IGbifDataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";

        private readonly INetConnector connector;

        public GbifDataRequester()
            : this(new NetConnector())
        {
        }

        public GbifDataRequester(INetConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException(nameof(connector));
            }

            this.connector = connector;
        }

        public async Task<GbifApiResponseModel> RequestData(string scientificName)
        {
            string url = $"v0.9/species/match?verbose=true&name={scientificName}";
            var connector = this.connector.Create(BaseAddress);
            var result = await connector.GetAndDeserializeDataContractJson<GbifApiResponseModel>(url);
            return result;
        }
    }
}
