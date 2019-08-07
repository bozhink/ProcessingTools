// <copyright file="FloatObjectParseStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles data profile.
    /// </summary>
    public class FloatObjectParseStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStylesDataProfile"/> class.
        /// </summary>
        public FloatObjectParseStylesDataProfile()
        {
            // Data - Data Access
            this.CreateMap<IFloatObjectInsertParseStyleModel, ProcessingTools.Data.Models.Mongo.Layout.FloatObjectParseStyle>();
            this.CreateMap<IFloatObjectUpdateParseStyleModel, ProcessingTools.Data.Models.Mongo.Layout.FloatObjectParseStyle>();

            // Data Access - Data Services
            this.CreateMap<IFloatObjectParseStyleDataTransferObject, FloatObjectParseStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IFloatObjectParseStyleDataTransferObject, IFloatObjectParseStyleModel>().As<FloatObjectParseStyleModel>();
            this.CreateMap<IFloatObjectDetailsParseStyleDataTransferObject, FloatObjectDetailsParseStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IFloatObjectDetailsParseStyleDataTransferObject, IFloatObjectDetailsParseStyleModel>().As<FloatObjectDetailsParseStyleModel>();
        }
    }
}
