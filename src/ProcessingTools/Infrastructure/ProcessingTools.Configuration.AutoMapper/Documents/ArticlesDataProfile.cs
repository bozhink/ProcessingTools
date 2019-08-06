// <copyright file="ArticlesDataProfile.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.AutoMapper.Documents
{
    using global::AutoMapper;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Articles;
    using ProcessingTools.Contracts.Models.Documents.Articles;
    using ProcessingTools.Data.Models.Mongo.Documents;

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
            this.CreateMap<IArticleInsertModel, Article>();
            this.CreateMap<IArticleUpdateModel, Article>();
            this.CreateMap<Journal, ArticleJournal>()
                .ForMember(aj => aj.Id, o => o.MapFrom(j => j.ObjectId.ToString()));
            this.CreateMap<Journal, IArticleJournalDataTransferObject>().As<ArticleJournal>();
        }
    }
}
