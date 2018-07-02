// <copyright file="TypeExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;

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

        /// <summary>
        /// Determines if a method is async or not.
        /// See http://stackoverflow.com/questions/28099669/intercept-async-method-that-returns-generic-task-via-dynamicproxy
        /// </summary>
        /// <param name="method">MethodInfo object.</param>
        /// <returns>MethodType</returns>
        public static MethodType GetDelegateType(this MethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            var returnType = method.ReturnType;

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return MethodType.AsyncFunction;
            }

            if (returnType == typeof(Task))
            {
                return MethodType.AsyncAction;
            }

            return MethodType.Synchronous;
        }

        /// <summary>
        /// Get description message for command.
        /// </summary>
        /// <param name="type">Source type.</param>
        /// <returns>Description as string.</returns>
        public static string GetDescriptionMessageForCommand(this Type type)
        {
            string message = type.GetCustomAttribute<DescriptionAttribute>(false)?.Description;

            if (string.IsNullOrWhiteSpace(message))
            {
                var name = Regex.Replace(type.FullName, @".*?([^\.]+)\Z", "$1");
                name = Regex.Replace(name, @"Command\Z", string.Empty);

                message = Regex.Replace(name, "(?=[A-Z])", " ").Trim();
            }

            return message;
        }
    }
}
