// <copyright file="PaleobiologyDatabaseDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Taxonomy.PaleobiologyDatabase
{
    using System;
    using System.Collections;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.PaleobiologyDatabase.Json;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Paleobiology Database (PBDB) data requester.
    /// </summary>
    public class PaleobiologyDatabaseDataRequester : IPaleobiologyDatabaseDataRequester
    {
        private const string PaleobiologyDatabaseBaseAddress = "https://paleobiodb.org";
        private readonly IHttpRequester httpRequester;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaleobiologyDatabaseDataRequester"/> class.
        /// </summary>
        /// <param name="httpRequester">HTTP requester.</param>
        public PaleobiologyDatabaseDataRequester(IHttpRequester httpRequester)
        {
            this.httpRequester = httpRequester ?? throw new ArgumentNullException(nameof(httpRequester));
        }

        /// <summary>
        /// Search scientific name in The Paleobiology Database (PBDB).
        /// </summary>
        /// <param name="scientificName">Scientific name of the taxon which rank is searched.</param>
        /// <returns>Taxonomic rank of the scientific name.</returns>
        // Example: https://paleobiodb.org/data1.1/taxa/single.txt?name=Dascillidae
        public async Task<string> SearchNameInPaleobiologyDatabase(string scientificName)
        {
            /*
             * "taxon_no","orig_no","record_type","associated_records","rank","taxon_name","common_name","status","parent_no","senior_no","reference_no","is_extant"
             * "69296","69296","taxon","","family","Dascillidae","soft bodied plant beetle","belongs to","69295","69296","5056","1"
             */
            Uri requestUri = UriExtensions.Append(PaleobiologyDatabaseBaseAddress, $"data1.1/taxa/single.txt?name={scientificName}");

            string responseString = await this.httpRequester.GetStringAsync(requestUri, ContentTypes.Xml).ConfigureAwait(false);

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
        // Example: https://paleobiodb.org/data1.1/taxa/list.json?name=Dascillidae&amp;rel=all_parents
        public async Task<PbdbAllParents> RequestDataAsync(string content)
        {
            Uri requestUri = UriExtensions.Append(PaleobiologyDatabaseBaseAddress, $"data1.1/taxa/list.json?name={content}&rel=all_parents");

            var result = await this.httpRequester.GetJsonToObjectAsync<PbdbAllParents>(requestUri).ConfigureAwait(false);
            return result;
        }
    }
}
