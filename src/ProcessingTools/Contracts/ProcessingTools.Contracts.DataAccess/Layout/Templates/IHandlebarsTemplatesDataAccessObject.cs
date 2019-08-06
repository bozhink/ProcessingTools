// <copyright file="IHandlebarsTemplatesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Templates
{
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars templates data access object (DAO).
    /// </summary>
    public interface IHandlebarsTemplatesDataAccessObject : ITemplatesDataAccessObject, IDataAccessObject<IHandlebarsTemplateDataTransferObject, IHandlebarsTemplateDetailsDataTransferObject, IHandlebarsTemplateInsertModel, IHandlebarsTemplateUpdateModel>
    {
    }
}
