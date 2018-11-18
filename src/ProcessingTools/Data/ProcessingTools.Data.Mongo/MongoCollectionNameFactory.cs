// <copyright file="MongoCollectionNameFactory.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using ProcessingTools.Common.Attributes;

    /// <summary>
    /// MongoDB collection name factory.
    /// </summary>
    internal static class MongoCollectionNameFactory
    {
        private static readonly ConcurrentDictionary<Type, string> CollectionNames = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Get collection name based on the model type.
        /// </summary>
        /// <param name="type">Type of the model.</param>
        /// <returns>Name of the collection.</returns>
        public static string Create(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return CollectionNames.GetOrAdd(type, t => GetCollectionName(t));
        }

        /// <summary>
        /// Get collection name based on the model type.
        /// </summary>
        /// <typeparam name="T">Type of the model.</typeparam>
        /// <returns>Name of the collection.</returns>
        public static string Create<T>()
        {
            return Create(typeof(T));
        }

        private static string GetCollectionName(Type type)
        {
            if (type.GetCustomAttributes(typeof(CollectionNameAttribute), false)?.SingleOrDefault() is CollectionNameAttribute collectioNameAttribute)
            {
                return collectioNameAttribute.Name;
            }
            else
            {
                string name = type.Name.ToLowerInvariant();
                int nameLength = name.Length;
                if (name[nameLength - 1] == 'y')
                {
                    return $"{name.Substring(0, nameLength - 1)}ies";
                }
                else
                {
                    return $"{name}s";
                }
            }
        }
    }
}
