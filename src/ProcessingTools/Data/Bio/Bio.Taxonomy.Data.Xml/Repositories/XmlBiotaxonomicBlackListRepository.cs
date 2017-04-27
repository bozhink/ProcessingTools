namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts.Repositories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlBiotaxonomicBlackListRepository : FileGenericRepository<IXmlBiotaxonomicBlackListContext, IBlackListEntity>, IXmlBiotaxonomicBlackListRepository
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

            this.Context.LoadFromFile(this.dataFileName).Wait();
        }

        public override object SaveChanges() => this.Context.WriteToFile(this.dataFileName).Result;

        public override async Task<object> SaveChangesAsync() => await this.Context.WriteToFile(this.dataFileName);

        public override Task<object> Update(IBlackListEntity entity) => this.Add(entity);
    }
}
