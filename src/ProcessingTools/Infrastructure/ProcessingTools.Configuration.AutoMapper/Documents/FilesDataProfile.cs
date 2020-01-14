// <copyright file="FilesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.Documents.Files;
    using ProcessingTools.Data.Models.Mongo.Documents;

    /// <summary>
    /// Files data profile.
    /// </summary>
    public class FilesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilesDataProfile"/> class.
        /// </summary>
        public FilesDataProfile()
        {
            this.CreateMap<IFileInsertModel, File>();
            this.CreateMap<IFileUpdateModel, File>();
        }
    }
}
