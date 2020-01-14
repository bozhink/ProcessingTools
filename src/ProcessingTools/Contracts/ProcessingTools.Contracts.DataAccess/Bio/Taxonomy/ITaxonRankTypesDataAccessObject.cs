// <copyright file="ITaxonRankTypesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Bio.Taxonomy
{
    using System.Threading.Tasks;

    /// <summary>
    /// Taxon rank types data access object.
    /// </summary>
    public interface ITaxonRankTypesDataAccessObject : IDataAccessObject
    {
        /// <summary>
        /// Seeds taxon rank types collection from enumeration <see cref="ProcessingTools.Common.Enumerations.TaxonRankType"/>.
        /// </summary>
        /// <returns>Result.</returns>
        Task<object> SeedFromTaxonRankTypeEnumAsync();
    }
}
