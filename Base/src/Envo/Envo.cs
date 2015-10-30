﻿namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Xml;
    using Configurator;
    using Contracts;
    using Extensions;
    using ServiceClient.ExtractHcmr;

    public class Envo : TaggerBase, IBaseTagger
    {
        private ILogger logger;

        public Envo(string xml, ILogger logger)
            : base(xml)
        {
            this.logger = logger;
        }

        public Envo(Config config, string xml, ILogger logger)
            : base(config, xml)
        {
            this.logger = logger;
        }

        public Envo(IBase baseObject, ILogger logger)
            : base(baseObject)
        {
            this.logger = logger;
        }

        public void Tag()
        {
            this.Tag(new XPathProvider(this.Config));
        }

        public void Tag(IXPathProvider xpathProvider)
        {
            NameTable nameTable = new NameTable();
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
            namespaceManager.AddNamespace("reflect", "Reflect");
            namespaceManager.PushScope();

            XmlDocument envoTermsTagSet = new XmlDocument();
            {
                XmlDocument envoTermsResponse = ExtractHcmrResolverDataRequester.UseExtractService(this.TextContent).Result;
                envoTermsResponse.SelectNodes("//reflect:count", namespaceManager).RemoveXmlNodes();

                try
                {
                    envoTermsResponse.Save(this.Config.EnvoResponseOutputXmlFileName);
                }
                catch (Exception e)
                {
                    string message = string.Format(
                            "Cannot write envoTermsResponse XML document to file '{0}'",
                            this.Config.EnvoResponseOutputXmlFileName);

                    this.logger?.Log(e, message);
                }

                envoTermsTagSet.LoadXml(
                    envoTermsResponse
                        .ApplyXslTransform(this.Config.EnvoTermsWebServiceTransformXslPath));
            }

            {
                XmlNodeList nodeList = this.XmlDocument.SelectNodes("/*", this.NamespaceManager);
                envoTermsTagSet.DocumentElement.ChildNodes.TagContentInDocument(nodeList, false, true, this.logger);
            }
        }
    }
}
