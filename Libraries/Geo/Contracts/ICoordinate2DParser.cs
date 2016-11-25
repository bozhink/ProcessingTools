namespace ProcessingTools.Geo.Contracts
{
    public interface ICoordinate2DParser
    {
        void ParseCoordinateString(string coordinateString, string coordinateType, ICoordinatePart latitude, ICoordinatePart longitude);
    }
}
