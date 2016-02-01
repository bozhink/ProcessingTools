namespace ProcessingTools.Infrastructure.Extensions
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Perform a deep Copy of the object.
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
                throw new ArgumentException("The type must be serializable.", "source");
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

        public static object DeepCopy(this object source)
        {
            Type type = source.GetType();

            if (!type.IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (object.ReferenceEquals(source, null))
            {
                return type.Default();
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