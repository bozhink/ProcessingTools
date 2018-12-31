// <copyright file="IXmlTaxaContext.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// XML taxa context.
    /// </summary>
    public interface IXmlTaxaContext : IXmlDbContext<ITaxonRankItem>
    {
    }
}
