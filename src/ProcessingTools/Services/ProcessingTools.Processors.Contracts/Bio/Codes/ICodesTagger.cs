// <copyright file="ICodesTagger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Bio.Codes
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Codes tagger.
    /// </summary>
    public interface ICodesTagger
    {
        /// <summary>
        /// Tag known specimen codes.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> context for tagging.</param>
        /// <returns>Task</returns>
        Task TagKnownSpecimenCodesAsync(IDocument document);

        /// <summary>
        /// Tag specimen codes.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> context for tagging.</param>
        /// <returns>Task</returns>
        Task TagSpecimenCodesAsync(IDocument document);
    }
}
