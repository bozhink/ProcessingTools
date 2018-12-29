// <copyright file="IXmlBlackListContext.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// XML blacklist context.
    /// </summary>
    public interface IXmlBlackListContext : IXmlDbContext<IBlackListItem>
    {
    }
}
