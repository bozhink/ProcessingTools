﻿// <copyright file="InstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Institutions
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Institutions;
    using ProcessingTools.Services.Contracts.Resources;

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
