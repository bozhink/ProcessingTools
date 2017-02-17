namespace ProcessingTools.Geo.Contracts
{
    using Models;

    public interface ICoordinate2DParser
    {
        void ParseCoordinateString(string coordinateString, string coordinateType, ICoordinatePart latitude, ICoordinatePart longitude);
    }
}
