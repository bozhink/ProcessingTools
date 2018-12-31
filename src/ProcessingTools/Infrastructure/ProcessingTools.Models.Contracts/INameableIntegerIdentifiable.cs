﻿// <copyright file="INameableIntegerIdentifiable.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with name and integer ID.
    /// </summary>
    public interface INameableIntegerIdentifiable : INamed, IIntegerIdentifiable
    {
    }
}
