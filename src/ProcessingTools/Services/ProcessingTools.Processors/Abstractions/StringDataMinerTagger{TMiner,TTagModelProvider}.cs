// <copyright file="StringDataMinerTagger{TMiner,TTagModelProvider}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Processors.Contracts;

    /// <summary>
    /// Generic string data miner tagger.
    /// </summary>
    /// <typeparam name="TMiner">Type of data miner.</typeparam>
    /// <typeparam name="TTagModelProvider">Type of tag model provider.</typeparam>
    public class StringDataMinerTagger<TMiner, TTagModelProvider> : IDocumentTagger
        where TMiner : IStringDataMiner
        where TTagModelProvider : class, IXmlTagModelProvider
    {
        private readonly IStringDataMinerEvaluator<TMiner> evaluator;
        private readonly IStringTagger tagger;
        private readonly TTagModelProvider tagModelProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringDataMinerTagger{TMiner, TTagModelProvider}"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public StringDataMinerTagger(IStringDataMinerEvaluator<TMiner> evaluator, IStringTagger tagger, TTagModelProvider tagModelProvider)
        {
            this.evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
            this.tagModelProvider = tagModelProvider ?? throw new ArgumentNullException(nameof(tagModelProvider));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var tagModel = this.tagModelProvider.TagModel(context.XmlDocument);

            var data = await this.evaluator.EvaluateAsync(context).ConfigureAwait(false);
            object result = await this.tagger.TagAsync(context, data, tagModel, XPathStrings.ContentNodes).ConfigureAwait(false);
            return result;
        }
    }
}
