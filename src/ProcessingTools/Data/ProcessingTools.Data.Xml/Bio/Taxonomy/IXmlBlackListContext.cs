// <copyright file="IXmlBlackListContext.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Data.Xml.Abstractions;

    /// <summary>
    /// XML blacklist context.
    /// </summary>
    public interface IXmlBlackListContext : IXmlDbContext<IBlackListItem>
    {
    }
}
