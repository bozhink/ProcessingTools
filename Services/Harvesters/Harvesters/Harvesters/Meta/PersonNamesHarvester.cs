namespace ProcessingTools.Harvesters.Harvesters.Meta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts.Harvesters.Meta;
    using Contracts.Models.Meta;
    using Models.Meta;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Extensions.Linq;

    public class PersonNamesHarvester : IPersonNamesHarvester
    {
        public async Task<IEnumerable<IPersonNameModel>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var items = await context.SelectNodes("//name[surname]")
                .Cast<XmlNode>()
                .Select(n => new PersonNameModel
                {
                    GivenNames = n[ElementNames.GivenNames]?.InnerText,
                    Surname = n[ElementNames.Surname]?.InnerText,
                    Prefix = n[ElementNames.Prefix]?.InnerText,
                    Suffix = n[ElementNames.Suffix]?.InnerText
                })
                .ToArrayAsync();

            return new HashSet<IPersonNameModel>(items);
        }
    }
}
