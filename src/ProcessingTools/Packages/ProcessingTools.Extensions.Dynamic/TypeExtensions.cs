// <copyright file="TypeExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

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
        private static readonly ConcurrentDictionary<Type, object> TypeDefaults = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Returns the default value of a specified Type.
        /// </summary>
        /// <param name="type">Type of the default value.</param>
        /// <returns>The default value for the type.</returns>
        public static object Default(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsValueType)
            {
                return TypeDefaults.GetOrAdd(type, t => Activator.CreateInstance(t));
            }

            return null;
        }

        /// <summary>
        /// Returns the default value of a specified Type.
        /// </summary>
        /// <param name="type">Type of the default value.</param>
        /// <returns>The default value for the type.</returns>
        public static object DefaultValue(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType || Nullable.GetUnderlyingType(type) != null)
            {
                return null;
            }

            if (type.IsGenericType)
            {
                return type.GenericTypeArguments[0].DefaultValue();
            }

            return TypeDefaults.GetOrAdd(type, t => Activator.CreateInstance(t));
        }

        /// <summary>
        /// Determines if a method is async or not.
        /// See http://stackoverflow.com/questions/28099669/intercept-async-method-that-returns-generic-task-via-dynamicproxy.
        /// </summary>
        /// <param name="method">MethodInfo object.</param>
        /// <returns>MethodType.</returns>
        public static MethodType GetDelegateType(this MethodInfo method)
        {
            if (method is null)
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
        /// Gets ID property.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns><see cref="PropertyInfo"/> for the ID property.</returns>
        public static PropertyInfo GetIdProperty(this Type modelType)
        {
            if (modelType is null)
            {
                throw new ArgumentNullException(nameof(modelType));
            }

            var properties = modelType.GetProperties();
            if (properties is null)
            {
                return null;
            }

            Regex matchIdName = new Regex(@"(?i)\A.*id\Z");
            var property = properties.FirstOrDefault(p => matchIdName.IsMatch(p.Name));
            return property;
        }

        /// <summary>
        /// Gets ID property.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="attributeType">Type of the ID attribute.</param>
        /// <returns><see cref="PropertyInfo"/> for the ID property.</returns>
        public static PropertyInfo GetIdProperty(this Type modelType, Type attributeType)
        {
            if (modelType is null)
            {
                throw new ArgumentNullException(nameof(modelType));
            }

            if (attributeType is null)
            {
                throw new ArgumentNullException(nameof(attributeType));
            }

            var properties = modelType.GetProperties();
            if (properties is null)
            {
                return null;
            }

            var property = properties.FirstOrDefault(p => Attribute.IsDefined(p, attributeType));
            if (property is null)
            {
                return modelType.GetIdProperty();
            }

            return property;
        }

        /// <summary>
        /// Gets ID property.
        /// </summary>
        /// <typeparam name="TIdAttribute">Type of the ID attribute.</typeparam>
        /// <param name="modelType">Type of the model.</param>
        /// <returns><see cref="PropertyInfo"/> for the ID property.</returns>
        public static PropertyInfo GetIdProperty<TIdAttribute>(this Type modelType)
            where TIdAttribute : Attribute
        {
            if (modelType is null)
            {
                throw new ArgumentNullException(nameof(modelType));
            }

            return modelType.GetIdProperty(typeof(TIdAttribute));
        }

        /// <summary>
        /// Gets value of the ID property.
        /// </summary>
        /// <param name="model">Model object to be evaluated.</param>
        /// <returns>Value of the ID property.</returns>
        public static object GetIdValue(this object model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var property = model.GetType().GetIdProperty();
            if (property is null)
            {
                return null;
            }

            return property.GetValue(model);
        }

        /// <summary>
        /// Gets value of the ID property.
        /// </summary>
        /// <param name="model">Model object to be evaluated.</param>
        /// <param name="attributeType">Type of the ID attribute.</param>
        /// <returns>Value of the ID property.</returns>
        public static object GetIdValue(this object model, Type attributeType)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (attributeType is null)
            {
                throw new ArgumentNullException(nameof(attributeType));
            }

            var property = model.GetType().GetIdProperty(attributeType);
            if (property is null)
            {
                return null;
            }

            return property.GetValue(model);
        }

        /// <summary>
        /// Gets value of the ID property.
        /// </summary>
        /// <typeparam name="TIdAttribute">Type of the ID attribute.</typeparam>
        /// <param name="model">Model object to be evaluated.</param>
        /// <returns>Value of the ID property.</returns>
        public static object GetIdValue<TIdAttribute>(this object model)
            where TIdAttribute : Attribute
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return model.GetIdValue(typeof(TIdAttribute));
        }
    }
}
