// <copyright file="FloatObjectTagStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.DataAccess.Models.Mongo.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles data profile.
    /// </summary>
    public class FloatObjectTagStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStylesDataProfile"/> class.
        /// </summary>
        public FloatObjectTagStylesDataProfile()
        {
            // Data - Data Access
            this.CreateMap<IFloatObjectInsertTagStyleModel, FloatObjectTagStyle>();
            this.CreateMap<IFloatObjectUpdateTagStyleModel, FloatObjectTagStyle>();
            this.CreateMap<FloatObjectTagStyle, FloatObjectTagStyleDataTransferObject>();
            this.CreateMap<FloatObjectTagStyle, IFloatObjectTagStyleDataTransferObject>().As<FloatObjectTagStyleDataTransferObject>();
            this.CreateMap<FloatObjectTagStyle, FloatObjectTagStyleDetailsDataTransferObject>();
            this.CreateMap<FloatObjectTagStyle, IFloatObjectDetailsTagStyleDataTransferObject>().As<FloatObjectTagStyleDetailsDataTransferObject>();

            // Data Access - Data Services
            this.CreateMap<IFloatObjectTagStyleDataTransferObject, FloatObjectTagStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IFloatObjectTagStyleDataTransferObject, IFloatObjectTagStyleModel>().As<FloatObjectTagStyleModel>();
            this.CreateMap<IFloatObjectDetailsTagStyleDataTransferObject, FloatObjectDetailsTagStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IFloatObjectDetailsTagStyleDataTransferObject, IFloatObjectDetailsTagStyleModel>().As<FloatObjectDetailsTagStyleModel>();
        }
    }
}
