// <copyright file="XmlBlackListContext.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using ProcessingTools.Data.Models.Xml.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// XML blacklist context.
    /// </summary>
    public class XmlBlackListContext : IXmlBlackListContext
    {
        private const string ItemNodeName = "item";
        private const string RootNodeName = "items";
        private const int MillisecondsToUpdate = 500;
        private DateTime? lastUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBlackListContext"/> class.
        /// </summary>
        public XmlBlackListContext()
        {
            this.Items = new ConcurrentQueue<IBlackListItem>();
        }

        /// <inheritdoc/>
        public IEnumerable<IBlackListItem> DataSet => new HashSet<IBlackListItem>(this.Items);

        private ConcurrentQueue<IBlackListItem> Items { get; set; }

        /// <inheritdoc/>
        public object Upsert(IBlackListItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!string.IsNullOrWhiteSpace(entity.Content))
            {
                this.Items.Enqueue(entity);
            }

            return entity;
        }

        /// <inheritdoc/>
        public object Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var items = this.DataSet.ToList();

            var entity = items.FirstOrDefault(i => i.Content == id.ToString());
            if (entity != null)
            {
                items.Remove(entity);
                this.Items = new ConcurrentQueue<IBlackListItem>(items);
            }

            return entity;
        }

        /// <inheritdoc/>
        public IBlackListItem Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.DataSet.FirstOrDefault(e => e.Content == id.ToString());
        }

        /// <inheritdoc/>
        public long LoadFromFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var timeSpan = this.lastUpdated - DateTime.Now;
            if (timeSpan.HasValue &&
                timeSpan.Value.Milliseconds < MillisecondsToUpdate)
            {
                return -1L;
            }

            XElement.Load(fileName)
                .Descendants(ItemNodeName)
                .AsParallel()
                .ForAll(element => this.Items.Enqueue(new BlackListItem
                {
                    Content = element.Value
                }));

            this.lastUpdated = DateTime.Now;

            return this.Items.Count;
        }

        /// <inheritdoc/>
        public Task<long> WriteToFileAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            this.LoadFromFile(fileName);

            var comparer = new BlackListItemEqualityComparer();

            var items = this.DataSet
                .Distinct(comparer)
                .Select(item => new XElement(ItemNodeName, item.Content))
                .ToArray();

            XElement list = new XElement(RootNodeName, items);

            list.Save(fileName, SaveOptions.DisableFormatting);

            return Task.FromResult((long)items.Length);
        }
    }
}
