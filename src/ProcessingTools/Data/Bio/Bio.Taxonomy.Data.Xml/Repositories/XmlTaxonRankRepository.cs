﻿namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts.Repositories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlTaxonRankRepository : FileGenericRepository<IXmlTaxaContext, ITaxonRankEntity>, IXmlTaxonRankRepository
    {
        private readonly string dataFileName;

        public XmlTaxonRankRepository(string dataFileName, IFactory<IXmlTaxaContext> contextFactory)
            : base(contextFactory)
        {
            if (string.IsNullOrWhiteSpace(dataFileName))
            {
                throw new ArgumentNullException(nameof(dataFileName));
            }

            this.dataFileName = dataFileName;

            this.Context.LoadFromFile(this.dataFileName).Wait();
        }

        public override object SaveChanges() => this.Context.WriteToFile(this.dataFileName).Result;

        public override async Task<object> SaveChangesAsync() => await this.Context.WriteToFile(this.dataFileName);
    }
}
