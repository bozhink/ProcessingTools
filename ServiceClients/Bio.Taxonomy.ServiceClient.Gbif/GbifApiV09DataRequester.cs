namespace ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using ProcessingTools.Net;
    using ProcessingTools.Net.Contracts;

    public class GbifApiV09DataRequester : IGbifApiV09DataRequester
    {
        private const string BaseAddress = "http://api.gbif.org";

        private readonly INetConnector connector;

        public GbifApiV09DataRequester()
            : this(new NetConnector())
        {
        }

        public GbifApiV09DataRequester(INetConnector connector)
        {
            if (connector == null)
            {
                throw new ArgumentNullException(nameof(connector));
            }

            this.connector = connector;
        }

        public async Task<GbifApiV09ResponseModel> RequestData(string scientificName)
        {
            string url = $"v0.9/species/match?verbose=true&name={scientificName}";
            var connector = this.connector.Create(BaseAddress);
            var result = await connector.GetAndDeserializeDataContractJson<GbifApiV09ResponseModel>(url);
            return result;
        }
    }
}
