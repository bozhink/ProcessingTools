namespace ProcessingTools.Contracts.Models.Geo
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;

    public interface IPostCode : IIntegerIdentifiable
    {
        string Code { get; }

        PostCodeType Type { get; }

        int CityId { get; }
    }
}
