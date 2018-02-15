namespace ProcessingTools.Processors.Generics
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Contracts.Processors.Providers;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Layout;

    public class GenericStringMinerTagger<TMiner, TTagModelProvider> : IDocumentTagger
        where TMiner : IStringDataMiner
        where TTagModelProvider : class, IXmlTagModelProvider
    {
        private readonly IStringDataMinerEvaluator<TMiner> evaluator;
        private readonly IStringTagger tagger;
        private readonly TTagModelProvider tagModelProvider;

        public GenericStringMinerTagger(
            IStringDataMinerEvaluator<TMiner> evaluator,
            IStringTagger tagger,
            TTagModelProvider tagModelProvider)
        {
            this.evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
            this.tagModelProvider = tagModelProvider ?? throw new ArgumentNullException(nameof(tagModelProvider));
        }

        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var tagModel = this.tagModelProvider.TagModel(context.XmlDocument);

            var data = await this.evaluator.Evaluate(context).ConfigureAwait(false);

            return await this.tagger.Tag(context, data, tagModel, XPathStrings.ContentNodes).ConfigureAwait(false);
        }
    }
}
