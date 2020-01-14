// <copyright file="TemplatesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Templates
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates;
    using ProcessingTools.Contracts.Models.Layout.Templates;
    using ProcessingTools.Services.Models.Layout.Templates;

    /// <summary>
    /// Templates data profile.
    /// </summary>
    public class TemplatesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatesDataProfile"/> class.
        /// </summary>
        public TemplatesDataProfile()
        {
            this.CreateMap<IIdentifiedTemplateMetaDataTransferObject, TemplateMetaModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IIdentifiedTemplateMetaDataTransferObject, IIdentifiedTemplateMetaModel>().As<TemplateMetaModel>();
        }
    }
}
