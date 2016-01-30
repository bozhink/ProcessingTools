namespace ProcessingTools.Wcf.ProxyServices.Bio.Taxonomy.Services
{
    using System.Linq;
    using DataContracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Gbif.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Services;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ServiceContracts;

    public class GbifClassificationService : IGbifClassificationService
    {
        public TaxonClassification GetClassification(string scientificName)
        {
            IGbifDataRequester requester = new GbifDataRequester();
            IGbifTaxaClassificationDataService service = new GbifTaxaClassificationDataService(requester);

            var result = service.Resolve(scientificName).FirstOrDefault();

            return new TaxonClassification
            {
                ScientificName = result.ScientificName,
                Rank = result.Rank
            };
        }
    }
}
