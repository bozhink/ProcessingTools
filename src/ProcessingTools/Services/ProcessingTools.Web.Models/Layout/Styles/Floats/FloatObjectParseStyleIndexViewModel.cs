﻿// <copyright file="FloatObjectParseStyleIndexViewModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Models.Layout.Styles.Floats
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Enumerations.Nlm;

    /// <summary>
    /// Float object parse style index view model.
    /// </summary>
    public class FloatObjectParseStyleIndexViewModel
    {
        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the style.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the reference type of the floating object according to NLM schema.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Reference type")]
        public ReferenceType FloatReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the XPath for selection of the XML objects which provide information about the floating object.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Float object XPath")]
        public string FloatObjectXPath { get; set; }

        /// <summary>
        /// Gets or sets created by.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets created on.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets modified by.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Modified by")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets modified on.
        /// </summary>
        [ReadOnly(true)]
        [Display(Name = "Modified on")]
        public DateTime ModifiedOn { get; set; }
    }
}