namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Common.Types;
    using ProcessingTools.Configurator;

    public class TaxonomicBlackListRepository : ITaxonomicBlackListRepository
    {
        private const string ItemNodeName = "item";
        private const string RootNodeName = "items";
        private const int MillisecondsToUpdate = 500;
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

        public virtual async Task<long> Count()
        {
            var count = (await this.All())
                .LongCount();

            return count;
        }

        public virtual async Task<long> Count(Expression<Func<string, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = (await this.All())
                .Where(filter)
                .LongCount();

            return count;
        }

        public Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Delete(id.ToString());
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

        public async Task<string> Get(Expression<Func<string, bool>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return (await this.All())
                .FirstOrDefault(selector);
        }

        public async Task<string> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return (await this.All())
                .FirstOrDefault(i => i == id.ToString());
        }

        public virtual async Task<IQueryable<string>> Find(
            Expression<Func<string, bool>> filter,
            Expression<Func<string, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (sort == null)
            {
                throw new ArgumentNullException(nameof(sort));
            }

            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }

            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }

            var query = await this.All();

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    query = query.OrderBy(sort);
                    break;

                case SortOrder.Descending:
                    query = query.OrderByDescending(sort);
                    break;

                default:
                    throw new NotImplementedException();
            }

            query = query.Skip(skip).Take(take);

            return query;
        }

        public virtual async Task<IQueryable<T>> Find<T>(
            Expression<Func<string, bool>> filter,
            Expression<Func<string, T>> projection,
            Expression<Func<string, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            return (await this.Find(filter, sort, sortOrder, skip, take))
                .Select(projection);
        }

        public virtual async Task<string> FindFirst(Expression<Func<string, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var entity = (await this.All())
                .FirstOrDefault(filter);

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public virtual async Task<T> FindFirst<T>(Expression<Func<string, bool>> filter, Expression<Func<string, T>> projection)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var entity = (await this.All())
                .Where(filter)
                .Select(projection)
                .FirstOrDefault();

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public Task<object> Update(string entity)
        {
            return this.Add(entity);
        }

        public Task<int> SaveChanges() => this.WriteItemsToFile();

        private Task ReadItemsFromFile()
        {
            return Task.Run(() =>
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