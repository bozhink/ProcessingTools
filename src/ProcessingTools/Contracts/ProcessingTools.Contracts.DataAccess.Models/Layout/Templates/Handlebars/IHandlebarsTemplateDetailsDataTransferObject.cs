// <copyright file="IHandlebarsTemplateDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars
{
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars template details data transfer object (DTO).
    /// </summary>
    public interface IHandlebarsTemplateDetailsDataTransferObject : IHandlebarsTemplateDataTransferObject, IHandlebarsTemplateDetailsModel
    {
    }
}
