// <copyright file="ModelExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Model Extensions.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        /// Gets ID property.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns><see cref="PropertyInfo"/> for the ID property.</returns>
        public static PropertyInfo GetIdProperty(this Type modelType)
        {
            var properties = modelType.GetProperties();
            if (properties == null)
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
            var properties = modelType.GetProperties();
            if (properties == null)
            {
                return null;
            }

            var property = properties.FirstOrDefault(p => Attribute.IsDefined(p, attributeType));
            if (property == null)
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
            return modelType.GetIdProperty(typeof(TIdAttribute));
        }

        /// <summary>
        /// Gets value of the ID property.
        /// </summary>
        /// <param name="model">Model object to be evaluated.</param>
        /// <returns>Value of the ID property.</returns>
        public static object GetIdValue(this object model)
        {
            var property = model.GetType().GetIdProperty();
            if (property == null)
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
            var property = model.GetType().GetIdProperty(attributeType);
            if (property == null)
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
            return model.GetIdValue(typeof(TIdAttribute));
        }
    }
}
