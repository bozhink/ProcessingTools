namespace ProcessingTools.Data.Bio.Taxonomy.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using ProcessingTools.Data.Bio.Taxonomy.Xml.Contracts;
    using ProcessingTools.Data.Models.Bio.Taxonomy.Xml;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class XmlBiotaxonomicBlackListContext : IXmlBiotaxonomicBlackListContext
    {
        private const string ItemNodeName = "item";
        private const string RootNodeName = "items";
        private const int MillisecondsToUpdate = 500;
        private DateTime? lastUpdated;

        public XmlBiotaxonomicBlackListContext()
        {
            this.Items = new ConcurrentQueue<IBlackListItem>();
        }

        public IQueryable<IBlackListItem> DataSet => new HashSet<IBlackListItem>(this.Items).AsQueryable();

        private ConcurrentQueue<IBlackListItem> Items { get; set; }

        public async Task<object> AddAsync(IBlackListItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!string.IsNullOrWhiteSpace(entity.Content))
            {
                this.Items.Enqueue(entity);
            }

            return await Task.FromResult(entity).ConfigureAwait(false);
        }

        public Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = new BlackListEntity
            {
                Content = id.ToString()
            };

            return this.Delete(entity);
        }

        public Task<IBlackListItem> GetAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return Task.Run(() => this.DataSet.FirstOrDefault(e => e.Content == id.ToString()));
        }

        public async Task<long> LoadFromFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return await Task.Run(() =>
            {
                var timeSpan = this.lastUpdated - DateTime.Now;
                if (timeSpan.HasValue &&
                    timeSpan.Value.Milliseconds < MillisecondsToUpdate)
                {
                    return -1L;
                }

                XElement.Load(fileName)
                    .Descendants(ItemNodeName)
                    .AsParallel()
                    .ForAll(element => this.Items.Enqueue(new BlackListEntity
                    {
                        Content = element.Value
                    }));

                this.lastUpdated = DateTime.Now;

                return this.Items.Count;
            })
            .ConfigureAwait(false);
        }

        public Task<object> UpdateAsync(IBlackListItem entity) => this.AddAsync(entity);

        public async Task<long> WriteToFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            await this.LoadFromFileAsync(fileName).ConfigureAwait(false);

            var comparer = new BlackListEntityEqualityComparer();

            var items = this.DataSet
                .Distinct(comparer)
                .Select(item => new XElement(ItemNodeName, item.Content))
                .ToArray();

            XElement list = new XElement(RootNodeName, items);

            list.Save(fileName, SaveOptions.DisableFormatting);

            return (long)items.Length;
        }

        private Task<object> Delete(IBlackListItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                var items = this.DataSet.ToList();
                items.Remove(entity);
                this.Items = new ConcurrentQueue<IBlackListItem>(items);

                return entity;
            });
        }
    }
}
