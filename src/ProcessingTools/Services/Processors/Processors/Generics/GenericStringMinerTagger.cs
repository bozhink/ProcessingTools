namespace ProcessingTools.Processors.Generics
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Providers;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;

    public class GenericStringMinerTagger<TMiner, TTagModelProvider> : IDocumentTagger
        where TMiner : IStringDataMiner
        where TTagModelProvider : class, IXmlTagModelProvider
    {
        private readonly IGenericStringDataMinerEvaluator<TMiner> evaluator;
        private readonly IStringTagger tagger;
        private readonly TTagModelProvider tagModelProvider;

        public GenericStringMinerTagger(
            IGenericStringDataMinerEvaluator<TMiner> evaluator,
            IStringTagger tagger,
            TTagModelProvider tagModelProvider)
        {
            this.evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
            this.tagModelProvider = tagModelProvider ?? throw new ArgumentNullException(nameof(tagModelProvider));
        }

        public async Task<object> Tag(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var tagModel = this.tagModelProvider.TagModel(context.XmlDocument);

            var data = await this.evaluator.Evaluate(context);

            return await this.tagger.Tag(context, data, tagModel, XPathStrings.ContentNodes);
        }
    }
}
