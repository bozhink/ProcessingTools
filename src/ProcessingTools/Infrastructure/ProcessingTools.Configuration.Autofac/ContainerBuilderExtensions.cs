// <copyright file="ContainerBuilderExtensions.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Configuration.Autofac
{
    using global::Autofac;
    using global::Autofac.Core;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Mongo;

    /// <summary>
    /// <see cref="ContainerBuilder" /> extensions.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register MongoDB database.
        /// </summary>
        /// <param name="builder"><see cref="ContainerBuilder" /> instance to be updated.</param>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="databaseName">Database name.</param>
        /// <param name="bindingName">Name of the binding.</param>
        /// <returns>Configured <see cref="ContainerBuilder" /> instance.</returns>
        public static ContainerBuilder RegisterMongoDatabase(this ContainerBuilder builder, string connectionString, string databaseName, string bindingName)
        {
            builder
                .Register(c =>
                {
                    IMongoClient client = new MongoClient(connectionString);
                    return client.GetDatabase(databaseName);
                })
                .As<IMongoDatabase>()
                .Named<IMongoDatabase>(bindingName)
                .InstancePerLifetimeScope();

            return builder;
        }

        /// <summary>
        /// Register MongoDB database initializer.
        /// </summary>
        /// <typeparam name="T">Type of the database initializer.</typeparam>
        /// <param name="builder"><see cref="ContainerBuilder" /> instance to be updated.</param>
        /// <param name="bindingName">Name of the binding.</param>
        /// <returns>Configured <see cref="ContainerBuilder" /> instance.</returns>
        public static ContainerBuilder RegisterMongoDatabaseInitializer<T>(this ContainerBuilder builder, string bindingName)
            where T : IDatabaseInitializer
        {
            builder.RegisterType<T>().AsSelf()
                .WithParameter(new ResolvedParameter(
                    (p, c) => p.ParameterType == typeof(IMongoDatabase),
                    (p, c) => c.ResolveNamed<IMongoDatabase>(bindingName)))
                .InstancePerDependency();

            return builder;
        }

        /// <summary>
        /// Register <see cref="IMongoCollection{T}" /> binding.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="builder"><see cref="ContainerBuilder" /> instance to be updated.</param>
        /// <param name="bindingName">Name of the binding.</param>
        /// <returns>Configured <see cref="ContainerBuilder" /> instance.</returns>
        public static ContainerBuilder RegisterMongoCollectionBinding<T>(this ContainerBuilder builder, string bindingName)
            where T : class
        {
            builder
                .Register(c => GetMongoCollection<T>(c, bindingName))
                .As<IMongoCollection<T>>()
                .InstancePerLifetimeScope();

            return builder;
        }

        private static IMongoCollection<T> GetMongoCollection<T>(IComponentContext context, string bindingName)
            where T : class
        {
            var db = context.ResolveNamed<IMongoDatabase>(bindingName);
            var settings = context.ResolveNamed<MongoCollectionSettings>(bindingName);
            string collectionName = MongoCollectionNameFactory.Create<T>();
            return db.GetCollection<T>(collectionName, settings);
        }
    }
}
