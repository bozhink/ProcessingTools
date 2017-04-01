namespace ProcessingTools.Web.Models.Cities
{
    using ProcessingTools.Contracts.Models;

    public class CountryResponseModel : INameableIntegerIdentifiable
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
