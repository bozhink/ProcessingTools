namespace ProcessingTools.Harvesters.Harvesters.Meta
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Harvesters.Meta;
    using ProcessingTools.Contracts.Models.Harvesters.Meta;
    using ProcessingTools.Harvesters.Models.Meta;

    public class PersonNamesHarvester : IPersonNamesHarvester
    {
        public async Task<IPersonNameModel[]> HarvestAsync(XmlNode context)
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

            return items;
        }
    }
}
