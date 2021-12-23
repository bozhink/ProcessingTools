// <copyright file="JournalStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Data.Models.Mongo.Layout.Styles;
    using ProcessingTools.DataAccess.Models.Mongo.Layout.Styles.Journals;
    using ProcessingTools.Services.Models.Layout.Styles.Journals;

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
            this.CreateMap<IJournalInsertStyleModel, JournalStyle>();
            this.CreateMap<IJournalUpdateStyleModel, JournalStyle>();
            this.CreateMap<JournalStyle, JournalStyleDataTransferObject>();
            this.CreateMap<JournalStyle, IJournalStyleDataTransferObject>().As<JournalStyleDataTransferObject>();
            this.CreateMap<JournalStyle, JournalStyleDetailsDataTransferObject>()
                .ForMember(dto => dto.FloatObjectParseStyles, o => o.MapFrom(dm => dm.FloatObjectParseStyles))
                .ForMember(dto => dto.FloatObjectTagStyles, o => o.MapFrom(dm => dm.FloatObjectTagStyles))
                .ForMember(dto => dto.ReferenceParseStyles, o => o.MapFrom(dm => dm.ReferenceParseStyles))
                .ForMember(dto => dto.ReferenceTagStyles, o => o.MapFrom(dm => dm.ReferenceTagStyles));
            this.CreateMap<JournalStyle, IJournalDetailsStyleDataTransferObject>().As<JournalStyleDetailsDataTransferObject>();

            // Data Access - Data Services
            this.CreateMap<IJournalStyleDataTransferObject, JournalStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IJournalStyleDataTransferObject, IJournalStyleModel>().As<JournalStyleModel>();
            this.CreateMap<IJournalDetailsStyleDataTransferObject, JournalDetailsStyleModel>().ForMember(sm => sm.Id, o => o.MapFrom(dm => dm.ObjectId.ToString()));
            this.CreateMap<IJournalDetailsStyleDataTransferObject, IJournalDetailsStyleModel>().As<JournalDetailsStyleModel>();
        }
    }
}
