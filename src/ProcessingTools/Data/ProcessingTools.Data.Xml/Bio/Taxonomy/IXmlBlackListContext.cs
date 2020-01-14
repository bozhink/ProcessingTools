// <copyright file="IXmlBlackListContext.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Data.Xml.Abstractions;

    /// <summary>
    /// XML blacklist context.
    /// </summary>
    public interface IXmlBlackListContext : IXmlDbContext<IBlackListItem>
    {
    }
}
