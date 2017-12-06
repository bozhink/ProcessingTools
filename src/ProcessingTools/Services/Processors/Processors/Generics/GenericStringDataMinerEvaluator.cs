namespace ProcessingTools.Processors.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;

    public class GenericStringDataMinerEvaluator<TMiner> : IGenericStringDataMinerEvaluator<TMiner>
        where TMiner : class, IStringDataMiner
    {
        private readonly TMiner miner;
        private readonly ITextContentHarvester contentHarvester;

        public GenericStringDataMinerEvaluator(TMiner miner, ITextContentHarvester contentHarvester)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
        }

        public async Task<IEnumerable<string>> Evaluate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.HarvestAsync(document.XmlDocument.DocumentElement);
            var data = await this.miner.MineAsync(textContent).ConfigureAwait(false);

            return data;
        }
    }
}
