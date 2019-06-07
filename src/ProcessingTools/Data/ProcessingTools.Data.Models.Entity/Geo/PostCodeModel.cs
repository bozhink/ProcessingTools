namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Geo;

    public class PostCodeModel : IPostCode
    {
        public int CityId { get; set; }

        public string Code { get; set; }

        public int Id { get; set; }

        public PostCodeType Type { get; set; }
    }
}
