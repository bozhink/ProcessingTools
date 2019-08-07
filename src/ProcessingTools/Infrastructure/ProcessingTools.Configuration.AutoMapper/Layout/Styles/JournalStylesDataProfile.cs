// <copyright file="JournalStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.References;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;
    using ProcessingTools.Services.Models.Layout.Styles.Floats;
    using ProcessingTools.Services.Models.Layout.Styles.Journals;
    using ProcessingTools.Services.Models.Layout.Styles.References;

    /// <summary>
    /// Journal styles data profile.
    /// </summary>
    public class JournalStylesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStylesDataProfile"/> class.
        /// </summary>
        public JournalStylesDataProfile()
        {
            // Data - Data Access
            this.CreateMap<IJournalInsertStyleModel, ProcessingTools.Data.Models.Mongo.Layout.JournalStyle>();
            this.CreateMap<IJournalUpdateStyleModel, ProcessingTools.Data.Models.Mongo.Layout.JournalStyle>();

            // Data Access - Data Services
            this.CreateMap<IJournalStyleDataTransferObject, JournalStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IJournalStyleDataTransferObject, IJournalStyleModel>().As<JournalStyleModel>();
            this.CreateMap<IJournalDetailsStyleDataTransferObject, JournalDetailsStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IJournalDetailsStyleDataTransferObject, IJournalDetailsStyleModel>().As<JournalDetailsStyleModel>();

            this.CreateMap<IFloatObjectParseStyleDataTransferObject, FloatObjectParseStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IFloatObjectParseStyleDataTransferObject, IFloatObjectParseStyleModel>().As<FloatObjectParseStyleModel>();

            this.CreateMap<IFloatObjectTagStyleDataTransferObject, FloatObjectTagStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IFloatObjectTagStyleDataTransferObject, IFloatObjectTagStyleModel>().As<FloatObjectTagStyleModel>();

            this.CreateMap<IReferenceParseStyleDataTransferObject, ReferenceParseStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IReferenceParseStyleDataTransferObject, IReferenceParseStyleModel>().As<ReferenceParseStyleModel>();

            this.CreateMap<IReferenceTagStyleDataTransferObject, ReferenceTagStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IReferenceTagStyleDataTransferObject, IReferenceTagStyleModel>().As<ReferenceTagStyleModel>();
        }
    }
}
