// <copyright file="PersonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models.Meta;
    using ProcessingTools.Contracts.Services.Meta;
    using ProcessingTools.Services.Models.Meta;

    /// <summary>
    /// Person Names Harvester.
    /// </summary>
    public class PersonNamesHarvester : IPersonNamesHarvester
    {
        /// <inheritdoc/>
        public Task<IList<IPersonNameModel>> HarvestAsync(XmlNode context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Run<IList<IPersonNameModel>>(() =>
            {
                var query = context.SelectNodes("//name[surname]")
                    .Cast<XmlNode>()
                    .Select(n => new PersonNameModel
                    {
                        GivenNames = n[ElementNames.GivenNames]?.InnerText,
                        Surname = n[ElementNames.Surname]?.InnerText,
                        Prefix = n[ElementNames.Prefix]?.InnerText,
                        Suffix = n[ElementNames.Suffix]?.InnerText,
                    });

                return query.ToArray<IPersonNameModel>();
            });
        }
    }
}
