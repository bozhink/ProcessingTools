// <copyright file="ControllerExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    /// <summary>
    /// Controller extensions.
    /// </summary>
    public static class ControllerExtensions
    {
        private const string ControllerNameSuffix = "Controller";

        /// <summary>
        /// Get controller name.
        /// </summary>
        /// <typeparam name="T">Type of the controller.</typeparam>
        /// <returns>Name of the controller.</returns>
        public static string GetControllerName<T>()
        {
            var type = typeof(T);
            string name = type.Name;
            int suffixIndex = name.IndexOf(ControllerNameSuffix, 0);
            if (suffixIndex > 0)
            {
                name = name.Substring(0, suffixIndex);
            }

            return name;
        }
    }
}
