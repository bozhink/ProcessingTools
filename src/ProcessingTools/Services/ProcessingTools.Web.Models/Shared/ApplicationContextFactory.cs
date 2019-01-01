﻿// <copyright file="ApplicationContextFactory.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Shared
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Application context factory.
    /// </summary>
    public class ApplicationContextFactory
    {
        /// <summary>
        /// Gets or sets the application context.
        /// </summary>
        public IApplicationContext ApplicationContext { get; set; }
    }
}
