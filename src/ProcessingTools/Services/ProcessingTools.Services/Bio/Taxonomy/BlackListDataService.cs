// <copyright file="BlackListDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Taxonomic black list data service.
    /// </summary>
    public class BlackListDataService : IBlackListDataService
    {
        private readonly IBlackListDataAccessObject dataAccessObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlackListDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        public BlackListDataService(IBlackListDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
        }

        /// <inheritdoc/>
        public async Task<object> InsertAsync(IEnumerable<string> items)
        {
            var validItems = this.ValidateInputItems(items);

            await this.dataAccessObject.InsertManyAsync(validItems).ConfigureAwait(false);
            return await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(IEnumerable<string> items)
        {
            var validItems = this.ValidateInputItems(items);

            await this.dataAccessObject.DeleteManyAsync(validItems).ConfigureAwait(false);
            return await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<string>> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Array.Empty<string>();
            }

            var data = await this.dataAccessObject.FindAsync(filter).ConfigureAwait(false);
            return data;
        }

        private IList<string> ValidateInputItems(IEnumerable<string> items)
        {
            if (items == null || !items.Any())
            {
                throw new ArgumentNullException(nameof(items));
            }

            var validItems = items.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

            if (validItems.Length < 1)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return validItems;
        }
    }
}
