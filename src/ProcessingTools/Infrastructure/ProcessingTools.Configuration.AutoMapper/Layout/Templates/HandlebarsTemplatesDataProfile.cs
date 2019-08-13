// <copyright file="HandlebarsTemplatesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Templates
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Data.Models.Mongo.Layout.Templates;
    using ProcessingTools.DataAccess.Models.Mongo.Layout.Templates.Handlebars;

    /// <summary>
    /// Handlebars templates data profile.
    /// </summary>
    public class HandlebarsTemplatesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HandlebarsTemplatesDataProfile"/> class.
        /// </summary>
        public HandlebarsTemplatesDataProfile()
        {
            // Data - Data Access
            this.CreateMap<IHandlebarsTemplateInsertModel, HandlebarsTemplate>();
            this.CreateMap<IHandlebarsTemplateUpdateModel, HandlebarsTemplate>();
            this.CreateMap<HandlebarsTemplate, HandlebarsTemplateDataTransferObject>();
            this.CreateMap<HandlebarsTemplate, IHandlebarsTemplateDataTransferObject>().As<HandlebarsTemplateDataTransferObject>();
            this.CreateMap<HandlebarsTemplate, HandlebarsTemplateDetailsDataTransferObject>();
            this.CreateMap<HandlebarsTemplate, IHandlebarsTemplateDetailsDataTransferObject>().As<HandlebarsTemplateDetailsDataTransferObject>();
            this.CreateMap<HandlebarsTemplate, HandlebarsTemplateMetaDataTransferObject>();
            this.CreateMap<HandlebarsTemplate, IIdentifiedTemplateMetaDataTransferObject>().As<HandlebarsTemplateMetaDataTransferObject>();
        }
    }
}
