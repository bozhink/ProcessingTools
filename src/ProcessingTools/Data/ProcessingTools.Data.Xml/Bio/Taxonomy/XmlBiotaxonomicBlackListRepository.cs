namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class XmlBiotaxonomicBlackListRepository : FileRepository<IXmlBlackListContext, IBlackListItem>, IXmlBiotaxonomicBlackListRepository
    {
        private readonly string dataFileName;

        public XmlBiotaxonomicBlackListRepository(string dataFileName, IFactory<IXmlBlackListContext> contextFactory)
            : base(contextFactory)
        {
            if (string.IsNullOrWhiteSpace(dataFileName))
            {
                throw new ArgumentNullException(nameof(dataFileName));
            }

            this.dataFileName = dataFileName;

            this.Context.LoadFromFile(this.dataFileName);
        }

        public override async Task<object> SaveChangesAsync() => await this.Context.WriteToFileAsync(this.dataFileName).ConfigureAwait(false);
    }
}
