namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;

    public interface IPostCode : IIntegerIdentifiable
    {
        string Code { get; }

        PostCodeType Type { get; }
    }
}
