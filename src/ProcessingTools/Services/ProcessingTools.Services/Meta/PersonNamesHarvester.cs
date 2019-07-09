// <copyright file="PersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Meta
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Services.Contracts.Meta;
    using ProcessingTools.Services.Models.Contracts.Meta;
    using ProcessingTools.Services.Models.Meta;

    /// <summary>
    /// Person Names Harvester.
    /// </summary>
    public class PersonNamesHarvester : IPersonNamesHarvester
    {
        /// <inheritdoc/>
        public Task<IPersonNameModel[]> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var query = context.SelectNodes("//name[surname]")
                .Cast<XmlNode>()
                .Select(n => new PersonNameModel
                {
                    GivenNames = n[ElementNames.GivenNames]?.InnerText,
                    Surname = n[ElementNames.Surname]?.InnerText,
                    Prefix = n[ElementNames.Prefix]?.InnerText,
                    Suffix = n[ElementNames.Suffix]?.InnerText,
                });

            return query.ToArrayAsync<IPersonNameModel>();
        }
    }
}
