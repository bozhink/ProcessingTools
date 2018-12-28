// <copyright file="ContainerBuilderExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Extensions
{
    using System;
    using Autofac;
    using MongoDB.Driver;
    using ProcessingTools.Data.Mongo;

    /// <summary>
    /// <see cref="ContainerBuilder" /> extensions.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register <see cref="IMongoCollection{T}" /> binding.
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder" /> instance to be updated.</param>
        /// <param name="bindingName">Name of the binding for the database provider.</param>
        /// <returns>Configured <see cref="ContainerBuilder" /> instance.</returns>
        public static ContainerBuilder RegisterMongoCollectionBinding<T>(this ContainerBuilder builder, string bindingName)
            where T: class
        {
            builder
                .Register(c =>
                {
                    var componentContext = c.Resolve<IComponentContext>();
                    var databaseProvider = componentContext.ResolveNamed<IMongoDatabaseProvider>(bindingName);
                    return CreateMongoCollection<T>(databaseProvider);
                })
                .As<IMongoCollection<T>>()
                .InstancePerLifetimeScope();
            
            return builder;
        }

        private static IMongoCollection<T> CreateMongoCollection<T>(IMongoDatabaseProvider databaseProvider)
            where T : class
        {
            var db = databaseProvider.Create();
            string collectionName = ProcessingTools.Data.Mongo.MongoCollectionNameFactory.Create<T>();
            return db.GetCollection<T>(collectionName);
        }
        
    }
}