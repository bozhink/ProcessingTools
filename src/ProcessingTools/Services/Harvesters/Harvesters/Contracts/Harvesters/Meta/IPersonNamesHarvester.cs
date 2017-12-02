namespace ProcessingTools.Harvesters.Contracts.Harvesters.Meta
{
    using ProcessingTools.Harvesters.Contracts.Models.Meta;
    using ProcessingTools.Services.Contracts.Harvesters;

    public interface IPersonNamesHarvester : IEnumerableXmlHarvester<IPersonNameModel>
    {
    }
}
