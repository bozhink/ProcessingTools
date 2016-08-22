namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;

    public class XmlBiotaxonomicBlackListIterableRepository : IXmlBiotaxonomicBlackListIterableRepository
    {
        private readonly IXmlBiotaxonomicBlackListContext context;

        public XmlBiotaxonomicBlackListIterableRepository(IXmlBiotaxonomicBlackListContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.context = contextProvider.Create();
        }

        public IEnumerable<IBlackListEntity> Entities => this.context.All().Result.ToList();
    }
}
