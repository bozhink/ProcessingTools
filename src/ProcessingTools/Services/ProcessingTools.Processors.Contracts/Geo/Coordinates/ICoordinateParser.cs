namespace ProcessingTools.Processors.Contracts.Geo.Coordinates
{
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

    public interface ICoordinateParser
    {
        ICoordinate ParseCoordinateString(string coordinateString, string coordinateType = null);
    }
}
