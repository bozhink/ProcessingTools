// <copyright file="ObjectExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        private static readonly Type[] CopyTypes =
        {
            typeof(int),
            typeof(int?),
            typeof(double),
            typeof(double?),
            typeof(string),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(bool),
            typeof(bool?),
            typeof(decimal),
            typeof(decimal?),
        };

        /// <summary>
        /// Perform a deep copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/78536/deep-cloning-objects.
        /// </remarks>
        public static T DeepCopy<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException($"The type {typeof(T)} must be serializable.", nameof(source));
            }

            if (object.ReferenceEquals(source, null))
            {
                return default;
            }

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Perform a deep copy of the object.
        /// </summary>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static object DeepCopy(this object source)
        {
            if (source is null)
            {
                return null;
            }

            Type type = source.GetType();

            if (!type.IsSerializable)
            {
                throw new ArgumentException($"The type {type} must be serializable.", nameof(source));
            }

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Position = 0;

                return formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Copy values of the source object to the target object.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="target">Target object to be updated.</param>
        /// <param name="source">Source object to be read.</param>
        /// <returns>Updated target object.</returns>
        public static T CopyFrom<T>(this T target, T source)
            where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in properties)
            {
                if (property.CanWrite && (property.PropertyType.IsValueType || CopyTypes.Contains(property.PropertyType)))
                {
                    property.SetValue(target, property.GetValue(source, null), null);
                }
            }

            return target;
        }

        /// <summary>
        /// Copy values of the source object to the target object.
        /// </summary>
        /// <typeparam name="T">Type of the objects.</typeparam>
        /// <param name="target">Target object to be updated.</param>
        /// <param name="source">Source object to be read.</param>
        /// <param name="exceptProperties">Names of the properties to not-be-mapped.</param>
        /// <returns>Updated target object.</returns>
        public static T CopyFrom<T>(this T target, T source, params string[] exceptProperties)
            where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in properties)
            {
                if (exceptProperties.Contains(property.Name))
                {
                    continue;
                }

                if (property.CanWrite && (property.PropertyType.IsValueType || CopyTypes.Contains(property.PropertyType)))
                {
                    property.SetValue(target, property.GetValue(source, null), null);
                }
            }

            return target;
        }
    }
}
