// <copyright file="IXmlTaxaContext.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Data.Xml.Abstractions;

    /// <summary>
    /// XML taxa context.
    /// </summary>
    public interface IXmlTaxaContext : IXmlDbContext<ITaxonRankItem>
    {
    }
}
