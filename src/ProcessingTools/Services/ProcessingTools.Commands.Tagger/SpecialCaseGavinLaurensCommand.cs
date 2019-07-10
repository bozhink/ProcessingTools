// <copyright file="SpecialCaseGavinLaurensCommand.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Services.Contracts.Special;

    /// <summary>
    /// Special case Gavin-Laurens command.
    /// </summary>
    public class SpecialCaseGavinLaurensCommand : DocumentParserCommand<IGavinLaurensParser>, ISpecialCaseGavinLaurensCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialCaseGavinLaurensCommand"/> class.
        /// </summary>
        /// <param name="parser">Instance of <see cref="IGavinLaurensParser"/>.</param>
        public SpecialCaseGavinLaurensCommand(IGavinLaurensParser parser)
            : base(parser)
        {
        }
    }
}
