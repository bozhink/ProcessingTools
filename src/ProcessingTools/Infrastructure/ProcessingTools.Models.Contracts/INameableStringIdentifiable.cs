﻿// <copyright file="INameableStringIdentifiable.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with name and string ID.
    /// </summary>
    public interface INameableStringIdentifiable : INamed, IStringIdentifiable
    {
    }
}
