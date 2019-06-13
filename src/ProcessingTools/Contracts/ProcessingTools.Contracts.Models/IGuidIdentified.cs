﻿// <copyright file="IGuidIdentified.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    using System;

    /// <summary>
    /// Model with GUID ID.
    /// </summary>
    public interface IGuidIdentified : IIdentified<Guid>
    {
    }
}