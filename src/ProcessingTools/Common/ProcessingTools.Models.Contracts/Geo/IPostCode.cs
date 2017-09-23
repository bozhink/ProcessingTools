namespace ProcessingTools.Models.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Enumerations;

    public interface IPostCode : IIntegerIdentifiable
    {
        string Code { get; }

        PostCodeType Type { get; }

        int CityId { get; }
    }
}
