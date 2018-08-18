namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts.Repositories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.File.Repositories;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class XmlBiotaxonomicBlackListRepository : FileGenericRepository<IXmlBiotaxonomicBlackListContext, IBlackListItem>, IXmlBiotaxonomicBlackListRepository
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

        public override object SaveChanges() => this.Context.WriteToFileAsync(this.dataFileName).Result;

        public override async Task<object> SaveChangesAsync() => await this.Context.WriteToFileAsync(this.dataFileName).ConfigureAwait(false);

        public override Task<object> UpdateAsync(IBlackListItem entity) => this.AddAsync(entity);
    }
}
