namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;

    using ProcessingTools.Configurator;

    public class XmlBiotaxonomicBlackListContext : IXmlBiotaxonomicBlackListContext
    {
        private const string ItemNodeName = "item";
        private const string RootNodeName = "items";
        private const int MillisecondsToUpdate = 500;
        private DateTime? lastUpdated;

        public XmlBiotaxonomicBlackListContext(Config config)
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

        public Task<object> Add(string entity)
        {
            if (string.IsNullOrWhiteSpace(entity))
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return Task.Run<object>(() =>
            {
                if (!string.IsNullOrWhiteSpace(entity))
                {
                    this.Items.Enqueue(entity);
                }

                return entity;
            });
        }

        public async Task<IQueryable<string>> All()
        {
            await this.ReadItemsFromFile();
            return new HashSet<string>(this.Items).AsQueryable();
        }

        public async Task<object> Delete(string entity)
        {
            if (string.IsNullOrWhiteSpace(entity))
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var items = (await this.All()).ToList();
            items.Remove(entity);
            this.Items = new ConcurrentQueue<string>(items);

            return entity;
        }

        public async Task<long> WriteItemsToFile()
        {
            var items = (await this.All())
                .Select(item => new XElement(ItemNodeName, item))
                .ToArray();

            XElement list = new XElement(RootNodeName, items);

            list.Save(this.Config.BlackListXmlFilePath, SaveOptions.DisableFormatting);

            return items.Length;
        }

        private Task ReadItemsFromFile() => Task.Run(() =>
                {
                    var timeSpan = this.lastUpdated - DateTime.Now;
                    if (timeSpan.HasValue &&
                        timeSpan.Value.Milliseconds < MillisecondsToUpdate)
                    {
                        return;
                    }

                    XElement.Load(this.Config.BlackListXmlFilePath)
                        .Descendants(ItemNodeName)
                        .AsParallel()
                        .ForAll(element => this.Items.Enqueue(element.Value));

                    this.lastUpdated = DateTime.Now;
                });
    }
}
