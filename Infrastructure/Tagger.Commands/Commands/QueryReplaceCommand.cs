namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors;

    [Description("Query replace.")]
    public class QueryReplaceCommand : IQueryReplaceCommand
    {
        private readonly IQueryReplacer queryReplacer;

        public QueryReplaceCommand(IQueryReplacer queryReplacer)
        {
            if (queryReplacer == null)
            {
                throw new ArgumentNullException(nameof(queryReplacer));
            }

            this.queryReplacer = queryReplacer;
        }

        public async Task<object> Run(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            int numberOfFileNames = settings.FileNames.Count;
            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The query file name should be set.");
            }

            string queryFileName = settings.FileNames[2];

            var processedContent = await this.queryReplacer.Replace(document.Xml, queryFileName);

            document.Xml = processedContent;

            return true;
        }
    }
}
