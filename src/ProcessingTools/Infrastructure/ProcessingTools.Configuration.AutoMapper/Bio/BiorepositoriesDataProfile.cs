// <copyright file="BiorepositoriesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Bio
{
    using global::AutoMapper;

    /// <summary>
    /// Biorepositories data profile.
    /// </summary>
    public class BiorepositoriesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesDataProfile"/> class.
        /// </summary>
        public BiorepositoriesDataProfile()
        {
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.ICollection, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.Collection>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.ICollectionLabel, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.CollectionLabel>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.ICollectionPer, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.CollectionPer>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.ICollectionPerLabel, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.CollectionPerLabel>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.IInstitution, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.Institution>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.IInstitutionLabel, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.InstitutionLabel>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.IStaff, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.Staff>();
            this.CreateMap<ProcessingTools.Contracts.Models.Bio.Biorepositories.IStaffLabel, ProcessingTools.Data.Models.Mongo.Bio.Biorepositories.StaffLabel>();
        }
    }
}
