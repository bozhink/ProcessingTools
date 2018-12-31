// <copyright file="XmlBlackListDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Models.Xml.Bio.Taxonomy;
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// XML implementation of <see cref="IBlackListDataAccessObject"/>.
    /// </summary>
    public class XmlBlackListDataAccessObject : XmlDataAccessObject<IXmlBlackListContext, IBlackListItem>, IBlackListDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlBlackListDataAccessObject"/> class.
        /// </summary>
        /// <param name="dataFileName">File name of the data XML file.</param>
        /// <param name="context">XML context to be requested.</param>
        public XmlBlackListDataAccessObject(string dataFileName, IXmlBlackListContext context)
            : base(dataFileName, context)
        {
        }

        /// <inheritdoc/>
        public Task<object> InsertManyAsync(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
            {
                throw new ArgumentNullException(nameof(items));
            }

            long count = 0L;
            foreach (var item in items)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    this.Context.Upsert(new BlackListItem
                    {
                        Content = item
                    });

                    count++;
                }
            }

            return Task.FromResult<object>(count);
        }

        /// <inheritdoc/>
        public Task<object> InsertOneAsync(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                this.Context.Upsert(new BlackListItem
                {
                    Content = item
                });

                return Task.FromResult<object>(item);
            }

            return Task.FromResult<object>(null);
        }

        /// <inheritdoc/>
        public Task<object> DeleteManyAsync(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
            {
                throw new ArgumentNullException(nameof(items));
            }

            long count = 0L;
            foreach (var item in items)
            {
                this.Context.Delete(item);

                count++;
            }

            return Task.FromResult<object>(count);
        }

        /// <inheritdoc/>
        public Task<object> DeleteOneAsync(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                this.Context.Delete(item);

                return Task.FromResult<object>(item);
            }

            return Task.FromResult<object>(null);
        }

        /// <inheritdoc/>
        public Task<IList<string>> FindAsync(string filter)
        {
            return Task.Run<IList<string>>(() =>
            {
                if (string.IsNullOrWhiteSpace(filter))
                {
                    return Array.Empty<string>();
                }

                Regex re = new Regex("(?i)" + Regex.Escape(filter), RegexOptions.Compiled);

                return this.Context.DataSet
                    .Where(t => re.IsMatch(t.Content))
                    .Select(i => i.Content)
                    .Distinct()
                    .ToArray();
            });
        }

        /// <inheritdoc/>
        public Task<IList<string>> GetAllAsync()
        {
            return Task.Run<IList<string>>(() => this.Context.DataSet.Select(i => i.Content).Distinct().ToArray());
        }
    }
}
