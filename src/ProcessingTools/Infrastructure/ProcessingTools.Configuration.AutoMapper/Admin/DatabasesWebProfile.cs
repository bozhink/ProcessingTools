// <copyright file="DatabasesWebProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Admin
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Services.Models.Admin.Databases;
    using ProcessingTools.Web.Models.Admin.Databases;

    /// <summary>
    /// Databases (web) profile.
    /// </summary>
    public class DatabasesWebProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabasesWebProfile"/> class.
        /// </summary>
        public DatabasesWebProfile()
        {
            this.CreateMap<IInitializeModel, InitializeResponseModel>();
            this.CreateMap<InitializeResponseModel, InitializeViewModel>();
        }
    }
}
