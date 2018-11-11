// <copyright file="PersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Meta
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Meta;

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
                    Suffix = n[ElementNames.Suffix]?.InnerText
                });

            return query.ToArrayAsync<IPersonNameModel>();
        }
    }
}
