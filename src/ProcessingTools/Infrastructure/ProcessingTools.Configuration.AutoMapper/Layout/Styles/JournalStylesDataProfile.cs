// <copyright file="JournalStylesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Layout.Styles
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Layout.Styles.Journals;
    using ProcessingTools.Data.Models.Mongo.Layout;

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
            this.CreateMap<IJournalInsertStyleModel, JournalStyle>();
            this.CreateMap<IJournalUpdateStyleModel, JournalStyle>();
        }
    }
}
