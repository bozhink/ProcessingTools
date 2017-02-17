namespace ProcessingTools.Geo.Models
{
    using Contracts.Models;

    public class Coordinate : ICoordinate
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
