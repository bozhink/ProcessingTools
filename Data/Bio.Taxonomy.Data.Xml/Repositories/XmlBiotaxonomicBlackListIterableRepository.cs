namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;

    public class XmlBiotaxonomicBlackListIterableRepository : IXmlBiotaxonomicBlackListIterableRepository
    {
        public XmlBiotaxonomicBlackListIterableRepository(IXmlBiotaxonomicBlackListContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.Context = contextProvider.Create();
        }

        public IEnumerable<IBlackListEntity> Entities => this.Context.DataSet.ToList();

        private IXmlBiotaxonomicBlackListContext Context { get; set; }
    }
}
