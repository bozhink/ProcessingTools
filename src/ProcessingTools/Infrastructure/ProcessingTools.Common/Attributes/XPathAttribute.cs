﻿// <copyright file="XPathAttribute.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Attributes
{
    using System;

    /// <summary>
    /// XPath attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class XPathAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XPathAttribute"/> class.
        /// </summary>
        /// <param name="xpath">XPath.</param>
        public XPathAttribute(string xpath)
        {
            this.XPath = xpath;
        }

        /// <summary>
        /// Gets the XPath.
        /// </summary>
        public string XPath { get; }
    }
}
