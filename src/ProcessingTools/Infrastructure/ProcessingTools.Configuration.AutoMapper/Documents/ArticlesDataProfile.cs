// <copyright file="ArticlesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Articles;
    using ProcessingTools.Contracts.Models.Documents.Articles;

    /// <summary>
    /// Articles data profile.
    /// </summary>
    public class ArticlesDataProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesDataProfile"/> class.
        /// </summary>
        public ArticlesDataProfile()
        {
            this.CreateMap<IArticleInsertModel, ProcessingTools.Data.Models.Mongo.Documents.Article>();
            this.CreateMap<IArticleUpdateModel, ProcessingTools.Data.Models.Mongo.Documents.Article>();
            this.CreateMap<ProcessingTools.Data.Models.Mongo.Documents.Journal, ProcessingTools.Data.Models.Mongo.Documents.ArticleJournal>()
                .ForMember(aj => aj.Id, o => o.MapFrom(j => j.ObjectId.ToString()));
            this.CreateMap<ProcessingTools.Data.Models.Mongo.Documents.Journal, IArticleJournalDataTransferObject>().As<ProcessingTools.Data.Models.Mongo.Documents.ArticleJournal>();
        }
    }
}
