// <copyright file="SpecimenCode.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Bio.Codes
{
    using ProcessingTools.Processors.Models.Contracts.Bio.Codes;

    /// <summary>
    /// Specimen code.
    /// </summary>
    public class SpecimenCode : ISpecimenCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecimenCode"/> class.
        /// </summary>
        /// <param name="prefix">Value of prefix.</param>
        /// <param name="type">Value of type.</param>
        /// <param name="code">Value of code,</param>
        /// <param name="fullString">Value of fullString.</param>
        public SpecimenCode(string prefix, string type, string code = null, string fullString = null)
        {
            this.Prefix = prefix;
            this.Type = type;
            this.Code = code;
            this.FullString = fullString;
        }

        /// <inheritdoc/>
        public string Prefix { get; set; }

        /// <inheritdoc/>
        public string Type { get; set; }

        /// <inheritdoc/>
        public string Code { get; set; }

        /// <inheritdoc/>
        public string FullString { get; set; }
    }
}
