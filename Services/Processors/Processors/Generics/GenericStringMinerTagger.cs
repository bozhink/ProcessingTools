namespace ProcessingTools.Processors.Generics
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Contracts.Providers;

    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GenericStringMinerTagger<TMiner, TTagModelProvider> : IDocumentTagger
        where TMiner : IStringDataMiner
        where TTagModelProvider : IXmlTagModelProvider
    {
        private readonly IGenericStringDataMinerEvaluator<TMiner> evaluator;
        private readonly IStringTagger tagger;
        private readonly TTagModelProvider tagModelProvider;

        public GenericStringMinerTagger(
            IGenericStringDataMinerEvaluator<TMiner> evaluator,
            IStringTagger tagger,
            TTagModelProvider tagModelProvider)
        {
            if (evaluator == null)
            {
                throw new ArgumentNullException(nameof(evaluator));
            }

            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            if (tagModelProvider == null)
            {
                throw new ArgumentNullException(nameof(tagModelProvider));
            }

            this.evaluator = evaluator;
            this.tagger = tagger;
            this.tagModelProvider = tagModelProvider;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var tagModel = this.tagModelProvider.TagModel(document.XmlDocument);

            var data = await this.evaluator.Evaluate(document);

            return await this.tagger.Tag(document, data, tagModel, XPathStrings.ContentNodes);
        }
    }
}