// <copyright file="CsvObjectAttribute.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System;

namespace ProcessingTools.Services.Serialization.Csv
{
    /// <summary>
    /// CSV object attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CsvObjectAttribute : Attribute
    {
    }
}
