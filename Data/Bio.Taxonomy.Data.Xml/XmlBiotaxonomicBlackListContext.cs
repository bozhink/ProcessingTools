namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Common.Validation;

    public class XmlBiotaxonomicBlackListContext : IXmlBiotaxonomicBlackListContext
    {
        private const string ItemNodeName = "item";
        private const string RootNodeName = "items";
        private const int MillisecondsToUpdate = 500;
        private DateTime? lastUpdated;

        public XmlBiotaxonomicBlackListContext()
        {
            this.Items = new ConcurrentQueue<IBlackListEntity>();
        }

        private ConcurrentQueue<IBlackListEntity> Items { get; set; }

        public IQueryable<IBlackListEntity> DataSet => new HashSet<IBlackListEntity>(this.Items).AsQueryable();

        public Task<object> Add(IBlackListEntity entity) => Task.Run<object>(() =>
        {
            DummyValidator.ValidateEntity(entity);

            if (!string.IsNullOrWhiteSpace(entity.Content))
            {
                this.Items.Enqueue(entity);
            }

            return entity;
        });

        public Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);
            var entity = new BlackListEntity
            {
                Content = id.ToString()
            };

            return this.Delete(entity);
        }

        public Task<IBlackListEntity> Get(object id) => Task.Run(() =>
        {
            DummyValidator.ValidateId(id);
            var entity = this.DataSet.FirstOrDefault(e => e.Content == id.ToString());
            return entity;
        });

        private Task<object> Delete(IBlackListEntity entity) => Task.Run(() =>
        {
            DummyValidator.ValidateEntity(entity);

            var items = this.DataSet.ToList();
            items.Remove(entity);
            this.Items = new ConcurrentQueue<IBlackListEntity>(items);

            return (object)entity;
        });

        public Task<long> LoadFromFile(string fileName) => Task.Run(() =>
        {
            DummyValidator.ValidateFileName(fileName);

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
        });

        public Task<object> Update(IBlackListEntity entity) => this.Add(entity);

        public Task<long> WriteToFile(string fileName) => Task.Run(() =>
        {
            DummyValidator.ValidateFileName(fileName);

            var items = this.DataSet
                .Select(item => new XElement(ItemNodeName, item.Content))
                .ToArray();

            XElement list = new XElement(RootNodeName, items);

            list.Save(fileName, SaveOptions.DisableFormatting);

            return (long)items.Length;
        });
    }
}
