namespace ProcessingTools.Web.Documents.Areas.Data.ViewModels.CoordinatesCalculator
{
    using System.Collections.Generic;

    public interface ICoordinates
    {
        ICollection<ICoordinate> Coordinates { get; }
    }
}
