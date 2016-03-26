namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using ProcessingTools.Contracts;

    public abstract class TaggerControllerFactory : ITaggerController
    {
        public async Task Run(XmlNode context, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (namespaceManager == null)
            {
                throw new ArgumentNullException("namespaceManager");
            }

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            try
            {
                XmlDocument document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                document.LoadXml(Resources.ContextWrapper);
                document.DocumentElement.InnerXml = context.InnerXml;

                await this.Run(document, namespaceManager, settings, logger);

                context.InnerXml = document.DocumentElement.InnerXml;
            }
            catch (Exception e)
            {
                logger?.Log(e, string.Empty);
            }
        }

        protected abstract Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger);
    }
}
