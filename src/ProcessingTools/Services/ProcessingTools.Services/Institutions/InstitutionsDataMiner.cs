// <copyright file="InstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Institutions
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Contracts.Services.Institutions;
    using ProcessingTools.Contracts.Services.Resources;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Institutions data miner.
    /// </summary>
    public class InstitutionsDataMiner : SimpleServiceStringDataMiner<IInstitutionsDataService, IInstitution, IFilter>, IInstitutionsDataMiner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionsDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IInstitutionsDataService"/> instance.</param>
        public InstitutionsDataMiner(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}
