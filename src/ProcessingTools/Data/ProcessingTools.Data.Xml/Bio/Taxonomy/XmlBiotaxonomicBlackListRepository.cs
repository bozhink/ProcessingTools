﻿namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class XmlBiotaxonomicBlackListRepository : FileRepository<IXmlBiotaxonomicBlackListContext, IBlackListItem>, IXmlBiotaxonomicBlackListRepository
    {
        private readonly string dataFileName;

        public XmlBiotaxonomicBlackListRepository(string dataFileName, IFactory<IXmlBiotaxonomicBlackListContext> contextFactory)
            : base(contextFactory)
        {
            if (string.IsNullOrWhiteSpace(dataFileName))
            {
                throw new ArgumentNullException(nameof(dataFileName));
            }

            this.dataFileName = dataFileName;

            this.Context.LoadFromFileAsync(this.dataFileName).Wait();
        }

        public override async Task<object> SaveChangesAsync() => await this.Context.WriteToFileAsync(this.dataFileName).ConfigureAwait(false);

        public override Task<object> UpdateAsync(IBlackListItem entity) => this.AddAsync(entity);
    }
}
