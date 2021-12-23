// <copyright file="ResolveMediaTypesCommand.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Services.Floats;

    /// <summary>
    /// Resolve media types command.
    /// </summary>
    [System.ComponentModel.Description("Resolve media-types.")]
    public class ResolveMediaTypesCommand : XmlContextParserCommand<IMediatypesParser>, IResolveMediaTypesCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveMediaTypesCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IMediatypesParser"/>.</param>
        public ResolveMediaTypesCommand(IMediatypesParser parser)
            : base(parser)
        {
        }
    }
}
