// <copyright file="ObjectExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
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
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            if (object.ReferenceEquals(source, null))
            {
                return default(T);
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
            if (source == null)
            {
                return null;
            }

            Type type = source.GetType();

            if (!type.IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, source);
                stream.Position = 0;

                return formatter.Deserialize(stream);
            }
        }
    }
}
