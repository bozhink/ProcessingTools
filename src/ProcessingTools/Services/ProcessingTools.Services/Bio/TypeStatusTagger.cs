// <copyright file="TypeStatusTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio
{
    using ProcessingTools.Services.Contracts.Bio;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts;
    using ProcessingTools.Services.Contracts.Bio;
    using ProcessingTools.Services.Models.Contracts;

    /// <summary>
    /// Type status tagger.
    /// </summary>
    public class TypeStatusTagger : StringDataMinerTagger<ITypeStatusDataMiner, ITypeStatusTagModelProvider>, ITypeStatusTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeStatusTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public TypeStatusTagger(IStringDataMinerEvaluator<ITypeStatusDataMiner> evaluator, IStringTagger tagger, ITypeStatusTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
