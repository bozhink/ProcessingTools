namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.References;

    [System.ComponentModel.Description("Tag references.")]
    public class TagReferencesCommand : ITagReferencesCommand
    {
        private readonly IReferencesTagger tagger;

        public TagReferencesCommand(IReferencesTagger tagger)
        {
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
        }

        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            this.SetReferencesOutputFileName(document, settings);

            return this.tagger.TagAsync(document.XmlDocument.DocumentElement);
        }

        private void SetReferencesOutputFileName(IDocument document, ICommandSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.OutputFileName))
            {
                return;
            }

            string referencesFileName = document.GenerateFileNameFromDocumentId();

            var outputDirectoryName = Path.GetDirectoryName(settings.OutputFileName);
            this.tagger.ReferencesGetReferencesXmlPath = Path.Combine(outputDirectoryName, $"{referencesFileName}-references.{FileConstants.XmlFileExtension}");
        }
    }
}
