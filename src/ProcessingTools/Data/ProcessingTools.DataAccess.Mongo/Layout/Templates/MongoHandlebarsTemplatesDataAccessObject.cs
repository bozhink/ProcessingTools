// <copyright file="MongoHandlebarsTemplatesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Mongo.Layout.Templates
{
    using AutoMapper;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.DataAccess.Layout.Templates;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Layout.Templates.Handlebars;
    using ProcessingTools.Data.Models.Mongo.Layout.Templates;

    /// <summary>
    /// MongoDB implementation of <see cref="IHandlebarsTemplatesDataAccessObject"/>.
    /// </summary>
    public class MongoHandlebarsTemplatesDataAccessObject : MongoTemplatesDataAccessObject<IHandlebarsTemplateDataTransferObject, IHandlebarsTemplateDetailsDataTransferObject, IHandlebarsTemplateInsertModel, IHandlebarsTemplateUpdateModel, HandlebarsTemplate>,
        IHandlebarsTemplatesDataAccessObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoHandlebarsTemplatesDataAccessObject"/> class.
        /// </summary>
        /// <param name="collection">Instance of <see cref="IMongoCollection{TDM}"/>.</param>
        /// <param name="applicationContext">Instance of <see cref="IApplicationContext"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public MongoHandlebarsTemplatesDataAccessObject(IMongoCollection<HandlebarsTemplate> collection, IApplicationContext applicationContext, IMapper mapper)
            : base(collection, applicationContext, mapper)
        {
        }
    }
}
