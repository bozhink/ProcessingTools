// <copyright file="XmlTaxonRanksDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Contracts.DataAccess.Bio.Taxonomy;
    using ProcessingTools.Data.Xml.Abstractions;

    /// <summary>
    /// XML implementation of <see cref="ITaxonRanksDataAccessObject"/>.
    /// </summary>
    public class XmlTaxonRanksDataAccessObject : XmlDataAccessObject<IXmlTaxaContext, ITaxonRankItem>, ITaxonRanksDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlTaxonRanksDataAccessObject"/> class.
        /// </summary>
        /// <param name="dataFileName">File name of the data XML file.</param>
        /// <param name="context">XML context to be requested.</param>
        public XmlTaxonRanksDataAccessObject(string dataFileName, IXmlTaxaContext context)
            : base(dataFileName, context)
        {
        }

        /// <inheritdoc/>
        public Task<object> UpsertAsync(ITaxonRankItem item) => Task.FromResult(this.Context.Upsert(item));

        /// <inheritdoc/>
        public Task<object> DeleteAsync(string name) => Task.FromResult(this.Context.Delete(name));

        /// <inheritdoc/>
        public Task<IList<ITaxonRankItem>> FindAsync(string filter)
        {
            return Task.Run<IList<ITaxonRankItem>>(() =>
            {
                Regex re = new Regex("(?i)" + Regex.Escape(filter), RegexOptions.Compiled);

                return this.Context.DataSet.Where(t => re.IsMatch(t.Name)).ToArray();
            });
        }

        /// <inheritdoc/>
        public Task<IList<ITaxonRankItem>> FindExactAsync(string filter)
        {
            return Task.Run<IList<ITaxonRankItem>>(() =>
            {
                return this.Context.DataSet.Where(t => t.Name.ToUpperInvariant() == filter.ToUpperInvariant()).ToArray();
            });
        }

        /// <inheritdoc/>
        public Task<IList<ITaxonRankItem>> GetAllAsync()
        {
            return Task.Run<IList<ITaxonRankItem>>(() =>
            {
                return this.Context.DataSet.ToArray();
            });
        }

        /// <inheritdoc/>
        public Task<IList<string>> GetWhiteListedAsync()
        {
            return Task.Run<IList<string>>(() =>
            {
                return this.Context.DataSet
                    .Where(t => t.IsWhiteListed)
                    .Select(t => t.Name)
                    .ToArray();
            });
        }
    }
}
