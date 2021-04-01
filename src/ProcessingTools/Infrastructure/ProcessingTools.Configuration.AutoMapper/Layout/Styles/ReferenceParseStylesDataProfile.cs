// <copyright file="ReferenceParseStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
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
    /// Reference parse styles data profile.
    /// </summary>
    public class ReferenceParseStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesDataProfile"/> class.
        /// </summary>
        public ReferenceParseStylesDataProfile()
        {
            // Data - Data Access
            this.CreateMap<IReferenceInsertParseStyleModel, ReferenceParseStyle>();
            this.CreateMap<IReferenceUpdateParseStyleModel, ReferenceParseStyle>();
            this.CreateMap<ReferenceParseStyle, ReferenceParseStyleDataTransferObject>();
            this.CreateMap<ReferenceParseStyle, IReferenceParseStyleDataTransferObject>().As<ReferenceParseStyleDataTransferObject>();
            this.CreateMap<ReferenceParseStyle, ReferenceParseStyleDetailsDataTransferObject>();
            this.CreateMap<ReferenceParseStyle, IReferenceDetailsParseStyleDataTransferObject>().As<ReferenceParseStyleDetailsDataTransferObject>();

            // Data Access - Data Services
            this.CreateMap<IReferenceParseStyleDataTransferObject, ReferenceParseStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IReferenceParseStyleDataTransferObject, IReferenceParseStyleModel>().As<ReferenceParseStyleModel>();
            this.CreateMap<IReferenceDetailsParseStyleDataTransferObject, ReferenceDetailsParseStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IReferenceDetailsParseStyleDataTransferObject, IReferenceDetailsParseStyleModel>().As<ReferenceDetailsParseStyleModel>();
        }
    }
}
