// <copyright file="HandlebarsTemplatesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Templates
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Data.Models.Mongo.Layout.Templates;
    using ProcessingTools.DataAccess.Models.Mongo.Layout.Templates.Handlebars;
    using ProcessingTools.Services.Models.Layout.Templates.Handlebars;

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

            // Data Access - Data Services
            this.CreateMap<IHandlebarsTemplateDataTransferObject, HandlebarsTemplateModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IHandlebarsTemplateDataTransferObject, IHandlebarsTemplateModel>().As<HandlebarsTemplateModel>();
            this.CreateMap<IHandlebarsTemplateDetailsDataTransferObject, HandlebarsTemplateDetailsModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IHandlebarsTemplateDetailsDataTransferObject, IHandlebarsTemplateDetailsModel>().As<HandlebarsTemplateDetailsModel>();
        }
    }
}
