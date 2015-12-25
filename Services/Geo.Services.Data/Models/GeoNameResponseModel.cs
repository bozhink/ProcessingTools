namespace ProcessingTools.Geo.Services.Data.Models
{
    using Contracts;

    public class GeoNameResponseModel : IGeoName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}