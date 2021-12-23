// <copyright file="CsvObjectAttribute.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Serialization.Csv
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
