namespace ProcessingTools.Harvesters.Contracts.Harvesters.Meta
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Models.Harvesters.Meta;

    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
