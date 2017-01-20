namespace ProcessingTools.Tagger.Core.Commands
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Attributes;
    using ProcessingTools.Constants;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.References;

    [Description("Tag references.")]
    public class TagReferencesCommand : ITagReferencesCommand
    {
        private readonly IReferencesTagger tagger;

        public TagReferencesCommand(IReferencesTagger tagger)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
        }

        public Task<object> Run(IDocument document, IProgramSettings settings)
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

            return this.tagger.Tag(document.XmlDocument.DocumentElement);
        }

        private void SetReferencesOutputFileName(IDocument document, IProgramSettings settings)
        {
            string articleId = document.SelectSingleNode(XPathStrings.ArticleIdOfTypeDoi)?.InnerText ?? string.Empty;

            string referencesFileName = articleId.ToLower()
                .RegexReplace(@"\A.*/", string.Empty)
                .RegexReplace(@"\W+", "-")
                .Trim(new char[] { ' ', '-' });

            if (string.IsNullOrWhiteSpace(referencesFileName))
            {
                referencesFileName = Guid.NewGuid().ToString();
            }

            var outputDirectoryName = Path.GetDirectoryName(settings.OutputFileName);
            this.tagger.ReferencesGetReferencesXmlPath = Path.Combine(outputDirectoryName, $"{referencesFileName}-references.{FileConstants.XmlFileExtension}");
        }
    }
}
