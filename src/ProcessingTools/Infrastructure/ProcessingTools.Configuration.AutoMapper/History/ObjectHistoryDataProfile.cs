// <copyright file="ObjectHistoryDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.History
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.Models.History;
    using ProcessingTools.Data.Models.Mongo.History;

    /// <summary>
    /// Object history data profile.
    /// </summary>
    public class ObjectHistoryDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectHistoryDataProfile"/> class.
        /// </summary>
        public ObjectHistoryDataProfile()
        {
            this.CreateMap<IObjectHistory, ObjectHistory>();
        }
    }
}
