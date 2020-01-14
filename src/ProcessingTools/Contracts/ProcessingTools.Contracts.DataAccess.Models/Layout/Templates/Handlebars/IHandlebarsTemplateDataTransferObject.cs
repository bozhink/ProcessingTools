// <copyright file="IHandlebarsTemplateDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars
{
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars template data transfer object (DTO).
    /// </summary>
    public interface IHandlebarsTemplateDataTransferObject : IDataTransferObject, IHandlebarsTemplateModel, IIdentifiedTemplateDataTransferObject
    {
    }
}
