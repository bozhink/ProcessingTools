namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Enumerations;
    using ProcessingTools.Contracts.Models.Geo;

    internal class PostCodeModel : IPostCode
    {
        public int CityId { get; set; }

        public string Code { get; set; }

        public int Id { get; set; }

        public PostCodeType Type { get; set; }
    }
}
