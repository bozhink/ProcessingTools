// <copyright file="ModelNameHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.HelpPage.ModelDescriptions
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Helper functions.
    /// </summary>
    public static class ModelNameHelper
    {
        /// <summary>
        /// Gets the model name based on the <see cref="ModelNameAttribute"/>.
        /// </summary>
        /// <param name="type">The type </param>
        /// <returns>The model name</returns>
        public static string GetModelName(Type type)
        {
            ModelNameAttribute modelNameAttribute = type.GetCustomAttribute<ModelNameAttribute>();
            if (modelNameAttribute != null && !string.IsNullOrEmpty(modelNameAttribute.Name))
            {
                return modelNameAttribute.Name;
            }

            string modelName = type.Name;
            if (type.IsGenericType)
            {
                // Format the generic type name to something like: GenericOfAgurment1AndArgument2
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.Name;

                // Trim the generic parameter counts from the name
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
                string[] argumentTypeNames = genericArguments.Select(t => GetModelName(t)).ToArray();
                modelName = string.Format(CultureInfo.InvariantCulture, "{0}Of{1}", genericTypeName, string.Join("And", argumentTypeNames));
            }

            return modelName;
        }
    }
}
