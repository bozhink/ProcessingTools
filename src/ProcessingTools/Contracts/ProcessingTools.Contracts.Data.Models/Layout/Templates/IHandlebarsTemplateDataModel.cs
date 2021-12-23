// <copyright file="IHandlebarsTemplateDataModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Models.Layout.Templates
{
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars template data model.
    /// </summary>
    public interface IHandlebarsTemplateDataModel : IDataModel, IHandlebarsTemplateBaseModel
    {
    }
}
