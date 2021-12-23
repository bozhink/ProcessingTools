// <copyright file="CustomIdAttribute.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic.Tests.Models
{
    using System;

    /// <summary>
    /// Custom ID attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    internal class CustomIdAttribute : Attribute
    {
    }
}
