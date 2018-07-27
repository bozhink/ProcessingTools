// <copyright file="AphiaDirectSoapRequester.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Aphia
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Aphia direct SOAP requester.
    /// </summary>
    public class AphiaDirectSoapRequester
    {
        private const string BaseAddress = "http://www.marinespecies.org";
        private const string ApiUrl = "aphia.php?p=soap";

        private readonly INetConnectorFactory connectorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AphiaDirectSoapRequester"/> class.
        /// </summary>
        /// <param name="connectorFactory">Connection factory.</param>
        public AphiaDirectSoapRequester(INetConnectorFactory connectorFactory)
        {
            this.connectorFactory = connectorFactory ?? throw new ArgumentNullException(nameof(connectorFactory));
        }

        /// <summary>
        /// Gets Aphia SOAP request XML with a specified scientific name.
        /// </summary>
        /// <param name="scientificName">Scientific name to be requested.</param>
        /// <returns><see cref="XmlDocument"/></returns>
        public XmlDocument GetAphiaSoapXml(string scientificName)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(@"<?xml version=""1.0""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
    xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
    xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soap:Body>
        <getAphiaRecords xmlns=""http://tempuri.org/"">
            <scientificname>" + scientificName + @"</scientificname>
            <marine_only>false</marine_only>
        </getAphiaRecords>
    </soap:Body>
</soap:Envelope>");
            return xml;
        }

        /// <summary>
        /// Search scientific name in Aphia.
        /// </summary>
        /// <param name="scientificName">Scientific name to be requested.</param>
        /// <returns>Task of <see cref="XmlDocument"/></returns>
        public async Task<XmlDocument> SearchAphia(string scientificName)
        {
            var connector = this.connectorFactory.Create(BaseAddress);
            var response = await connector.PostAsync(
                ApiUrl,
                this.GetAphiaSoapXml(scientificName).OuterXml,
                ContentTypes.Xml,
                Defaults.Encoding)
                .ConfigureAwait(false);

            return response.ToXmlDocument();
        }
    }
}
