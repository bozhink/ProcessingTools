namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Contracts;

    public class TaxonomicListDataService : ITaxonomicListDataService<string>
    {
        private ICollection<string> dataList;

        public TaxonomicListDataService(string listFilePath)
        {
            if (string.IsNullOrWhiteSpace(listFilePath))
            {
                throw new ArgumentNullException("listFilePath");
            }

            this.dataList = new HashSet<string>(XDocument.Load(listFilePath)
                .Descendants()
                .Select(item => item.Value));
        }

        public IQueryable<string> All()
        {
            return this.dataList.AsQueryable();
        }
    }
}