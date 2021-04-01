// <copyright file="IDocumentProcessingCommandsModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Commands
{
    using System.Collections.Generic;

    /// <summary>
    /// Document processing commands model.
    /// </summary>
    public interface IDocumentProcessingCommandsModel : IDocumentProcessingModel
    {
        /// <summary>
        /// Gets the list of the processing commands to be applied.
        /// </summary>
        IList<IProcessingCommandModel> Commands { get; }
    }
}
