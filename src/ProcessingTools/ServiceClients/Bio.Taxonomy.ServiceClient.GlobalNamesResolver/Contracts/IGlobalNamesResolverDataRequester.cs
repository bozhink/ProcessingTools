namespace ProcessingTools.Bio.Taxonomy.ServiceClient.GlobalNamesResolver.Contracts
{
    using System.Threading.Tasks;
    using System.Xml;

    public interface IGlobalNamesResolverDataRequester
    {
        Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId = null);

        Task<XmlDocument> SearchWithGlobalNamesResolverPost(string[] scientificNames, int[] sourceId = null);

        Task<XmlDocument> SearchWithGlobalNamesResolverPostNewerRequestVersion(string[] scientificNames, int[] sourceId = null);
    }
}
