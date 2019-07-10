// <copyright file="InstitutionsTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Institutions
{
    using ProcessingTools.Services.Contracts.Institutions;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts;
    using ProcessingTools.Services.Contracts.Institutions;
    using ProcessingTools.Services.Models.Contracts;

    /// <summary>
    /// Institutions tagger.
    /// </summary>
    public class InstitutionsTagger : StringDataMinerTagger<IInstitutionsDataMiner, IInstitutionTagModelProvider>, IInstitutionsTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionsTagger"/> class.
        /// </summary>
        /// <param name="evaluator">Generic string data miner evaluator.</param>
        /// <param name="tagger">String tagger.</param>
        /// <param name="tagModelProvider">Tag model provider.</param>
        public InstitutionsTagger(IStringDataMinerEvaluator<IInstitutionsDataMiner> evaluator, IStringTagger tagger, IInstitutionTagModelProvider tagModelProvider)
            : base(evaluator, tagger, tagModelProvider)
        {
        }
    }
}
