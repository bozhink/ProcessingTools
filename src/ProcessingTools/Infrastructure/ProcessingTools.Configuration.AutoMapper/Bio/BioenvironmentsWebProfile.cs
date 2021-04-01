// <copyright file="BioenvironmentsWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Bio
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Bio.Environments;
    using ProcessingTools.Web.Models.Bio.Environments;

    /// <summary>
    /// Bio environments web profile.
    /// </summary>
    public class BioenvironmentsWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BioenvironmentsWebProfile"/> class.
        /// </summary>
        public BioenvironmentsWebProfile()
        {
            this.CreateMap<IEnvoTerm, EnvoTermResponseModel>();
        }
    }
}
