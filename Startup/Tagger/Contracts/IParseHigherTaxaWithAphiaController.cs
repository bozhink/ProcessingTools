namespace ProcessingTools.MainProgram.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public interface IParseHigherTaxaWithAphiaController : IParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaClassificationDataService>
    {
    }
}
