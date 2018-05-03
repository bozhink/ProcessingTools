namespace ProcessingTools.Commands.Tagger
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Commands.Models.Contracts;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [System.ComponentModel.Description("Query replace.")]
    public class QueryReplaceCommand : IQueryReplaceCommand
    {
        private readonly IQueryReplacer queryReplacer;

        public QueryReplaceCommand(IQueryReplacer queryReplacer)
        {
            this.queryReplacer = queryReplacer ?? throw new ArgumentNullException(nameof(queryReplacer));
        }

        public async Task<object> RunAsync(IDocument document, ICommandSettings settings)
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
                throw new InvalidOperationException("The query file name should be set.");
            }

            string queryFileName = settings.FileNames[2];

            var processedContent = await this.queryReplacer.ReplaceAsync(document.Xml, queryFileName);

            document.Xml = processedContent;

            return true;
        }
    }
}
