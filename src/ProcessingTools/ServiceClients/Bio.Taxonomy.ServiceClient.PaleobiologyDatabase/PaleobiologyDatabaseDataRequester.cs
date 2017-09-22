namespace ProcessingTools.Bio.Taxonomy.ServiceClient.PaleobiologyDatabase
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.PaleobiologyDatabase.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.PaleobiologyDatabase.Models;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;

    public class PaleobiologyDatabaseDataRequester : IPaleobiologyDatabaseDataRequester
    {
        private const string PaleobiologyDatabaseBaseAddress = "https://paleobiodb.org";
        private readonly INetConnectorFactory connectorFactory;

        public PaleobiologyDatabaseDataRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        /// <summary>
        /// Search scientific name in The Paleobiology Database (PBDB).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>Taxonomic rank of the scientific name.</returns>
        /// <example>https://paleobiodb.org/data1.1/taxa/single.txt?name=Dascillidae</example>
        public async Task<string> SearchNameInPaleobiologyDatabase(string scientificName)
        {
            /*
             * "taxon_no","orig_no","record_type","associated_records","rank","taxon_name","common_name","status","parent_no","senior_no","reference_no","is_extant"
             * "69296","69296","taxon","","family","Dascillidae","soft bodied plant beetle","belongs to","69295","69296","5056","1"
             */
            string url = $"data1.1/taxa/single.txt?name={scientificName}";

            var connector = this.connectorFactory.Create(PaleobiologyDatabaseBaseAddress);
            string responseString = await connector.GetAsync(url, ContentTypes.Xml).ConfigureAwait(false);

            string keys = Regex.Match(responseString, "\\A[^\r\n]+").Value;
            string values = Regex.Match(responseString, "\n[^\r\n]+").Value;
            Match matchKeys = Regex.Match(keys, "(?<=\")[^,\"]*(?=\")");
            Match matchValues = Regex.Match(values, "(?<=\")[^,\"]*(?=\")");

            Hashtable response = new Hashtable();
            while (matchKeys.Success && matchValues.Success)
            {
                response.Add(matchKeys.Value, matchValues.Value);
                matchKeys = matchKeys.NextMatch();
                matchValues = matchValues.NextMatch();
            }

            string result = string.Empty;
            if (response["taxon_name"].ToString().CompareTo(scientificName) == 0)
            {
                result = response["rank"].ToString();
            }

            return result;
        }

        /// <summary>
        /// Search scientific name in The Paleobiology Database (PBDB).
        /// </summary>
        /// <param name="content">Scientific name of the taxon which rank is searched.</param>
        /// <returns>PbdbAllParents object which provides information about the scientific name.</returns>
        /// <example>https://paleobiodb.org/data1.1/taxa/list.json?name=Dascillidae&rel=all_parents</example>
        public async Task<PbdbAllParents> RequestDataAsync(string content)
        {
            string url = $"data1.1/taxa/list.json?name={content}&rel=all_parents";

            var connector = this.connectorFactory.Create(PaleobiologyDatabaseBaseAddress);
            var result = await connector.GetJsonObjectAsync<PbdbAllParents>(url).ConfigureAwait(false);
            return result;
        }
    }
}
