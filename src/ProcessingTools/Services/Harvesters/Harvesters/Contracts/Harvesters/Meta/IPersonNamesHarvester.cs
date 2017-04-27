namespace ProcessingTools.Harvesters.Contracts.Harvesters.Meta
{
    using Models.Meta;
    using ProcessingTools.Contracts.Harvesters;

    public interface IPersonNamesHarvester : IGenericEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
