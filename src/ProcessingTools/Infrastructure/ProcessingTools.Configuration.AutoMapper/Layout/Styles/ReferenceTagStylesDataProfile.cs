// <copyright file="ReferenceTagStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.DataAccess.Models.Mongo.Layout.Styles.References;
    using ProcessingTools.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles data profile.
    /// </summary>
    public class ReferenceTagStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesDataProfile"/> class.
        /// </summary>
        public ReferenceTagStylesDataProfile()
        {
            // Data - Data Access
            this.CreateMap<IReferenceInsertTagStyleModel, ReferenceTagStyle>();
            this.CreateMap<IReferenceUpdateTagStyleModel, ReferenceTagStyle>();
            this.CreateMap<ReferenceTagStyle, ReferenceTagStyleDataTransferObject>();
            this.CreateMap<ReferenceTagStyle, IReferenceTagStyleDataTransferObject>().As<ReferenceTagStyleDataTransferObject>();
            this.CreateMap<ReferenceTagStyle, ReferenceTagStyleDetailsDataTransferObject>();
            this.CreateMap<ReferenceTagStyle, IReferenceDetailsTagStyleDataTransferObject>().As<ReferenceTagStyleDetailsDataTransferObject>();

            // Data Access - Data Services
            this.CreateMap<IReferenceTagStyleDataTransferObject, ReferenceTagStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IReferenceTagStyleDataTransferObject, IReferenceTagStyleModel>().As<ReferenceTagStyleModel>();
            this.CreateMap<IReferenceDetailsTagStyleDataTransferObject, ReferenceDetailsTagStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IReferenceDetailsTagStyleDataTransferObject, IReferenceDetailsTagStyleModel>().As<ReferenceDetailsTagStyleModel>();
        }
    }
}
