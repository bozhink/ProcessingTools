namespace ProcessingTools.Processors.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Harvesters.Contracts.Content;

    public class GenericStringDataMinerEvaluator<TMiner> : IGenericStringDataMinerEvaluator<TMiner>
        where TMiner : IStringDataMiner
    {
        private readonly TMiner miner;
        private readonly ITextContentHarvester contentHarvester;

        public GenericStringDataMinerEvaluator(TMiner miner, ITextContentHarvester contentHarvester)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            this.miner = miner;
            this.contentHarvester = contentHarvester;
        }

        public async Task<IEnumerable<string>> Evaluate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var data = await this.miner.Mine(textContent);

            return data;
        }
    }
}
