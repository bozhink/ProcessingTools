namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Extensions related to Type objects.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/325426/programmatic-equivalent-of-defaulttype.
    /// </remarks>
    public static class TypeExtensions
    {
        /// <summary>
        /// A thread-safe way to hold default instances created at run-time.
        /// </summary>
        private static ConcurrentDictionary<Type, object> typeDefaults = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Returns the default value of a specified Type.
        /// </summary>
        /// <param name="type">Type of the default value.</param>
        /// <returns>The default value for Type type.</returns>
        public static object Default(this Type type)
        {
            if (type.IsValueType)
            {
                return typeDefaults.GetOrAdd(type, t => Activator.CreateInstance(t));
            }

            return null;
        }

        /// <summary>
        /// Returns the default value of a specified Type.
        /// </summary>
        /// <param name="type">Type of the default value.</param>
        /// <returns>The default value for Type type.</returns>
        public static object DefaultValue(this Type type)
        {
            if (!type.IsValueType || Nullable.GetUnderlyingType(type) != null)
            {
                return null;
            }

            if (type.IsGenericType)
            {
                return type.GenericTypeArguments[0].DefaultValue();
            }

            return typeDefaults.GetOrAdd(type, t => Activator.CreateInstance(t));
        }
    }
}