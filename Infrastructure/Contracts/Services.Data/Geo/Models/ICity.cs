namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models;

    public interface ICity : ISynonymisable<ICitySynonym>, INameableIntegerIdentifiable, IServiceModel
    {
        ICollection<IPostCode> PostCodes { get; }
    }
}
