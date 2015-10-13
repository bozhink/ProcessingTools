namespace ProcessingTools.BaseLibrary.Coordinates
{
    public class Coordinate : ICoordinate
    {
        public Coordinate()
            : this(null, null)
        {
        }

        public Coordinate(string latitude, string longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
