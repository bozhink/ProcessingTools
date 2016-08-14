namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;

    public class XmlBiotaxonomicBlackListIterableRepository : IXmlBiotaxonomicBlackListIterableRepository
    {
        private readonly IXmlBiotaxonomicBlackListContext context;

        public XmlBiotaxonomicBlackListIterableRepository(IXmlBiotaxonomicBlackListContextProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.context = provider.Create();
        }

        public virtual async Task<IQueryable<IBlackListEntity>> All()
        {
            var query = await this.context.All();

            return query.Select(s => new BlackListEntity
            {
                Content = s
            });
        }
    }
}
