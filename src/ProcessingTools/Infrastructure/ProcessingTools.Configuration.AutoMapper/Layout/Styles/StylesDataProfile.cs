// <copyright file="StylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;
    using ProcessingTools.Contracts.Models.Layout.Styles;
    using ProcessingTools.Services.Models.Layout.Styles;

    /// <summary>
    /// Styles data profile.
    /// </summary>
    public class StylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StylesDataProfile"/> class.
        /// </summary>
        public StylesDataProfile()
        {
            this.CreateMap<IIdentifiedStyleDataTransferObject, StyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IIdentifiedStyleDataTransferObject, IIdentifiedStyleModel>().As<StyleModel>();
        }
    }
}
