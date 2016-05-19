namespace ProcessingTools.Wcf.ProxyServices.Bio.Taxonomy.ServiceContracts
{
    using System.ServiceModel;
    using DataContracts;

    [ServiceContract]
    public interface IGbifClassificationService
    {
        [OperationContract]
        TaxonClassification GetClassification(string scientificName);
    }
}
