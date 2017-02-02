namespace ProcessingTools.Harvesters.Contracts.Harvesters.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;

    public interface ITaxonNamesHarvester : IStringEnumerableXmlHarvester
    {
        Task<IEnumerable<string>> HarvestLowerTaxa(XmlNode context);

        Task<IEnumerable<string>> HarvestHigherTaxa(XmlNode context);
    }
}
