namespace ProcessingTools.MainProgram.Factories
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using ProcessingTools.Contracts;

    public abstract class TaggerControllerFactory : ITaggerController
    {
        public async Task Run(XmlNode context, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
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

        protected abstract Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger);
    }
}
