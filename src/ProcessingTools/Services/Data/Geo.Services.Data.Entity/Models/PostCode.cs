namespace ProcessingTools.Geo.Services.Data.Entity.Models
{
    using ProcessingTools.Contracts.Services.Data.Geo.Models;
    using ProcessingTools.Enumerations;

    internal class PostCode : IPostCode
    {
        public int CityId { get; set; }

        public string Code { get; set; }

        public int Id { get; set; }

        public PostCodeType Type { get; set; }
    }
}
