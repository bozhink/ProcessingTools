// <copyright file="CustomIdAttribute.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Tests.Models
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
