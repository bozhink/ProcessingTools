// <copyright file="CsvObjectAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Serialization.Csv
{
    using System;

    /// <summary>
    /// CSV object attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CsvObjectAttribute : Attribute
    {
    }
}
