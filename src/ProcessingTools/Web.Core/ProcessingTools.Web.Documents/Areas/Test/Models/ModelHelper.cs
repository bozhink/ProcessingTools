// <copyright file="ModelHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Test.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model helper.
    /// </summary>
    public static class ModelHelper
    {
        /// <summary>
        /// Get multi line data.
        /// </summary>
        /// <returns>List of table objects.</returns>
        public static List<object> MultiLineData()
        {
            List<object> objs = new List<object>
            {
                new[] { "x", "sin(x)", "cos(x)", "sin(x)^2" }
            };
            for (int i = 0; i < 70; i++)
            {
                double x = 0.1 * i;
                objs.Add(new[] { x, Math.Sin(x), Math.Cos(x), Math.Sin(x) * Math.Sin(x) });
            }

            return objs;
        }
    }
}
