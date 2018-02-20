namespace ProcessingTools.Processors.Contracts.Geo.Coordinates
{
    using ProcessingTools.Processors.Models.Contracts.Geo.Coordinates;

    public interface ICoordinate2DParser
    {
        void ParseCoordinateString(string coordinateString, string coordinateType, ICoordinatePart latitude, ICoordinatePart longitude);
    }
}
