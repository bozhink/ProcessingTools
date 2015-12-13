namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Contracts;

    public class XmlListDataService : IRepositoryDataService<string>
    {
        private string listFilePath;
        private ICollection<string> dataList;

        public XmlListDataService(string listFilePath)
        {
            this.ListFilePath = listFilePath;

            this.dataList = new HashSet<string>(XDocument.Load(this.ListFilePath)
                .Descendants()
                .Select(item => item.Value));
        }

        private string ListFilePath
        {
            get
            {
                return this.listFilePath;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("ListFilePath");
                }

                this.listFilePath = value;
            }
        }

        public IQueryable<string> All()
        {
            return this.dataList.AsQueryable();
        }
    }
}