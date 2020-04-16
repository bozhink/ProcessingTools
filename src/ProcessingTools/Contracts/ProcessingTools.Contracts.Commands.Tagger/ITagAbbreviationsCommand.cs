﻿// <copyright file="ITagAbbreviationsCommand.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Commands.Tagger
{
    /// <summary>
    /// Tag Abbreviations Command.
    /// </summary>
    public interface ITagAbbreviationsCommand : ITaggerCommand, ISimpleTaggerCommand
    {
    }
}