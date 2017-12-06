namespace ProcessingTools.Harvesters.Contracts.Harvesters.Bio
{
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Services.Harvesters;

    public interface ITaxonNamesHarvester : IStringEnumerableXmlHarvester
    {
        Task<string[]> HarvestLowerTaxaAsync(XmlNode context);

        Task<string[]> HarvestHigherTaxaAsync(XmlNode context);
    }
}
