// <copyright file="StringDataMinerEvaluator{TMiner}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Content;

    /// <summary>
    /// Generic string data miner evaluator.
    /// </summary>
    /// <typeparam name="TMiner">Type of string data miner.</typeparam>
    public class StringDataMinerEvaluator<TMiner> : IStringDataMinerEvaluator<TMiner>
        where TMiner : class, IStringDataMiner
    {
        private readonly TMiner miner;
        private readonly ITextContentHarvester contentHarvester;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringDataMinerEvaluator{TMiner}"/> class.
        /// </summary>
        /// <param name="miner">String data miner.</param>
        /// <param name="contentHarvester">Text content harvester.</param>
        public StringDataMinerEvaluator(TMiner miner, ITextContentHarvester contentHarvester)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
        }

        /// <inheritdoc/>
        public Task<IList<string>> EvaluateAsync(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return this.EvaluateInternalAsync(document);
        }

        private async Task<IList<string>> EvaluateInternalAsync(IDocument document)
        {
            var textContent = await this.contentHarvester.HarvestAsync(document.XmlDocument.DocumentElement).ConfigureAwait(false);
            var data = await this.miner.MineAsync(textContent).ConfigureAwait(false);

            return data;
        }
    }
}
