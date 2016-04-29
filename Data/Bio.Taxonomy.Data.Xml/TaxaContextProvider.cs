namespace ProcessingTools.Bio.Taxonomy.Data.Xml
{
    using Contracts;

    public class TaxaContextProvider : ITaxaContextProvider
    {
        public ITaxaContext Create()
        {
            return new TaxaContext();
        }
    }
}