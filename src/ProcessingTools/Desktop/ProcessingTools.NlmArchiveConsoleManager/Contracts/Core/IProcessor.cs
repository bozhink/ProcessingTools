﻿// <copyright file="IProcessor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.NlmArchiveConsoleManager.Contracts.Core
{
    using System.Threading.Tasks;

    public interface IProcessor
    {
        Task Process();
    }
}