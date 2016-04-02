namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;

    using ProcessingTools.Configurator;

    public class TaxonomicBlackListRepository : ITaxonomicBlackListRepository
    {
        private const int MillisecondsToUpdate = 500;
        private const string RootNodeName = "items";
        private const string ItemNodeName = "item";

        private DateTime? lastUpdated;

        public TaxonomicBlackListRepository()
            : this(ConfigBuilder.Create())
        {
        }

        public TaxonomicBlackListRepository(Config config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;
            this.Items = new ConcurrentQueue<string>();
        }

        private Config Config { get; set; }

        private ConcurrentQueue<string> Items { get; set; }

        public Task Add(string entity)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrWhiteSpace(entity))
                {
                    this.Items.Enqueue(entity);
                }
            });
        }

        public Task<IQueryable<string>> All()
        {
            this.ReadItemsFromFile();
            return Task.FromResult(new HashSet<string>(this.Items).AsQueryable());
        }

        public async Task<IQueryable<string>> All(int skip, int take)
        {
            return (await this.All())
                .OrderBy(i => i)
                .Skip(skip)
                .Take(take);
        }

        public Task Delete(object id)
        {
            return this.Delete(id.ToString());
        }

        public async Task Delete(string entity)
        {
            var items = (await this.All()).ToList();
            items.Remove(entity);
            this.Items = new ConcurrentQueue<string>(items);
        }

        public async Task<string> Get(object id)
        {
            return (await this.All())
                .FirstOrDefault(i => i == id.ToString());
        }

        public Task<int> SaveChanges()
        {
            return this.WriteItemsToFile();
        }

        public Task Update(string entity)
        {
            return this.Add(entity);
        }

        private void ReadItemsFromFile()
        {
            var timeSpan = this.lastUpdated - DateTime.Now;
            if (timeSpan.HasValue &&
                timeSpan.Value.Milliseconds < MillisecondsToUpdate)
            {
                return;
            }

            XElement list = XElement.Load(this.Config.BlackListXmlFilePath);

            foreach (var element in list.Descendants(ItemNodeName))
            {
                this.Items.Enqueue(element.Value);
            }

            this.lastUpdated = DateTime.Now;
        }

        private async Task<int> WriteItemsToFile()
        {
            var items = (await this.All())
                .Select(item => new XElement(ItemNodeName, item))
                .ToArray();

            XElement list = new XElement(RootNodeName, items);

            list.Save(this.Config.BlackListXmlFilePath, SaveOptions.DisableFormatting);

            return items.Length;
        }
    }
}