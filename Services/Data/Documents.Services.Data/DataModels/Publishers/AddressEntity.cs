namespace ProcessingTools.Documents.Services.Data.DataModels.Publishers
{
    using System;
    using ProcessingTools.Common.Models;
    using ProcessingTools.Documents.Data.Common.Contracts.Models;

    public class AddressEntity : ModelWithUserInformation, IAddressEntity
    {
        public AddressEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
