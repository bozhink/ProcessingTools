namespace ProcessingTools.Harvesters.Contracts.Harvesters.Meta
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Harvesters.Contracts.Models.Meta;

    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
