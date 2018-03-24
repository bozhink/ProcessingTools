﻿// <copyright file="CsvColumnAttribute.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Serialization.Csv
{
    using System;

    /// <summary>
    /// CSV column attribute.
    /// </summary>
    public class CsvColumnAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvColumnAttribute"/> class.
        /// </summary>
        public CsvColumnAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvColumnAttribute"/> class.
        /// </summary>
        /// <param name="name">The column name.</param>
        public CsvColumnAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the column name.
        /// </summary>
        public string Name { get; private set; }
    }
}
