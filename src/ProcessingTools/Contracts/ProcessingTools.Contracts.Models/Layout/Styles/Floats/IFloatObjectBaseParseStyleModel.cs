﻿// <copyright file="IFloatObjectBaseParseStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Layout.Styles.Floats
{
    /// <summary>
    /// Float object base parse style model.
    /// </summary>
    public interface IFloatObjectBaseParseStyleModel : IFloatObjectStyleModel, IParseStyleModel
    {
        /// <summary>
        /// Gets the script content.
        /// </summary>
        string Script { get; }
    }
}